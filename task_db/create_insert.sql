-- Create Tables
CREATE TABLE client (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    CUI_CNP VARCHAR(255),
    email VARCHAR(255),
    password VARCHAR(255)
);

CREATE TABLE offer (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255),
    description TEXT,
    validity_period DATE,
    creation_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    client_company VARCHAR(255)
);

CREATE TABLE offer_device (
    id SERIAL PRIMARY KEY,
    offer_id INT REFERENCES offer(id) ON DELETE CASCADE,
    name VARCHAR(255),
    quantity INT,
    price DECIMAL(10,2),
    category VARCHAR(255),
    sub_category VARCHAR(255),
    system_type VARCHAR(255)
);

CREATE TABLE offer_maintenance_system (
    id SERIAL PRIMARY KEY,
    offer_id INT REFERENCES offer(id) ON DELETE CASCADE,
    name VARCHAR(255),
    quantity INT,
    price DECIMAL(10,2)
);

CREATE TABLE contract (
    id SERIAL PRIMARY KEY,
    offer_id INT REFERENCES offer(id) ON DELETE CASCADE,
    start_date DATE,
    end_date DATE,
    free_interventions BOOLEAN,
    signatures TEXT
);

CREATE TABLE technician (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    email VARCHAR(255),
    password VARCHAR(255),
    phone_number VARCHAR(255),
    signature TEXT,
    status VARCHAR(255)
);

CREATE TABLE intervention_document (
    id SERIAL PRIMARY KEY,
    client_id INT REFERENCES client(id) ON DELETE CASCADE,
    technician_id INT REFERENCES technician(id) ON DELETE SET NULL,
    work_point_address TEXT,
    contact_person VARCHAR(255),
    intervention_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(255),
    time_interval TEXT
);

CREATE TABLE invoice (
    id SERIAL PRIMARY KEY,
    issuing_company VARCHAR(255),
    client_company VARCHAR(255),
    subtotal DECIMAL(10,2),
    total DECIMAL(10,2),
    issue_date DATE,
    payment_due DATE
);

CREATE TABLE invoice_item (
    id SERIAL PRIMARY KEY,
    invoice_id INT REFERENCES invoice(id) ON DELETE CASCADE,
    name VARCHAR(255),
    quantity INT,
    unit_price DECIMAL(10,2),
    total_price DECIMAL(10,2),
    vat DECIMAL(10,2)
);

CREATE TABLE client_contact (
    id SERIAL PRIMARY KEY,
    client_id INT REFERENCES client(id) ON DELETE CASCADE,
    contact_name VARCHAR(255)
);

-- Bulk Insert Script for Dummy Data
INSERT INTO offer (name, description, validity_period, client_company) 
SELECT 'Offer ' || i, 'Description ' || i, CURRENT_DATE + (i % 30), 'Company ' || i
FROM generate_series(1, 100000) i;

INSERT INTO offer_device (offer_id, name, quantity, price, category, sub_category, system_type)
SELECT i, 'Device ' || i, (random() * 10)::INT + 1, (random() * 1000)::DECIMAL, 'Category ' || (i % 5), 'Subcategory ' || (i % 3), 'System ' || (i % 4)
FROM generate_series(1, 100000) i;

INSERT INTO offer_maintenance_system (offer_id, name, quantity, price)
SELECT i, 'Maintenance System ' || i, (random() * 5)::INT + 1, (random() * 500)::DECIMAL
FROM generate_series(1, 100000) i;

INSERT INTO contract (offer_id, start_date, end_date, free_interventions, signatures)
SELECT id, CURRENT_DATE, CURRENT_DATE + 365, random() > 0.5, 'Signature ' || id
FROM offer;

INSERT INTO client (name, CUI_CNP, email, password)
SELECT 'Client ' || i, 'CUI' || i, 'client' || i || '@email.com', 'password' || i
FROM generate_series(1, 100000) i;

INSERT INTO technician (first_name, last_name, email, password, phone_number, signature, status)
SELECT 'TechFirst' || i, 'TechLast' || i, 'tech' || i || '@email.com', 'password' || i, '0700000' || i, 'Signature ' || i, 'Available'
FROM generate_series(1, 100000) i;

INSERT INTO intervention_document (client_id, technician_id, work_point_address, contact_person, intervention_date, status, time_interval)
SELECT (random() * 100000)::INT + 1, (random() * 100000)::INT + 1, 'Address ' || i, 'Contact ' || i, CURRENT_TIMESTAMP, 'New Request', '09:00-10:00'
FROM generate_series(1, 100000) i;

INSERT INTO invoice (issuing_company, client_company, subtotal, total, issue_date, payment_due)
SELECT 'Issuer ' || i, 'Client ' || i, (random() * 10000)::DECIMAL, (random() * 12000)::DECIMAL, CURRENT_DATE, CURRENT_DATE + 30
FROM generate_series(1, 100000) i;

INSERT INTO invoice_item (invoice_id, name, quantity, unit_price, total_price, vat)
SELECT i, 'Product ' || i, (random() * 10)::INT + 1, (random() * 500)::DECIMAL, (random() * 5000)::DECIMAL, (random() * 100)::DECIMAL
FROM generate_series(1, 100000) i;

INSERT INTO client_contact (client_id, contact_name)
SELECT i, 'Contact Person ' || i
FROM generate_series(1, 100000) i;
