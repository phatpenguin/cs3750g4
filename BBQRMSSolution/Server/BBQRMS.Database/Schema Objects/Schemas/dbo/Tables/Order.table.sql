CREATE TABLE [dbo].[Order]
(
	Id	int	identity,
	OrderItemId		int NOT NULL,
	PaymentTypeId	int NOT NULL,
	LocationId		int NOT NULL,
	OrderTypeId		int NOT NULL,
	TableId			int NOT NULL,
	TableSectionId	int	NOT NULL,
	Total			money NOT NULL,
	OrderRecieved	int	NOT NULL,
	EnteredTime		datetime NOT NULL,
	CompletedType	datetime NULL
)
