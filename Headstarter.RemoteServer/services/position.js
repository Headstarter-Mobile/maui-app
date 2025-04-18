const grpc = require('@grpc/grpc-js');
const { validateToken, checkPermission, db2pb_position } = require('../utils');

module.exports = {
    getPosition: (pool) => async (call, callback) => {
        try {
            const { id } = call.request; // call.request is Position
            const client = await pool.connect();
            const result = await client.query('SELECT * FROM position WHERE id = $1', [id]);
            client.release();

            if (result.rows.length > 0) {
                callback(null, result.rows[0]);
            } else {
                callback({ code: grpc.status.NOT_FOUND, details: 'Position not found' });
            }
        } catch (error) {
            console.error('Error getting position:', error);
            callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
        }
    },
    getAllPositions: (pool) => async (call) => {
        try {
            const filters = call.request;
            console.log(filters);
            let query = `SELECT * FROM position WHERE 1=1`;
            const values = [];
            let paramIndex = 1;
            // Filter by position ID
            if (filters.id) {
                query += ` AND id = $${paramIndex++}`;
                values.push(filters.id);
            }

            // Filter by company ID
            if (filters.companyId) {
                query += ` AND company_id = $${paramIndex++}`;
                values.push(filters.companyId);
            }

            // Filter by position level
            if (filters.level !== undefined && filters.level !== null) {
                query += ` AND level = $${paramIndex++}`;
                values.push(filters.level);
            }

            // Filter by type
            if (filters.type !== undefined && filters.type !== null) {
                query += ` AND type = $${paramIndex++}`;
                values.push(filters.type);
            }

            // Filter by hours
            if (filters.hours) {
                query += ` AND hours = $${paramIndex++}`;
                values.push(filters.hours);
            }

            // Filter by status
            if (filters.status !== undefined && filters.status !== null) {
                query += ` AND status = $${paramIndex++}`;
                values.push(filters.status);
            }

            // Filter by search in title
            if (filters.title && filters.title.trim() !== '') {
                query += ` AND LOWER(title) LIKE $${paramIndex++}`;
                values.push(`%${filters.title.toLowerCase()}%`);
            }

            // Filter by min and max years
            if (filters.yearsRequired?.from) {
                query += ` AND years_required_from >= $${paramIndex++}`;
                values.push(filters.yearsRequired.from);
            }

            if (filters.yearsRequired?.to) {
                query += ` AND years_required_to <= $${paramIndex++}`;
                values.push(filters.yearsRequired.to);
            }

            console.log(query, values);

            const client = await pool.connect();

            // 1. Get all positions
            const positionResult = await client.query(query, values);
            const positionIds = positionResult.rows.map(r => r.id);

            console.log(positionResult.rows);

            // 2. Get related offices
            const officeQuery = `
        SELECT o.*, po.position_id
        FROM office o
        JOIN position_office po ON o.id = po.office_id
        WHERE po.position_id = ANY($1)
    `;
            const officeResult = await client.query(officeQuery, [positionIds]);

            client.release();

            // 3. Group offices by position_id
            const officesByPositionId = {};
            for (const row of officeResult.rows) {
                const office = {
                    id: row.id,
                    name: row.name,
                    address: row.address,
                    location: row.location,
                    description: row.description,
                    companyId: row.company_id,
                    createdAt: row.created_at.toISOString?.() ?? String(row.created_at),
                    updatedAt: row.updated_at.toISOString?.() ?? String(row.updated_at)
                };

                if (!officesByPositionId[row.position_id]) {
                    officesByPositionId[row.position_id] = [];
                }

                officesByPositionId[row.position_id].push(office);
            }

            // 4. Stream positions with their offices
            for (const row of positionResult.rows) {
                const grpcPosition = db2pb_position(row, officesByPositionId[row.id] ?? []);
                console.log(grpcPosition);
                call.write(grpcPosition);
            }

            call.end();

        } catch (error) {
            console.error('Error getting all positions:', error);
            call.emit('error', {
                code: grpc.status.INTERNAL,
                details: 'Internal server error',
            });
        }
    },
    createPosition: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            checkPermission('positions.create.company', { companyId: call.user.companyId })(call, callback, async () => {
                try {
                    const { status, title, description, companyId, externalApplicationLink, createdAt, updatedAt, publishedAt, expiresAt } = call.request; // call.request is Position
                    const client = await pool.connect();
                    const result = await client.query(
                        'INSERT INTO positions (status, title, description, company_id, external_application_link, created_at, updated_at, published_at, expires_at) VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9) RETURNING *',
                        [status, title, description, companyId, externalApplicationLink, createdAt, updatedAt, publishedAt, expiresAt]
                    );
                    client.release();

                    if (result.rows.length > 0) {
                        callback(null, result.rows[0]);
                    } else {
                        callback({ code: grpc.status.INTERNAL, details: 'Could not create position' });
                    }
                } catch (error) {
                    console.error('Error creating position:', error);
                    callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
                }
            });
        });
    },
    updatePosition: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            try {
                const { oldData, newData } = call.request; // call.request is PositionUpdateRequest
                const { id, status, title, description, companyId, externalApplicationLink, createdAt, updatedAt, publishedAt, expiresAt } = newData;

                const client = await pool.connect();
                const result = await client.query('SELECT company_id FROM position WHERE id = $1', [id]);
                client.release();

                if (result.rows.length === 0) {
                    return callback({ code: grpc.status.NOT_FOUND, details: 'Position not found' });
                }

                const positionCompanyId = result.rows[0].company_id;
                checkPermission('positions.update.company', { companyId: positionCompanyId })(call, callback, async () => {
                    const result = await pool.query(
                        'UPDATE positions SET status = $2, title = $3, description = $4, company_id = $5, external_application_link = $6, created_at = $7, updated_at = $8, published_at = $9, expires_at = $10 WHERE id = $1 RETURNING *',
                        [id, status, title, description, companyId, externalApplicationLink, createdAt, updatedAt, publishedAt, expiresAt]
                    );
                    callback(null, result.rows[0]);
                });
            } catch (error) {
                console.error('Error updating position:', error);
                callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
            }
        });
    },
    deletePosition: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            try {
                const { id } = call.request; // call.request is Position
                const client = await pool.connect();
                const result = await client.query('SELECT company_id FROM position WHERE id = $1', [id]);
                client.release();

                if (result.rows.length === 0) {
                    return callback({ code: grpc.status.NOT_FOUND, details: 'Position not found' });
                }

                const positionCompanyId = result.rows[0].company_id;
                checkPermission('positions.delete.company', { companyId: positionCompanyId })(call, callback, async () => {
                    await client.query('DELETE FROM position WHERE id = $1', [id]);
                    client.release();
                    callback(null, {});
                });
            } catch (error) {
                console.error('Error deleting position:', error);
                callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
            }
        });
    },
};
