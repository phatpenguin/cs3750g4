CREATE TABLE [dbo].[SupplierContact]
(
	Id			int identity,
	Name		nvarchar(MAX) NOT NULL,
	Phone1		nvarchar NOT NULL,
	Phone2		nvarchar NULL,
	Phone3		nvarchar NULL,
	Email		nvarchar NULL
)
