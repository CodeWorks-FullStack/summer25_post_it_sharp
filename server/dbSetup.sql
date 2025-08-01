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


SELECT
albums.id, albums.title,
COUNT(watchers.id) AS watcherCount
FROM albums
LEFT JOIN watchers ON watchers.album_id = albums.id
GROUP BY albums.id
;

  SELECT 
        albums.*,
        COUNT(watchers.id) AS watcherCount,
        accounts.*
    FROM albums
    JOIN accounts ON albums.creator_id = accounts.id
    LEFT JOIN watchers ON watchers.album_id = albums.id
    GROUP BY albums.id
    ORDER BY albums.created_at ASC;


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

-- 👀 WATCHERS

CREATE TABLE watchers(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  album_id INT NOT NULL,
  account_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (account_id) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (album_id) REFERENCES albums(id) ON DELETE CASCADE
) default charset utf8mb4 COMMENT '';


SELECT * FROM watchers where album_id = 500;

SELECT
      watchers.id AS watcherId,
      watchers.account_id,
      albums.*
    FROM watchers
    JOIN albums ON watchers.album_id = albums.id
    WHERE account_id = '645d75fdfdcb015333f9b0ba';

SELECT
  watchers.id AS watcherId,
  watchers.account_id,
  albums.*,
  accounts.*
FROM watchers
JOIN albums ON watchers.album_id = albums.id
JOIN accounts ON albums.creator_id = accounts.id
WHERE account_id = '645d75fdfdcb015333f9b0ba';

INSERT INTO watchers (album_id, account_id) VALUES (2, '645d75fdfdcb015333f9b0ba');
