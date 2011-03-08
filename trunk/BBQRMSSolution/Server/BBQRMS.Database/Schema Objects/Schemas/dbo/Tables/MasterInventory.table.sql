CREATE TABLE [dbo].[MasterInventory]
(
	Id				int	identity,
	Name			nvarchar(MAX) NOT NULL,
	UnitQty			int NOT NULL,
	ExpirationDate	datetime NOT NULL,
	OrderLeadDays	int	NOT NULL,
	IsActive		bit NOT NULL Default(1)
)
