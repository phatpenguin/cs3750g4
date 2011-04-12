CREATE TABLE [dbo].[OrderItem]
(
	Id			int				identity,
	OrderId		int				NOT NULL,
	MenuItemId	int				NOT NULL,
	Name		varchar(MAX)	NOT NULL,
	Quantity	int				NOT NULL,
	UnitPrice	numeric(18,2)	NOT NULL,
	UnitTax		numeric(18,2)	NOT NULL
)
