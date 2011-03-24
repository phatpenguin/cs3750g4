CREATE TABLE [dbo].[MasterInventory]
(
	Id				int	identity(1,1),
	Name			nvarchar(MAX) NOT NULL,
	LocationId		int not NULL,
	UnitQty			int NOT NULL,
	ExpirationDate	datetime NOT NULL,
	OrderLeadDays	int	NOT NULL,
	IsActive		bit NOT NULL Default(1)
)
