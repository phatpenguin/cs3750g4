CREATE TABLE [dbo].[ConsumedInventory]
(
	Id					int	identity,
	MasterInventoryId	int	NOT NULL,
	Quantity			numeric(18,2) NOT NULL,
	ConsumptionTypeId	int	NOT NULL,
	DateConsumed		datetime NOT NULL,
	EmployeeId			int NOT NULL,
	ExcludeFromDailyShopping bit NOT NULL default(0)
)
