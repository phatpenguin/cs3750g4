﻿CREATE TABLE [dbo].[Payment]
(
	Id				int				identity, 
	OrderId			int				NOT NULL,
	PaymentTypeId	int				NOT NULL,
	Amount			numeric(18,10)	NOT NULL,
	Memo			text
)
