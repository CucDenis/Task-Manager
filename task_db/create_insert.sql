-- ===========================
-- CREATE TABLES
-- ===========================

CREATE TABLE roles (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) UNIQUE
);

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,  -- Added password field
    cnp VARCHAR(20) UNIQUE NOT NULL, -- Added unique CNP
    role_id INT REFERENCES roles(id),
    refresh_token TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE companies (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    cui VARCHAR(20) UNIQUE NOT NULL, -- Added unique CUI
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE system_types (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE sub_system_types (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) UNIQUE NOT NULL,
    system_type_id INT REFERENCES system_types(id) ON DELETE CASCADE
);

CREATE TABLE availability (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE technicians (
    id SERIAL PRIMARY KEY,
    user_id INT UNIQUE REFERENCES users(id) ON DELETE CASCADE,
    company_id INT REFERENCES companies(id) ON DELETE SET NULL,
    phone VARCHAR(20) UNIQUE NOT NULL,
    availability_id INT REFERENCES availability(id) ON DELETE SET NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE sub_system_type_expertise (
    id SERIAL PRIMARY KEY,
    technician_id INT REFERENCES technicians(id) ON DELETE CASCADE,
    sub_system_type_id INT REFERENCES sub_system_types(id) ON DELETE CASCADE
);

CREATE TABLE contracts (
    id SERIAL PRIMARY KEY,
    client_company_id INT REFERENCES companies(id) ON DELETE CASCADE,
    technician_company_id INT REFERENCES companies(id) ON DELETE CASCADE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE clients (
    id SERIAL PRIMARY KEY,
    user_id INT UNIQUE REFERENCES users(id) ON DELETE CASCADE,
    company_id INT REFERENCES companies(id) ON DELETE SET NULL,
    phone VARCHAR(20) UNIQUE NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE urgency_levels (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE interventions (
    id SERIAL PRIMARY KEY,
    client_id INT REFERENCES clients(id) ON DELETE CASCADE,
    technician_id INT REFERENCES technicians(id) ON DELETE CASCADE,
    name VARCHAR(255) NOT NULL,
    level_id INT REFERENCES urgency_levels(id) ON DELETE SET NULL,
    description TEXT,
    location TEXT,
    client_signature BYTEA,
    technician_signature BYTEA,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE invoices (
    id SERIAL PRIMARY KEY,
    contract_id INT REFERENCES contracts(id) ON DELETE CASCADE,
    intervention_id INT REFERENCES interventions(id) ON DELETE CASCADE,
    description TEXT NOT NULL,
    emmiting_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


-- ===========================
-- BULK INSERT DATA
-- ===========================

-- Insert roles
INSERT INTO roles (name) VALUES 
('Client'), 
('Technician'), 
('Admin');

-- Insert system types
INSERT INTO system_types (name) VALUES 
('HVAC'), 
('Electrical'), 
('Plumbing');

-- Insert sub-system types with random system type references
INSERT INTO sub_system_types (name, system_type_id)
SELECT 'Type ' || i, id
FROM generate_series(1, 10) i
CROSS JOIN LATERAL (SELECT id FROM system_types ORDER BY random() LIMIT 1) s;

-- Insert availability
INSERT INTO availability (name) VALUES 
('Available'), 
('Busy'), 
('On Leave');

-- Insert urgency levels
INSERT INTO urgency_levels (name) VALUES 
('Low'), 
('Medium'), 
('High'), 
('Critical');

-- Insert companies with random names
INSERT INTO companies (name, cui)
SELECT 'Company ' || i, 'CUI' || i
FROM generate_series(1, 50) i;

-- Insert users with random roles
INSERT INTO users (first_name, last_name, email, role_id, refresh_token, password, cnp)
SELECT 
    'User' || i,
    'Last' || i,
    'user' || i || '@example.com',
    role.id,  -- Randomly assigned role_id from roles
    NULL,
    'password' || i,  -- Example password
    'CNP' || i
FROM generate_series(1, 200) i
CROSS JOIN LATERAL (
    SELECT id
    FROM roles
    ORDER BY random() 
    LIMIT 1
) role;

-- Insert clients with valid user_id and company_id references
INSERT INTO clients (user_id, company_id, phone, created_at)
SELECT 
    client_user.id,  -- Reference user_id from the users table where role is 'Client'
    company.id,      -- Reference company_id from companies table
    '0700000' || client_user.id,  -- Generate phone number based on user_id
    CURRENT_TIMESTAMP
FROM 
    (SELECT id
     FROM users 
     WHERE role_id = 1  -- Only select users with role_id = 1 (Clients)
     ORDER BY random() 
     LIMIT 100) AS client_user
CROSS JOIN LATERAL (
    SELECT id
    FROM companies
    ORDER BY random()
    LIMIT 1
) AS company;

-- Insert technicians with valid user_id, company_id, and availability_id references
INSERT INTO technicians (user_id, company_id, phone, availability_id, created_at)
SELECT 
    user_data.id,  -- Reference user_id from the users subquery
    company.id,    -- Reference company_id from companies table
    '0700000' || user_data.id,  -- Generate phone number based on user_id
    availability.id,  -- Reference availability_id from availability table
    CURRENT_TIMESTAMP
FROM 
    (SELECT id
     FROM users 
     WHERE role_id = 2  -- Only select users with role_id = 2 (Technicians)
     ORDER BY random() 
     LIMIT 200) AS user_data
CROSS JOIN LATERAL (
    SELECT id
    FROM companies
    ORDER BY random()
    LIMIT 1
) AS company
CROSS JOIN LATERAL (
    SELECT id
    FROM availability
    ORDER BY random()
    LIMIT 1
) AS availability;

-- Insert sub_system_type_expertise with valid technician_id and sub_system_type_id references
INSERT INTO sub_system_type_expertise (technician_id, sub_system_type_id)
SELECT tech.id, sub.id
FROM 
    (SELECT id FROM technicians ORDER BY random() LIMIT 500) tech,
    (SELECT id FROM sub_system_types ORDER BY random() LIMIT 500) sub;

-- Insert contracts with valid client_company_id and technician_company_id references
INSERT INTO contracts (client_company_id, technician_company_id, created_at)
SELECT 
    client_company.id,  -- Reference client_company_id from companies
    technician_company.id,  -- Reference technician_company_id from companies
    CURRENT_TIMESTAMP
FROM 
    (SELECT id
     FROM companies
     ORDER BY random() 
     LIMIT 100) AS client_company
CROSS JOIN LATERAL (
    SELECT id
    FROM companies
    ORDER BY random()
    LIMIT 1
) AS technician_company;

-- Insert interventions with valid client_id, technician_id, level_id, and other references
INSERT INTO interventions (client_id, technician_id, name, level_id, description, location, client_signature, technician_signature, created_at)
SELECT 
    client.id,  -- Reference client_id from clients
    technician.id,  -- Reference technician_id from technicians
    'Intervention ' || i,  -- Generate unique intervention name
    urgency_level.id,  -- Reference urgency_level from urgency_levels
    'Description for intervention ' || i,  -- Generate description for intervention
    'Location ' || i,  -- Generate location for intervention
    NULL,  -- Placeholder for client_signature
    NULL,  -- Placeholder for technician_signature
    CURRENT_TIMESTAMP
FROM generate_series(1, 100000) i
CROSS JOIN LATERAL (
    SELECT id
    FROM clients
    ORDER BY random()
    LIMIT 1
) AS client
CROSS JOIN LATERAL (
    SELECT id
    FROM technicians
    ORDER BY random()
    LIMIT 1
) AS technician
CROSS JOIN LATERAL (
    SELECT id
    FROM urgency_levels
    ORDER BY random()
    LIMIT 1
) AS urgency_level;

-- Insert invoices with valid contract_id, intervention_id, and other references
INSERT INTO invoices (contract_id, intervention_id, description, emmiting_date, created_at)
SELECT 
    contract.id,  -- Reference contract_id from contracts
    intervention.id,  -- Reference intervention_id from interventions
    'Invoice for intervention ' || i,  -- Generate unique invoice description
    CURRENT_TIMESTAMP,  -- Set emmiting_date
    CURRENT_TIMESTAMP  -- Set created_at
FROM generate_series(1, 50000) i
CROSS JOIN LATERAL (
    SELECT id
    FROM contracts
    ORDER BY random()
    LIMIT 1
) AS contract
CROSS JOIN LATERAL (
    SELECT id
    FROM interventions
    ORDER BY random()
    LIMIT 1
) AS intervention;
