CREATE TABLE [dbo].[SupplierContact]
(
	id			int identity,
	name		nvarchar(MAX) NOT NULL,
	phone1		nvarchar NOT NULL,
	phone2		nvarchar NULL,
	phone3		nvarchar NULL,
	email		nvarchar NULL
)
