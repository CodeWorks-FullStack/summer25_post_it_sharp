CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

-- 📓ALBUMS
CREATE TABLE albums(
id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
title TINYTEXT NOT NULL,
description VARCHAR(1000),
cover_img VARCHAR(500),
archived BOOLEAN NOT NULL DEFAULT false,
category ENUM('aesthetics', 'games', 'animals', 'food', 'vibes', 'misc'),
creator_id VARCHAR(255) NOT NULL,
FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';

SELECT 
albums.*,
accounts.*
FROM albums
JOIN accounts ON albums.creator_id = accounts.id
WHERE albums.id = 4;

-- 🖼️PICTURES
CREATE TABLE pictures(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  img_url VARCHAR(500) NOT NULL,
  creator_id VARCHAR(255) NOT NULL,
  album_id int NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (album_id) REFERENCES albums(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';