const grpc = require('@grpc/grpc-js');
const { validateToken, checkPermission, db2pb_company } = require('../utils');

module.exports = {
    getCompany: (pool) => async (call, callback) => {
        try {
            const { id } = call.request; // call.request is Company
            const client = await pool.connect();
            const result = await client.query('SELECT * FROM company WHERE id = $1', [id]);
            client.release();

            if (result.rows.length > 0) {
                console.log(db2pb_company(result.rows[0]));
                callback(null, db2pb_company(result.rows[0]));
            } else {
                callback({ code: grpc.status.NOT_FOUND, details: 'Company not found' });
            }
        } catch (error) {
            console.error('Error getting company:', error);
            callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
        }
    },
    getAllCompanies: (pool) => async (call) => {
        try {
            const client = await pool.connect();
            const result = await client.query('SELECT * FROM company');
            client.release();

            result.rows.forEach((row) => {
                const pb_data = db2pb_company(row);
                console.log(pb_data);
                call.write(pd_data);
            });
            call.end();
        } catch (error) {
            console.error('Error getting all companies:', error);
            call.emit('error', { code: grpc.status.INTERNAL, details: 'Internal server error' });
        }
    },
    createCompany: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            // Assuming only admins can create companies
            checkPermission('companies.create', {})(call, callback, async () => { // Adjust permission string as needed
                try {
                    const { name, description, logo, website, createdAt, updatedAt } = call.request; // call.request is Company
                    const client = await pool.connect();
                    const result = await client.query(
                        'INSERT INTO companies (name, description, logo, website, created_at, updated_at) VALUES ($1, $2, $3, $4, $5, $6) RETURNING *',
                        [name, description, logo, website, createdAt, updatedAt]
                    );
                    client.release();

                    if (result.rows.length > 0) {
                        callback(null, result.rows[0]);
                    } else {
                        callback({ code: grpc.status.INTERNAL, details: 'Could not create company' });
                    }
                } catch (error) {
                    console.error('Error creating company:', error);
                    callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
                }
            });
        });
    },
    updateCompany: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            try {
                const { oldData, newData } = call.request; // call.request is CompanyRequest
                const { id, name, description, logo, website, createdAt, updatedAt } = newData;

                // You might want to use oldData for optimistic concurrency control or auditing
                const client = await pool.connect();
                const result = await client.query(
                    'UPDATE companies SET name = $2, description = $3, logo = $4, website = $5, created_at = $6, updated_at = $7 WHERE id = $1 RETURNING *',
                    [id, name, description, logo, website, createdAt, updatedAt]
                );
                client.release();

                if (result.rows.length > 0) {
                    callback(null, result.rows[0]);
                } else {
                    callback({ code: grpc.status.NOT_FOUND, details: 'Company not found' });
                }
            } catch (error) {
                console.error('Error updating company:', error);
                callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
            }
        });
    },
    deleteCompany: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            // Assuming only admins can delete companies
            checkPermission('companies.delete', {})(call, callback, async () => { // Adjust permission string as needed
                try {
                    const { id } = call.request; // call.request is Company
                    const client = await pool.connect();
                    await client.query('DELETE FROM company WHERE id = $1', [id]);
                    client.release();
                    callback(null, {});
                } catch (error) {
                    console.error('Error deleting company:', error);
                    callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
                }
            });
        });
    },
};
