CREATE TABLE [dbo].[Suppliers]
(
	id				int identity,
	name			nvarchar(MAX) NOT NULL,
	address1		nvarchar NOT NULL,
	phone1			nvarchar NOT NULL,
	rating			int	NULL
)
