-- Create Tables

CREATE TABLE roles (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255)
);

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    email VARCHAR(255),
    role_id INT REFERENCES roles(id),
    refresh_token TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE companies (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE system_types (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255)
);

CREATE TABLE sub_system_types (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    system_type_id INT REFERENCES system_types(id)
);

CREATE TABLE availability (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255)
);

CREATE TABLE technicians (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id),
    company_id INT REFERENCES companies(id),
    phone VARCHAR(255),
    availability_id INT REFERENCES availability(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE sub_system_type_expertise (
    id SERIAL PRIMARY KEY,
    technician_id INT REFERENCES technicians(id),
    sub_system_type_id INT REFERENCES sub_system_types(id)
);

CREATE TABLE contracts (
    id SERIAL PRIMARY KEY,
    client_company_id INT REFERENCES companies(id),
    technician_company_id INT REFERENCES companies(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE clients (
    id SERIAL PRIMARY KEY,
    user_id INT REFERENCES users(id),
    company_id INT REFERENCES companies(id),
    phone VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE urgency_levels (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255)
);

CREATE TABLE interventions (
    id SERIAL PRIMARY KEY,
    client_id INT REFERENCES clients(id),
    technician_id INT REFERENCES technicians(id),
    name VARCHAR(255),
    level_id INT REFERENCES urgency_levels(id),
    description TEXT,
    location TEXT,
    client_signature BYTEA,
    technician_signature BYTEA,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE invoices (
    id SERIAL PRIMARY KEY,
    contract_id INT REFERENCES contracts(id),
    intervention_id INT REFERENCES interventions(id),
    description TEXT,
    emmiting_date TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Bulk Insert Data
INSERT INTO roles (name) VALUES ('Client'), ('Technician'), ('Admin');

INSERT INTO system_types (name) VALUES ('HVAC'), ('Electrical'), ('Plumbing');

INSERT INTO sub_system_types (name, system_type_id)
SELECT 'Type ' || i, id
FROM generate_series(1, 10) i
CROSS JOIN LATERAL (SELECT id FROM system_types ORDER BY random() LIMIT 1) s;

INSERT INTO availability (name) VALUES ('Available'), ('Busy'), ('On Leave');

INSERT INTO urgency_levels (name) VALUES ('Low'), ('Medium'), ('High'), ('Critical');

INSERT INTO companies (name)
SELECT 'Company ' || i FROM generate_series(1, 50) i;

-- Populate Users Table with proper role_id references
INSERT INTO users (first_name, last_name, email, role_id, refresh_token)
SELECT 
    'User' || i,
    'Last' || i,
    'user' || i || '@example.com',
    role.id,  -- Use the role id from the roles table
    NULL
FROM 
    generate_series(1, 200) i
CROSS JOIN LATERAL (
    SELECT id
    FROM roles
    ORDER BY random()  -- Randomly pick a role from the roles table
    LIMIT 1
) role;

-- Populate Clients Table with proper user_id and company_id references
INSERT INTO clients (user_id, company_id, phone, created_at)
SELECT 
    client_user.id,                  -- Reference user_id from the users table where the role is 'Client'
    company.id,                       -- Reference company_id from the companies table
    '0700000' || client_user.id,      -- Generate phone number based on user_id
    CURRENT_TIMESTAMP                 -- Use the current timestamp for created_at
FROM 
    (SELECT id
     FROM users 
     WHERE role_id = 1  -- Only select users with role_id = 1 (Clients)
     ORDER BY random() 
     LIMIT 100) AS client_user   -- Select 100 random clients from the users table
CROSS JOIN LATERAL (
    SELECT id
    FROM companies
    ORDER BY random()  -- Randomly pick a company for the client
    LIMIT 1
) AS company;

-- Populate Technicians Table with proper user_id, company_id, and availability_id references
INSERT INTO technicians (user_id, company_id, phone, availability_id, created_at)
SELECT 
    user_data.id,                    -- Reference user_id from the users subquery
    company.id,                       -- Reference company_id from the companies table
    '0700000' || user_data.id,        -- Generate phone number based on user_id
    availability.id,                  -- Reference availability_id from the availability table
    CURRENT_TIMESTAMP                 -- Use the current timestamp for created_at
FROM 
    (SELECT id
     FROM users 
     WHERE role_id = 2 
     ORDER BY random() 
     LIMIT 200) AS user_data  -- Alias the subquery as user_data
CROSS JOIN LATERAL (
    SELECT id
    FROM companies
    ORDER BY random()  -- Randomly pick a company from the companies table
    LIMIT 1
) AS company
CROSS JOIN LATERAL (
    SELECT id
    FROM availability
    ORDER BY random()  -- Randomly pick an availability from the availability table
    LIMIT 1
) AS availability;

INSERT INTO sub_system_type_expertise (technician_id, sub_system_type_id)
SELECT tech.id, sub.id
FROM (SELECT id FROM technicians ORDER BY random() LIMIT 500) tech,
     (SELECT id FROM sub_system_types ORDER BY random() LIMIT 500) sub;

-- Populate Contracts Table with valid company references
INSERT INTO contracts (client_company_id, technician_company_id, created_at)
SELECT 
    client_company.id,              -- Reference client_company_id from the companies table
    technician_company.id,          -- Reference technician_company_id from the companies table
    CURRENT_TIMESTAMP               -- Use the current timestamp for created_at
FROM 
    (SELECT id
     FROM companies
     ORDER BY random() 
     LIMIT 100) AS client_company  -- Select 100 random companies for the client company
CROSS JOIN LATERAL (
    SELECT id
    FROM companies
    ORDER BY random()  -- Randomly pick a different company for the technician company
    LIMIT 1
) AS technician_company;

-- Populate Interventions Table with valid client_id, technician_id, level_id, and other references
INSERT INTO interventions (client_id, technician_id, name, level_id, description, location, client_signature, technician_signature, created_at)
SELECT 
    client.id,                      -- Reference client_id from the clients table
    technician.id,                  -- Reference technician_id from the technicians table
    'Intervention ' || i,           -- Generate unique intervention name
    urgency_level.id,               -- Reference level_id from the urgency_levels table
    'Description for intervention ' || i, -- Generate description for the intervention
    'Location ' || i,               -- Generate location for the intervention
    NULL,                           -- client_signature (NULL as placeholder)
    NULL,                           -- technician_signature (NULL as placeholder)
    CURRENT_TIMESTAMP               -- Use the current timestamp for created_at
FROM 
    generate_series(1, 100000) i
CROSS JOIN LATERAL (
    SELECT id
    FROM clients
    ORDER BY random()  -- Randomly pick a client from the clients table
    LIMIT 1
) AS client
CROSS JOIN LATERAL (
    SELECT id
    FROM technicians
    ORDER BY random()  -- Randomly pick a technician from the technicians table
    LIMIT 1
) AS technician
CROSS JOIN LATERAL (
    SELECT id
    FROM urgency_levels
    ORDER BY random()  -- Randomly pick an urgency level from the urgency_levels table
    LIMIT 1
) AS urgency_level;

-- Populate Invoices Table with valid contract_id, intervention_id, and other references
INSERT INTO invoices (contract_id, intervention_id, description, emmiting_date, created_at)
SELECT 
    contract.id,                     -- Reference contract_id from the contracts table
    intervention.id,                 -- Reference intervention_id from the interventions table
    'Invoice for intervention ' || i, -- Generate unique invoice description
    CURRENT_TIMESTAMP,               -- Use the current timestamp for emmiting_date
    CURRENT_TIMESTAMP                -- Use the current timestamp for created_at
FROM 
    generate_series(1, 50000) i
CROSS JOIN LATERAL (
    SELECT id
    FROM contracts
    ORDER BY random()  -- Randomly pick a contract from the contracts table
    LIMIT 1
) AS contract
CROSS JOIN LATERAL (
    SELECT id
    FROM interventions
    ORDER BY random()  -- Randomly pick an intervention from the interventions table
    LIMIT 1
) AS intervention;

