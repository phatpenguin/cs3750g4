CREATE TABLE [dbo].[Employee]
(
	Id			int identity,
	FirstName	nvarchar(MAX) NOT NULL,
	LastName	nvarchar(MAX) NOT NULL,
	HireDate	datetime not null,
	Phone1		nvarchar,
	Phone2		nvarchar,
	Phone3		nvarchar, 
	Address1	nvarchar(MAX) NULL,
	Address2	nvarchar(MAX) NULL,
	Email1		nvarchar(MAX) NULL,
	Email2		nvarchar(MAX) NULL,
	PayTypeId	int NOT NULL,
	PayAmount	money NOT NULL
)
