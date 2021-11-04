CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'Keep Name',
  description VARCHAR(255) COMMENT 'Keep Description',
  img VARCHAR(255) COMMENT 'Keep Image',
  views INT NOT NULL COMMENT "Views for this keep",
  shares INT NOT NULL COMMENT "number of shares for this keep",
  keeps INT NOT NULL COMMENT "number of times this has been kept",
  creatorId VARCHAR(255) NOT NULL COMMENT 'primary key',
  -- NOTE it says there needs to be a profile creator here, I think that would be handled by having the creator Id
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'Keep Name',
  description VARCHAR(255) COMMENT 'Vault Description',
  isPrivate TINYINT COMMENT 'Bool value for private',
  creatorId VARCHAR(255) NOT NULL COMMENT 'primary key',
  -- NOTE it says there needs to be a profile creator here, I think that would be handled by having the creator Id
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'primary key',
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE keeps;
DROP Table accounts;
DROP TABLE vaults;
DROP TABLE vaultkeeps;

SELECT * FROM vaultkeeps;




