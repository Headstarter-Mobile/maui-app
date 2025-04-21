const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');
const path = require('path');

// Load proto files
const USER_PROTO_PATH = __dirname + '/../Headstarter.Models/Protos/service_user.proto';

const packageDefinition = protoLoader.loadSync(USER_PROTO_PATH, {
    keepCase: true,
    longs: String,
    enums: String,
    defaults: true,
    oneofs: true,
    includeDirs: [__dirname + '/../Headstarter.Models/Protos']
});

const userProto = grpc.loadPackageDefinition(packageDefinition).user;

const client = new userProto.UserService(
    //'129.159.196.117:5001',
    '129.159.196.117:50051',
    //'127.0.0.1:5001',
    grpc.credentials.createInsecure()
);

function createUserObject() {
    return {
        name: 'John Doe',
        email: 'john.doe@example.com',
        password: 'securepassword123',
        companyId: 1,
        type: 0
    };
}

// Updated service calls
function createUser() {
    const user = createUserObject();
    client.CreateUser(user, (err, response) => {
        if (err) console.error('CreateUser error:', err);
        else console.log('Created user:', response);
    });
}

function loginUser() {
    const credentials = {
        id: 0,
        name: '',
        email: 'john.doe@example.com',
        password: 'securepassword123',
        companyId: 0,
        type: 0
    };

    client.LoginUser(credentials, (err, response) => {
        if (err) console.error('Login error:', err);
        else console.log('Login response:', response);
    });
}

function getAllUsers() {
    const request = {
        id: 1,
        name: 'Alice Johnson',
        email: 'alice@example.com',
        password: '$argon2id$v=19$m=65536,t=3,p=4$UZkKDzX2myuA2wOFm0zYLg$00JiBxrWlayha2RIPEbfMvz7qO9FV7WxpXJeGX2qdzE',
        company_id: 1,
        type: 0,
        created_at: '2025-04-11 22:18:20.926388',
        updated_at: '2025-04-11 22:18:20.926388',
    };
    
    const call = client.GetAllUsers(request);

    call.on('data', (user) => console.log('User:', user));
    call.on('end', () => console.log('Stream ended'));
    call.on('error', (err) => console.error('Stream error:', err));
}

setTimeout(getAllUsers, 3000);
