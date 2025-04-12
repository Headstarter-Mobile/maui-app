
-- DROP ALL TABLES IF THEY EXIST (for testing purposes)
DROP TABLE IF EXISTS application, auth_token, company, notification, office, position, position_office, "users" CASCADE;

-- Table: company
CREATE TABLE company (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    logo TEXT,
    website TEXT,
    created_at TIMESTAMP,
    updated_at TIMESTAMP
);

-- Table: office
CREATE TABLE office (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    address TEXT,
    location TEXT,
    description TEXT,
    company_id INTEGER REFERENCES company(id),
    created_at TIMESTAMP,
    updated_at TIMESTAMP
);

-- Table: position
CREATE TABLE position (
    id SERIAL PRIMARY KEY,
    status INTEGER,
    title TEXT,
    description TEXT,
    company_id INTEGER REFERENCES company(id),
    external_application_link TEXT,
    created_at TIMESTAMP,
    updated_at TIMESTAMP,
    published_at TIMESTAMP,
    expires_at TIMESTAMP,
    level INTEGER,
    years_required_from INTEGER,
    years_required_to INTEGER,
    hours INTEGER,
    type INTEGER
);

-- Table: position_office (for repeated office)
CREATE TABLE position_office (
    position_id INTEGER REFERENCES position(id),
    office_id INTEGER REFERENCES office(id),
    PRIMARY KEY (position_id, office_id)
);

-- Table: "users"
CREATE TABLE "users" (
    id SERIAL PRIMARY KEY,
    name TEXT,
    email TEXT UNIQUE,
    password TEXT,
    company_id INTEGER REFERENCES company(id),
    type INTEGER,
    created_at TIMESTAMP,
    updated_at TIMESTAMP
);

-- Table: application
CREATE TABLE application (
    id BIGSERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES "users"(id),
    position_id INTEGER REFERENCES position(id),
    status TEXT,
    created_at TIMESTAMP,
    updated_at TIMESTAMP
);

-- Table: auth_token
CREATE TABLE auth_token (
    token TEXT PRIMARY KEY,
    user_id INTEGER REFERENCES "users"(id),
    expires_on TIMESTAMP,
    device_type TEXT,
    device_os TEXT
);

-- Table: notification
CREATE TABLE notification (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES "users"(id),
    title TEXT,
    text TEXT,
    type TEXT,
    created_at TIMESTAMP,
    "read" BOOLEAN
);


-- Insert test companies
INSERT INTO company (name, description, logo, website, created_at, updated_at)
VALUES 
('OpenAI', 'AI research lab', 'openai.png', 'https://openai.com', now(), now()),
('TechCorp', 'Tech solutions provider', 'techcorp.png', 'https://techcorp.com', now(), now());

-- Insert test offices
INSERT INTO office (name, address, location, description, company_id, created_at, updated_at)
VALUES
('HQ', '123 Main St', 'San Francisco', 'Main headquarters', 1, now(), now()),
('Branch Office', '456 Side St', 'New York', 'East coast branch', 2, now(), now());

-- Insert test users with Argon2-hashed passwords
INSERT INTO "users" (name, email, password, company_id, type, created_at, updated_at)
VALUES
('Alice Johnson', 'alice@example.com', '$argon2id$v=19$m=65536,t=3,p=4$UZkKDzX2myuA2wOFm0zYLg$00JiBxrWlayha2RIPEbfMvz7qO9FV7WxpXJeGX2qdzE', 1, 0, now(), now()), -- pass123
('Bob Smith', 'bob@example.com', '$argon2id$v=19$m=65536,t=3,p=4$KKHuKBtIMjY3Amn9kGR73Q$K0j3BB3Cki7kArynySh1NXhABqo5VTwATSRdNKM6TwM', 2, 1, now(), now()); -- pass456

-- Insert test positions
INSERT INTO position (status, title, description, company_id, external_application_link, created_at, updated_at, published_at, expires_at, level, years_required_from, years_required_to, hours, type)
VALUES
(1, 'Software Engineer', 'Develop awesome apps', 1, 'https://apply.here', now(), now(), now(), now() + interval '30 days', 2, 2, 5, 40, 0);

-- Link position to offices
INSERT INTO position_office (position_id, office_id)
VALUES
(1, 1),
(1, 2);

-- Insert test applications
INSERT INTO application (user_id, position_id, status, created_at, updated_at)
VALUES
(1, 1, 'submitted', now(), now());

-- Insert test auth tokens
INSERT INTO auth_token (token, user_id, expires_on, device_type, device_os)
VALUES
('token123', 1, now() + interval '1 day', 'mobile', 'iOS');

-- Insert test notifications
INSERT INTO notification (user_id, title, text, type, created_at, "read")
VALUES
(1, 'New Position Posted', 'Check out the new Software Engineer role!', 'info', now(), false);
