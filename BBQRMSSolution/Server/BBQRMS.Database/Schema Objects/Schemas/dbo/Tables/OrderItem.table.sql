CREATE TABLE [dbo].[OrderItem]
(
	id			int identity,
	menuItemId	int	NOT NULL,
	orderId		int NOT NULL,
	quantity	int	NOT NULL,
	unitPrice	int NOT NULL
)
