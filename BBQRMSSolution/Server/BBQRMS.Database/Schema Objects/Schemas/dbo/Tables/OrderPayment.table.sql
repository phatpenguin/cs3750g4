CREATE TABLE [dbo].[OrderPayment]
(
	Id			int				identity,
	Amount		decimal(18,10)	NOT NULL,
	AuthNum		varchar(max),
	PaymentType	int				NOT NULL,
	OrderId		int				NOT NULL
)
