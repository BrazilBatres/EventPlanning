USE event_planning;
/*
INSERT INTO User_type (type) VALUES ('Seller');
INSERT INTO User_type (type) VALUES ('Buyer');
INSERT INTO User_type (type) VALUES ('Administrator');
*/
/*
INSERT INTO Product_category (category) VALUES ('Decorations');
INSERT INTO Product_category (category) VALUES ('Food and Drinks');
INSERT INTO Product_category (category) VALUES ('Music and Entertainment');
INSERT INTO Product_category (category) VALUES ('Transportation');
INSERT INTO Product_category (category) VALUES ('Photography and Video');
*/
INSERT INTO Users (password, email, first_name, last_name, user_type_id) 
VALUES ('password1', 'seller1@example.com', 'John', 'Doe', 1);

INSERT INTO Sellers (user_id, company_name, company_address, company_phone) 
VALUES (LAST_INSERT_ID(), 'ABC Decorations', '123 Main St', '12345678');

