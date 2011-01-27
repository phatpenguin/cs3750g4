CREATE TABLE [dbo].[Supplier]
(
	Id				int identity,
	Name			nvarchar(MAX) NOT NULL,
	Address1		nvarchar NOT NULL,
	Phone1			nvarchar NOT NULL,
	Rating			int	NULL
)
