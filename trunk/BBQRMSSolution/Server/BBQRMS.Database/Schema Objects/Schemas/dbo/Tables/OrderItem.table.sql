CREATE TABLE [dbo].[OrderItem]
(
	Id			int identity,
	MenuItemId	int	NOT NULL,
	OrderId		int NOT NULL,
	Quantity	int	NOT NULL,
	UnitPrice	int NOT NULL
)
