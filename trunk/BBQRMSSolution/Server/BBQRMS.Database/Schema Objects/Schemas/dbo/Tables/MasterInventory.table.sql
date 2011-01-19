CREATE TABLE [dbo].[MasterInventory]
(
	id				int	identity,
	name			nvarchar NOT NULL,
	unitQty			int NOT NULL,
	expirationDate	datetime NOT NULL,
	orderLeadDays	int	NOT NULL
)
