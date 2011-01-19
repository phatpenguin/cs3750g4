CREATE TABLE [dbo].[ConsumedInventory]
(
	id					int	identity,
	masterInventoryId	int	NOT NULL,
	quantity			money NOT NULL,
	consumptionTypeId	int	NOT NULL,
	dateConsumed		dateTime NOT NULL,
	employeeId			int NOT NULL
)
