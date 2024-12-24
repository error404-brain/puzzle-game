CREATE DATABASE puzzleGame;
USE puzzleGame;

CREATE TABLE User (
    id INT AUTO_INCREMENT PRIMARY KEY,           
    username VARCHAR(50) NOT NULL UNIQUE,        
    user_password VARCHAR(255) NOT NULL         
);

-- Tạo bảng Point
CREATE TABLE Point (
    id INT AUTO_INCREMENT PRIMARY KEY,         
    user_id INT NOT NULL,                      
    points INT NOT NULL DEFAULT 0,             
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES User(id)    
);
