CREATE TABLE [dbo].[Order]
(
	Id	int	identity,
	PaymentTypeId	int NOT NULL,
	LocationId		int NOT NULL,
	OrderTypeId		int NOT NULL,
	Total			decimal(18,10) NOT NULL
)
