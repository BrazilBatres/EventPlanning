/*CREATE DATABASE event_planning;*/

USE event_planning;
DROP TABLE IF exists Event_Products;
DROP TABLE IF exists Event_Services;
DROP TABLE IF exists Products;
DROP TABLE IF exists Services;
DROP TABLE IF exists Events;
DROP TABLE IF exists Sellers;
DROP TABLE IF exists Buyers;
DROP TABLE IF exists Administrators;
DROP TABLE IF exists Users;
DROP TABLE IF exists User_type;
DROP TABLE IF exists Service_category;
DROP TABLE IF exists Product_category;

CREATE TABLE User_type (
  id INT NOT NULL AUTO_INCREMENT,
  type VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Service_category (
  id INT NOT NULL AUTO_INCREMENT,
  category VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Product_category (
  id INT NOT NULL AUTO_INCREMENT,
  category VARCHAR(50) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Users (
  id INT NOT NULL AUTO_INCREMENT,
  first_name VARCHAR(50) NOT NULL,
  last_name VARCHAR(50) NOT NULL,
  email VARCHAR(100) NOT NULL,
  password VARCHAR(100) NOT NULL,
  user_type_id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (user_type_id) REFERENCES User_type(id)
);

CREATE TABLE Sellers (
id INT NOT NULL AUTO_INCREMENT,
  user_id INT NOT NULL,
  company_name VARCHAR(100) NOT NULL,
  company_address VARCHAR(100) NOT NULL,
  company_phone VARCHAR(20) NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (user_id) REFERENCES Users(id)
);

CREATE TABLE Buyers (
id INT NOT NULL AUTO_INCREMENT,
  user_id INT NOT NULL,
  shipping_address VARCHAR(100) NOT NULL,
  contact_phone VARCHAR(20) NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (user_id) REFERENCES Users(id)
);

CREATE TABLE Administrators (
id INT NOT NULL AUTO_INCREMENT,
  user_id INT NOT NULL,
  start_date DATE NOT NULL,
  end_date DATE NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (user_id) REFERENCES Users(id)
);

CREATE TABLE Products (
  id INT NOT NULL AUTO_INCREMENT,
  seller_id INT NOT NULL,
  product_name VARCHAR(100) NOT NULL,
  product_description VARCHAR(1000) NOT NULL,
  product_price DECIMAL(10,2) NOT NULL,
  product_category_id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (seller_id) REFERENCES Sellers(id),
  FOREIGN KEY (product_category_id) REFERENCES Product_category(id)
);

CREATE TABLE Services (
  id INT NOT NULL AUTO_INCREMENT,
  seller_id INT NOT NULL,
  service_name VARCHAR(100) NOT NULL,
  service_description VARCHAR(1000) NOT NULL,
  service_price DECIMAL(10,2) NOT NULL,
  service_category_id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (seller_id) REFERENCES Sellers(id),
  FOREIGN KEY (service_category_id) REFERENCES Service_category(id)
);

CREATE TABLE Events (
  id INT NOT NULL AUTO_INCREMENT,
  buyer_id INT NOT NULL,
  event_name VARCHAR(100) NOT NULL,
  event_date DATE NOT NULL,
  event_location VARCHAR(100) NOT NULL,
  event_description VARCHAR(1000) NOT NULL,
  event_budget DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (buyer_id) REFERENCES Buyers(id)
);

CREATE TABLE Event_Products (
  event_id INT NOT NULL,
  product_id INT NOT NULL,
  quantity INT NOT NULL,
  PRIMARY KEY (event_id, product_id),
  FOREIGN KEY (event_id) REFERENCES Events(id),
  FOREIGN KEY (product_id) REFERENCES Products(id)
);

CREATE TABLE Event_Services (
  event_id INT NOT NULL,
  service_id INT NOT NULL,
  PRIMARY KEY (event_id, service_id),
  FOREIGN KEY (event_id) REFERENCES Events(id),
  FOREIGN KEY (service_id) REFERENCES Services(id)
);
