CREATE TABLE [dbo].[SupplierContact]
(
	Id			int identity,
	Name		nvarchar(MAX) NOT NULL,
	Phone1		nvarchar(MAX) NOT NULL,
	Phone2		nvarchar(MAX) NULL,
	Phone3		nvarchar(MAX) NULL,
	Email		nvarchar(MAX) NULL
)
