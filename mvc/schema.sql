
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the CapstoneDB Database (IF EXISTS)
DROP DATABASE IF EXISTS CapstoneDB
GO

-- Create a new CapstoneDB Database
CREATE DATABASE CapstoneDB;
GO

-- Switch to the CapstoneDB Database
USE CapstoneDB
GO

BEGIN TRANSACTION;

CREATE TABLE users
(
	id			int			identity(1,1),
	username	varchar(50)	not null,
	password	varchar(50)	not null,
	salt		varchar(50)	not null,
	role		varchar(50)	default('user'),

	constraint pk_users primary key (id)
);


COMMIT TRANSACTION;