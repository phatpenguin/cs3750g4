CREATE TABLE [dbo].[OrderItem]
(
	Id			int				identity,
	OrderId		int				NOT NULL,
	MenuItemId	int				NOT NULL,
	Name		int				NOT NULL,
	Quantity	int				NOT NULL,
	UnitPrice	numeric(18,10)	NOT NULL,
	UnitTax		numeric(18,10)	NOT NULL
)
