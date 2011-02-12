CREATE TABLE [dbo].[Supplier]
(
	Id				int identity,
	Name			nvarchar(MAX) NOT NULL,
	Address		    nvarchar(MAX) NOT NULL,
	Phone			nvarchar(MAX) NOT NULL,
	State			nvarchar(MAX),
	Zip				nvarchar(MAX),
	PointOfContact  nvarchar(MAX),
	Rating			int	NULL
)
