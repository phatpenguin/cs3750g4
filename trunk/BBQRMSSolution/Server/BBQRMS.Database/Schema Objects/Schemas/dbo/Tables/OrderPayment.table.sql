CREATE TABLE [dbo].[OrderPayment]
(
	Id			int				identity,
	OrderId		int				NOT NULL,
	Amount		decimal(18,10)	NOT NULL,
	PaymentType	int				NOT NULL,
	Memo		text
)
