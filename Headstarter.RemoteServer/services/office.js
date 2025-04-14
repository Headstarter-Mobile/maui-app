const grpc = require('@grpc/grpc-js');
const { validateToken, checkPermission, db2pb_office } = require('../utils');

module.exports = {
    getOffice: (pool) => async (call, callback) => {
        try {
            const { id } = call.request; // call.request is Office
            const client = await pool.connect();
            const result = await client.query('SELECT * FROM office WHERE id = $1', [id]);
            client.release();

            if (result.rows.length > 0) {
                callback(null, db2pb_office(result.rows[0]));
            } else {
                callback({ code: grpc.status.NOT_FOUND, details: 'Office not found' });
            }
        } catch (error) {
            console.error('Error getting office:', error);
            callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
        }
    },
    getAllOffices: (pool) => async (call) => {
        try {
            const filters = call.request;
            let query = `SELECT * FROM office WHERE 1=1`;
            const values = [];
            let paramIndex = 1;

            if (filters.id) {
                query += ` AND id = $${paramIndex++}`;
                values.push(filters.id);
            }

            if (filters.name) {
                query += ` AND name ILIKE $${paramIndex++}`;
                values.push(filters.name);
            }

            if (filters.address) {
                query += ` AND address ILIKE $${paramIndex++}`;
                values.push(filters.address);
            }

            if (filters.location) {
                query += ` AND location ILIKE $${paramIndex++}`;
                values.push(filters.location);
            }

            if (filters.description) {
                query += ` AND description ILIKE $${paramIndex++}`;
                values.push(filters.description);
            }

            if (filters.companyId) {
                query += ` AND company_id = $${paramIndex++}`;
                values.push(filters.companyId);
            }

            const client = await pool.connect();
            const result = await client.query(query, values);
            client.release();

            result.rows.forEach((row) => {
                const pb_office = db2pb_office(row);
                call.write(pb_office);
                console.log("Written:", pb_office);
            });
            call.end();
            console.log("ended");
        } catch (error) {
            console.error('Error getting all offices:', error);
            call.emit('error', {
                code: grpc.status.INTERNAL,
                details: 'Internal server error',
            });
        }
    },
    createOffice: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            checkPermission('offices.create.company', { companyId: call.user.companyId })(call, callback, async () => {
                try {
                    const { name, address, location, description, companyId, createdAt, updatedAt } = call.request; // call.request is Office
                    const client = await pool.connect();
                    const result = await client.query(
                        'INSERT INTO offices (name, address, location, description, company_id, created_at, updated_at) VALUES ($1, $2, $3, $4, $5, $6, $7) RETURNING *',
                        [name, address, location, description, companyId, createdAt, updatedAt]
                    );
                    client.release();

                    if (result.rows.length > 0) {
                        callback(null, result.rows[0]);
                    } else {
                        callback({ code: grpc.status.INTERNAL, details: 'Could not create office' });
                    }
                } catch (error) {
                    console.error('Error creating office:', error);
                    callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
                }
            });
        });
    },
    updateOffice: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            try {
                const { oldData, newData } = call.request; // call.request is OfficeUpdateRequest
                const { id, name, address, location, description, companyId, createdAt, updatedAt } = newData;

                const client = await pool.connect();
                const result = await client.query('SELECT company_id FROM office WHERE id = $1', [id]);
                client.release();

                if (result.rows.length === 0) {
                    return callback({ code: grpc.status.NOT_FOUND, details: 'Office not found' });
                }

                const officeCompanyId = result.rows[0].company_id;
                checkPermission('offices.update.company', { companyId: officeCompanyId })(call, callback, async () => {
                    const result = await pool.query(
                        'UPDATE offices SET name = $2, address = $3, location = $4, description = $5, company_id = $6, created_at = $7, updated_at = $8 WHERE id = $1 RETURNING *',
                        [id, name, address, location, description, companyId, createdAt, updatedAt]
                    );
                    callback(null, result.rows[0]);
                });
            } catch (error) {
                console.error('Error updating office:', error);
                callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
            }
        });
    },
    deleteOffice: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            try {
                const { id } = call.request; // call.request is Office
                const client = await pool.connect();
                const result = await client.query('SELECT company_id FROM office WHERE id = $1', [id]);
                client.release();

                if (result.rows.length === 0) {
                    return callback({ code: grpc.status.NOT_FOUND, details: 'Office not found' });
                }

                const officeCompanyId = result.rows[0].company_id;
                checkPermission('offices.delete.company', { companyId: officeCompanyId })(call, callback, async () => {
                    await client.query('DELETE FROM office WHERE id = $1', [id]);
                    client.release();
                    callback(null, {});
                });
            } catch (error) {
                console.error('Error deleting office:', error);
                callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
            }
        });
    },
};
