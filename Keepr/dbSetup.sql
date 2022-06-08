CREATE TABLE IF NOT EXISTS accounts(
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
    id INT AUTO_INCREMENT primary key COMMENT 'primary key',
    name TEXT NOT NULL,
    description TEXT NOT NULL,
    isPrivate BOOLEAN,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;

CREATE TABLE IF NOT EXISTS keeps(
    id INT AUTO_INCREMENT primary key COMMENT 'primary key',
    name TEXT NOT NULL,
    description TEXT NOT NULL,
    img TEXT NOT NULL,
    views INT,
    kept INT,
    vaultKeepId INT,
    creatorId VARCHAR(255) NOT NULL,
    -- FOREIGN KEY (vaultKeepId) REFERENCES vaultKeeps(id),
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;

CREATE TABLE IF NOT EXISTS vaultKeeps(
    id INT AUTO_INCREMENT primary key COMMENT 'primary key',
    vaultId INT NOT NULL,
    keepId INT NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
    FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;

DROP TABLE accounts;

DROP TABLE keeps;

DROP TABLE vaults;

DROP TABLE vaultKeeps;

INSERT INTO
    keeps (
        name,
        description,
        img,
        views,
        kept,
        creatorId
    )
VALUES
    (
        "dog",
        "good floofy dog",
        "https://thiscatdoesnotexist.com",
        0,
        0,
        "62703c3e7700bb6623dd8a72"
    );

INSERT INTO
    keeps (name, description, img, views, kept, creatorId)
VALUES
    (
        'test',
        'test',
        'test',
        0,
        0,
        '62703c3e7700bb6623dd8a72'
    );

SELECT LAST_INSERT_ID();