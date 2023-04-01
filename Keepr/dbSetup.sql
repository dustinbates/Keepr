CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture',
  coverImg varchar(255) COMMENT 'User Cover Image'
) default charset utf8 COMMENT '';

DROP TABLE accounts;


-- Section Keeps
CREATE TABLE keep(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  name VARCHAR(255) NOT NULL COMMENT 'Keep Name',
  description VARCHAR(1000) NOT NULL COMMENT 'Keep Description',
  img VARCHAR(1000) NOT NULL COMMENT 'Keep Image',
  views INT NOT NULL DEFAULT 0 COMMENT 'Number of Views',

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE keep;

INSERT INTO keep
(name, description, img, creatorId)
VALUES
("Dog", "It's just a dog", 'https://hips.hearstapps.com/hmg-prod/images/golden-retriever-royalty-free-image-506756303-1560962726.jpg?crop=1.00xw:0.756xh;0,0.0756xh&resize=1200:*', '6414d2061c38166fcc365e1d');

-- Section Vaults
CREATE TABLE vault(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  name VARCHAR(255) NOT NULL COMMENT 'Vault Name',
  description VARCHAR(255) NOT NULL DEFAULT 'No Description' COMMENT 'Vault Description',
  img VARCHAR(1000) NOT NULL DEFAULT 'https://www.corporatechallenge.com.au/wp-content/uploads/2020/09/Vault-Hero.png' COMMENT 'Vault Image',
  isPrivate BOOLEAN NOT NULL DEFAULT false,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE vault;

INSERT INTO vault
(name, description, creatorId, isPrivate)
VALUES
("Cool Stuff", "Lots of cool in here guys", '6414d2061c38166fcc365e1d', true);

-- Section VaultKeeps
CREATE TABLE vaultKeep(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Creator Id',
  vaultId INT NOT NULL COMMENT 'Vault Id',
  keepId INT NOT NULL COMMENT 'Keep Id',

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vault(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keep(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE vaultKeep;

INSERT INTO vaultKeep
(creatorId, vaultId, keepId)
VALUES
('6414d2061c38166fcc365e1d', 3, 2)