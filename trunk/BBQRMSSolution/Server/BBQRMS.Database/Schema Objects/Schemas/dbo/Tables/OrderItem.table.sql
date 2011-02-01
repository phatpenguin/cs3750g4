CREATE TABLE [dbo].[OrderItem]
(
	Id			int identity,
	Name	int	NOT NULL,
	Quantity	int	NOT NULL,
	UnitPrice	int NOT NULL,
	TaxAmt		numeric(18,10)
)
