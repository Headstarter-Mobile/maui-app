// utils.js
const grpc = require('@grpc/grpc-js');
const jwt = require('jsonwebtoken');
const argon2 = require('argon2');

async function hashPassword(password) {
    try {
        const hash = await argon2.hash(password);
        return hash;
    } catch (err) {
        console.error('Error hashing password:', err);
        throw err;
    }
}

async function verifyPassword(hashedPassword, passwordHashFromClient) {
    try {
        return await argon2.verify(hashedPassword, passwordHashFromClient);
    } catch (err) {
        return false;
    }
}

async function generateToken(userId, permissions, expiresIn) {
    const payload = {
        userId: userId,
        permissions: permissions,
    };
    const secretKey = 'your-secret-key';
    const token = jwt.sign(payload, secretKey, { expiresIn: expiresIn });
    return token;
}

function validateToken(call, callback, next) {
    const token = call.metadata.get('authorization')[0];
    if (!token) {
        return callback({ code: grpc.status.UNAUTHENTICATED, details: 'Missing token' });
    }

    try {
        const decoded = jwt.verify(token, 'your-secret-key');
        call.user = decoded; // Attach user info to call
        next();
    } catch (err) {
        return callback({ code: grpc.status.UNAUTHENTICATED, details: 'Invalid token' });
    }
}

function checkPermission(requiredPermission, context) {
    return (call, callback, next) => {
        if (!call.user) {
            return callback({ code: grpc.status.UNAUTHENTICATED, details: 'Missing user context' });
        }

        if (call.user.permissions.includes('admin')) {
            return next(); // Admin has all permissions
        }

        if (call.user.permissions.includes(requiredPermission)) {
            // Perform contextual checks if needed
            switch (requiredPermission) {
                case 'positions.update.company':
                case 'positions.delete.company':
                    if (context && context.companyId === call.user.companyId) {
                        return next();
                    }
                    break;
                case 'applications.read.own':
                case 'applications.create.own':
                    if (context && context.userId === call.user.id) {
                        return next();
                    }
                    break;
                case 'companies.update.company':
                    if (context && context.companyId === call.user.companyId) {
                        return next();
                    }
                    break;
                case 'offices.create.company':
                case 'offices.update.company':
                case 'offices.delete.company':
                    if (context && context.companyId === call.user.companyId) {
                        return next();
                    }
                    break;
                case 'users.delete.own':
                case 'users.update.own':
                case 'auth_tokens.invalidate.own':
                    if (context && context.userId === call.user.id) {
                        return next();
                    }
                    break;
                default:
                    return next(); // Simple permission check
            }
        }

        return callback({ code: grpc.status.PERMISSION_DENIED, details: 'Insufficient permissions' });
    };
}

function db2pb_company(db_company) {
    return {
        id: db_company.id,
        name: db_company.name,
        description: db_company.description,
        logo: db_company.logo,
        website: db_company.website,
        createdAt: db_company.created_at.toISOString?.() ?? String(db_company.created_at),
        updatedAt: db_company.updated_at.toISOString?.() ?? String(db_company.updated_at),
    };
}

function db2pb_position(db_position, offices) {
    return {
        id: db_position.id,
        status: db_position.status,
        title: db_position.title,
        description: db_position.description,
        companyId: db_position.company_id,
        externalApplicationLink: db_position.external_application_link,
        createdAt: db_position.created_at.toISOString?.() ?? String(db_position.created_at),
        updatedAt: db_position.updated_at.toISOString?.() ?? String(db_position.updated_at),
        publishedAt: db_position.published_at?.toISOString?.() ?? String(db_position.published_at),
        expiresAt: db_position.expires_at?.toISOString?.() ?? String(db_position.expires_at),
        level: db_position.level,
        hours: db_position.hours,
        type: db_position.type,
        yearsRequired: {
            from: db_position.years_required_from,
            to: db_position.years_required_to
        },
        offices: offices
    };
}

function db2pb_office(db_office) {
    return {
        id: db_office.id,
        name: db_office.name,
        address: db_office.address,
        location: db_office.location,
        description: db_office.description,
        companyId: db_office.company_id,
        createdAt: '' + db_office.created_at.toISOString?.() ?? String(db_office.created_at),
        updatedAt: '' + db_office.updated_at.toISOString?.() ?? String(db_office.updated_at),
    };
}

module.exports = {
    hashPassword,
    verifyPassword,
    generateToken,
    validateToken,
    checkPermission,
    db2pb_company,
    db2pb_position,
    db2pb_office
};
