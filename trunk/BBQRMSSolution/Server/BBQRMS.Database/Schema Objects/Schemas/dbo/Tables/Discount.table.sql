CREATE TABLE [dbo].[Discount]
(
	Id				int		not null	identity,
	Amount			decimal	not null,
	OrderId			int		not null,
	DiscountTypeId	int		not null
)
