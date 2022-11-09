Create Database BloodBankDB
 use BloodBankDB
 CREATE TABLE BloodBank 
( 
BankID INT IDENTITY (1, 1) NOT NULL,
BankName NVARCHAR (50) NULL,
 PRIMARY KEY CLUSTERED (BankID ASC)
 ); 
 Select*from BloodBank;

CREATE TABLE dbo.Donor
( 
DonorID INT IDENTITY (1, 1) NOT NULL, 
DonorName NVARCHAR (50) NULL, 
DonationDate DATETIME NULL, 
Gender NVARCHAR (10) NULL, 
FilePath NVARCHAR (500) NULL, 
BankID INT REFERENCES BloodBank (BankID), 
PRIMARY KEY CLUSTERED (DonorID ASC) 
);
