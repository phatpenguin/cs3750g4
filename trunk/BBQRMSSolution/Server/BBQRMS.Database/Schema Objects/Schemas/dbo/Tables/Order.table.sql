CREATE TABLE [dbo].[Order]
(
	id	int	identity,
	orderItemId		int NOT NULL,
	paymentTypeId	int NOT NULL,
	locationId		int NOT NULL,
	orderTypeId		int NOT NULL,
	tableId			int NOT NULL,
	tableSectionId	int	NOT NULL,
	total			money NOT NULL,
	orderRecieved	int	NOT NULL,
	enteredTime		datetime NOT NULL,
	completedType	datetime NULL
)
