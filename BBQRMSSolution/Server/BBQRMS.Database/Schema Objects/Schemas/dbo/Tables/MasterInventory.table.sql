CREATE TABLE [dbo].[MasterInventory]
(
	Id				int	identity,
	Name			nvarchar NOT NULL,
	UnitQty			int NOT NULL,
	ExpirationDate	datetime NOT NULL,
	OrderLeadDays	int	NOT NULL
)
