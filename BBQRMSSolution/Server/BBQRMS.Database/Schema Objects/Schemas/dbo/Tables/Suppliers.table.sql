CREATE TABLE [dbo].[Supplier]
(
	Id				int identity,
	Name			nvarchar(MAX) NOT NULL,
	Address1		nvarchar(MAX) NOT NULL,
	Phone1			nvarchar(MAX) NOT NULL,
	Rating			int	NULL
)
