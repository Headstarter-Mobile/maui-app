const grpc = require('@grpc/grpc-js');
const { hashPassword, verifyPassword, generateToken } = require('../utils'); // Import utilities
const { validateToken, checkPermission } = require('../utils');

module.exports = {
    getUser: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            checkPermission('users.read', {})(call, callback, async () => { // Adjust permission as needed
                try {
                    const { id } = call.request; // call.request is User
                    const client = await pool.connect();
                    const result = await client.query('SELECT id, name, email, company_id, type, created_at, updated_at FROM users WHERE id = $1', [id]);
                    client.release();

                    if (result.rows.length > 0) {
                        callback(null, result.rows[0]);
                    } else {
                        callback({ code: grpc.status.NOT_FOUND, details: 'User not found' });
                    }
                } catch (error) {
                    console.error('Error getting user:', error);
                    callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
                }
            });
        });
    },
    getAllUsers: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            checkPermission('users.read', {})(call, callback, async () => { // Adjust permission as needed
                try {
                    const client = await pool.connect();
                    const result = await client.query('SELECT id, name, email, company_id, type, created_at, updated_at FROM users');
                    client.release();

                    result.rows.forEach((row) => call.write(row));
                    call.end();
                } catch (error) {
                    console.error('Error getting all users:', error);
                    call.emit('error', { code: grpc.status.INTERNAL, details: 'Internal server error' });
                }
            });
        });
    },
    createUser: (pool) => async (call, callback) => {
        try {
            const { name, email, password, companyId, type, createdAt, updatedAt } = call.request; // call.request is User
            const hashedPassword = await hashPassword(password); // Hash the password

            const client = await pool.connect();
            let query, values;
            if (type === 0) { // CANDIDATE
                query = 'INSERT INTO users (name, email, password, company_id, type, created_at, updated_at) VALUES ($1, $2, $3, $4, $5, $6, $7) RETURNING id, name, email, company_id, type, created_at, updated_at';
                values = [name, email, hashedPassword, null, type, createdAt, updatedAt];
            }
            else if (type === 1) { // RECRUITER
                query = 'INSERT INTO users (name, email, password, company_id, type, created_at, updated_at) VALUES ($1, $2, $3, $4, $5, $6, $7) RETURNING id, name, email, company_id, type, created_at, updated_at';
                values = [name, email, hashedPassword, companyId, type, createdAt, updatedAt];
            }
            const result = await client.query(query, values);
            client.release();

            if (result.rows.length > 0) {
                callback(null, result.rows[0]);
            } else {
                callback({ code: grpc.status.INTERNAL, details: 'Could not create user' });
            }
        } catch (error) {
            console.error('Error creating user:', error);
            callback({ code: grpc.status.INTERNAL, details: 'Internal server error' });
        }
    },
    loginUser: (pool) => async (call, callback) => {
        try {
            const { email, password } = call.request; // This is a User message

            const client = await pool.connect();
            const result = await client.query(
                'SELECT id, name, email, password, company_id, type, created_at, updated_at FROM users WHERE email = $1',
                [email]
            );
            client.release();

            if (result.rows.length === 0) {
                return callback({
                    code: grpc.status.UNAUTHENTICATED,
                    details: 'User with the given email not found.',
                });
            }

            const user = result.rows[0];

            const passwordMatch = await verifyPassword(user.password, password);
            if (!passwordMatch) {
                return callback({
                    code: grpc.status.UNAUTHENTICATED,
                    details: 'Invalid password.',
                });
            }

            // Determine permissions
            let permissions = [];
            switch (user.type) {
                case 0: // CANDIDATE
                    permissions = [
                        'positions.read.all',
                        'companies.read.all',
                        'applications.create.own',
                        'applications.read.own',
                        'notifications.create',
                        'notifications.read.own',
                        'users.delete.own',
                        'users.update.own',
                        'auth_tokens.invalidate.own',
                    ];
                    break;
                case 1: // RECRUITER
                    permissions = [
                        'applications.read.own',
                        'positions.create.company',
                        'positions.update.company',
                        'positions.read.all',
                        'companies.read.all',
                        'positions.delete.company',
                        'companies.update.company',
                        'offices.create.company',
                        'offices.update.company',
                        'offices.delete.company',
                        'notifications.create',
                        'notifications.read.own',
                        'users.delete.own',
                        'users.update.own',
                        'auth_tokens.invalidate.own',
                    ];
                    break;
                case 2: // ADMIN
                    permissions = ['admin'];
                    break;
            }

            const token = await generateToken(user.id, permissions, '1h'); // returns AuthToken

            // Respond with LoggedUserData { userData, token }
            callback(null, {
                userData: {
                    id: user.id,
                    name: user.name,
                    email: user.email,
                    companyId: user.company_id,
                    type: user.type,
                    createdAt: user.created_at.toISOString?.() ?? user.created_at,
                    updatedAt: user.updated_at.toISOString?.() ?? user.updated_at,
                },
                token: {
                    token: token,
                    userId: user.id,
                    expiresOn: '',
                    deviceType: '',
                    deviceOs: '',
                }
            });
        } catch (err) {
            console.error('Login error:', err);
            callback({
                code: grpc.status.INTERNAL,
                details: 'Internal server error',
            });
        }
    },
    updateUser: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            checkPermission('users.update.own', { userId: call.user.id })(call, callback, async () => {
                try {
                    const { oldData, newData } = call.request; // call.request is UserUpdateRequest
                    const { id, name, email, password, companyId, type, createdAt, updatedAt } = newData;
                    const hashedPassword = await hashPassword(password);

                    const client = await pool.connect();
                    const result = await client.query(
                        'UPDATE users SET name = $2, email = $3, password = $4, company_id = $5, type = $6, created_at = $7, updated_at = $8 WHERE id = $1 RETURNING id, name, email, company_id, type, created_at, updated_at',
                        [id, name, email, hashedPassword, companyId, type, createdAt, updatedAt]
                    );
                    client.release();

                    if (result.rows.length > 0) {
                        callback(null, result.rows[0]);
                    } else {
                        callback({ code: grpc.status.NOT_FOUND, details: 'User not found' });
                    }
                } catch (error) {
                    console.error('error updating user', error);
                    callback({ code: grpc.status.INTERNAL, details: 'internal server error' });
                }
            });
        });
    },
    deleteUser: (pool) => async (call, callback) => {
        validateToken(call, callback, async () => {
            checkPermission('users.delete.own', { userId: call.user.id })(call, callback, async () => {
                try {
                    const { id } = call.request; // call.request is User
                    const client = await pool.connect();
                    await client.query('DELETE FROM users WHERE id = $1', [id]);
                    client.release();
                    callback(null, {});
                } catch (error) {
                    console.error('error deleting user', error);
                    callback({ code: grpc.status.INTERNAL, details: 'internal server error' });
                }
            });
        });
    },
};
