DROP DATABASE IF EXISTS gasStation;

CREATE DATABASE gasStation;
USE gasStation;

CREATE TABLE Firm (
    firm_id INT PRIMARY KEY AUTO_INCREMENT,
    firm_name VARCHAR(100) NOT NULL UNIQUE,
    firm_address VARCHAR(255) NOT NULL UNIQUE,
    firm_phone VARCHAR(11)  NOT NULL UNIQUE
);

INSERT INTO Firm (firm_name, firm_address, firm_phone)
VALUES 
    ('Белнефтехим', 'Минск, ул. Притыцкого, 25', '70123456789'),
    ('Славнефть', 'Гродно, ул. Ленина, 10', '70222113344'),
    ('А-100', 'Брест, пр. Московский, 5', '70333221100'),
    ('Лукойл', 'Витебск, пр. Ленинский, 100', '70443322111');


CREATE TABLE Customer (
    card_account INT PRIMARY KEY AUTO_INCREMENT,
    customer_full_name VARCHAR(100) NOT NULL,
    customer_address VARCHAR(255),
    customer_phone VARCHAR(11) UNIQUE
);

ALTER TABLE Customer AUTO_INCREMENT = 100000;

INSERT INTO Customer (customer_full_name, customer_address, customer_phone)
VALUES 
    ('Иванов Иван Иванович', 'Минск, ул. Пушкина, 10', '79123456789'),
    ('Петров Петр Петрович', 'Гродно, ул. Лермонтова, 5', '79234567890'),
    ('Козлова Анна Сергеевна', 'Брест, пр. Гагарина, 15', '79345678901'),
    ('Куприянова Мария Николаева', 'Владивосток, ул. Красноармейская, 20', '79456789012'),
	('Полежаева Антонина Валерьевна', 'Витебск, ул. Толстого, 20', '79456709012'),
	('Михеев Алексей Иванович', 'Витебск, ул. Толстого, 20', '79456789615'),
	('Сапунов Николай Олегович', 'Находка, ул. Быстрых, 3', '79456789516'),
	('Киреев Владислав Романович', 'Керчь, ул. Ленина, 100', '79456789417');


CREATE TABLE GasStation (
    gasStation_id INT PRIMARY KEY AUTO_INCREMENT,
    gasStation_address VARCHAR(255) UNIQUE,
    firm_id INT,
    FOREIGN KEY (firm_id) REFERENCES Firm(firm_id) ON DELETE CASCADE
);

ALTER TABLE GasStation AUTO_INCREMENT = 1000;
 
INSERT INTO GasStation (gasStation_address, firm_id)
VALUES 
    ('Минск, ул. Жукова, 25', 1), 
    ('Минск, ул. Строителей, 10', 1), 
    ('Владивосток, ул. Ломателей, 11', 1), 
    ('Гродно, ул. Канифолина, 12', 1),
    ('Находка, пр. Гагарина, 15', 2), 
    ('Гродно, пр. Быстрых, 19', 2), 
    ('Гродно, ул. Пушкинская, 5', 2), 
    ('Брест, ул. Толстого, 20', 3), 
    ('Керчь, ул. Ленина, 30', 4), 
    ('Москва, пр. Мира, 50', 4), 
    ('Ленинград, пр. Мира, 50', 4), 
    ('Вологда, пр. Мира, 50', 4); 


CREATE TABLE Fuel (
    fuel_id INT PRIMARY KEY AUTO_INCREMENT,
    fuel_type VARCHAR(10) NOT NULL,
    fuel_unit VARCHAR(10) NOT NULL DEFAULT 'ЛИТР',
    fuel_price DECIMAL(5,2) NOT NULL,
    firm_id INT,
    FOREIGN KEY (firm_id) REFERENCES Firm(firm_id)  ON DELETE CASCADE
);

ALTER TABLE Fuel AUTO_INCREMENT = 100;

INSERT INTO Fuel (fuel_type, fuel_price, firm_id)
VALUES 
    ('АИ-76', 48, 1), 
    ('АИ-92', 59.73, 1), 
    ('АИ-95', 61.93, 1), 
    ('АИ-96', 63.73, 1), 
    ('АИ-92', 55.30, 2), 
    ('АИ-95', 59.43, 2),  
    ('Дизель', 76.00, 2), 
    ('Газ', 34.50, 2), 
    ('АИ-92', 76.40, 3), 
    ('АИ-95', 89.40, 3), 
    ('АИ-76', 43.90, 4), 
    ('АИ-92', 49.40, 4),  
    ('АИ-95', 52.99, 4), 
    ('АИ-96', 58.99, 4), 
    ('Дизель', 72.30, 4), 
    ('Газ', 24.50, 4); 

CREATE TABLE FuelSale (
	sale_id INT UNIQUE KEY AUTO_INCREMENT,
    sale_date DATETIME NOT NULL DEFAULT NOW(),
    card_account INT,
    gasStation_id INT,
    fuel_id INT,
    quantity DECIMAL(5, 2),
    FOREIGN KEY (card_account) REFERENCES Customer(card_account) ON DELETE CASCADE,
    FOREIGN KEY (gasStation_id) REFERENCES GasStation(gasStation_id)  ON DELETE CASCADE,
    FOREIGN KEY (fuel_id) REFERENCES Fuel(fuel_id)  ON DELETE CASCADE
);

ALTER TABLE FuelSale AUTO_INCREMENT = 500000;

INSERT INTO FuelSale (card_account, gasStation_id, fuel_id, quantity)
VALUES 
    (100000, 1000, 100, 23.00),
    (100000, 1000, 101, 45.25),
    (100000, 1001, 102, 60.75),
    (100000, 1001, 103, 40.00),
    (100000, 1005, 104, 45.25),
    (100001, 1002, 102, 25.75),
    (100001, 1002, 103, 25.50),
    (100001, 1003, 100, 25.20),
    (100001, 1004, 105, 35.00),
    (100001, 1005, 105, 15.90),
    (100002, 1003, 101, 20.00),
    (100002, 1004, 106, 20.00),
    (100002, 1004, 107, 25.90),
	(100002, 1005, 106, 30.50),
    (100002, 1004, 106, 13.25),
    (100003, 1005, 106, 30.75),
    (100003, 1006, 104, 23.00),
    (100003, 1006, 104, 39.25),
    (100004, 1007, 108, 42.50),
    (100004, 1007, 109, 24.50),
    (100004, 1008, 110, 12.75),
    (100004, 1008, 111, 64.75),
    (100005, 1008, 112, 34.75),
    (100006, 1010, 115, 29.25),
    (100006, 1009, 115, 25.50),
    (100006, 1010, 114, 35.50),
    (100006, 1009, 113, 15.50),
    (100006, 1011, 112, 15.50);
    
    
CREATE VIEW user_firm_view AS
SELECT 
    card_account AS username,
    '111' AS password,
    'customer_role' AS role
FROM customer
UNION
SELECT 
    firm_name AS username,
    '111' AS password,
    'firm_role' AS role
FROM firm
UNION
SELECT
    'admin' AS username,
    'admin' AS password,
    'administrator' AS role;