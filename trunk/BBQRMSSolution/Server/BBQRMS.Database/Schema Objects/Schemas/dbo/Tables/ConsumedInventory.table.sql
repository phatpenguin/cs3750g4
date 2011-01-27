CREATE TABLE [dbo].[ConsumedInventory]
(
	Id					int	identity,
	MasterInventoryId	int	NOT NULL,
	Quantity			money NOT NULL,
	ConsumptionTypeId	int	NOT NULL,
	DateConsumed		dateTime NOT NULL,
	EmployeeId			int NOT NULL
)
