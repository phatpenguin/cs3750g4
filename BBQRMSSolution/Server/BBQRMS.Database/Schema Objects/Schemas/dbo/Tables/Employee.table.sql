CREATE TABLE [dbo].[Employee]
(
	id			int identity,
	firstName	nvarchar(MAX) NOT NULL,
	lastName	nvarchar(MAX) NOT NULL,
	hireDate	datetime not null,
	phone1		nvarchar,
	phone2		nvarchar,
	phone3		nvarchar, 
	address1	nvarchar(MAX) NULL,
	address2	nvarchar(MAX) NULL,
	email1		nvarchar(MAX) NULL,
	email2		nvarchar(MAX) NULL,
	payTypeId	int NOT NULL,
	payAmount	money NOT NULL
)
