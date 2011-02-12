ALTER TABLE [dbo].[Order]
	ADD CONSTRAINT [FK_PaymentStateId_Order] 
	FOREIGN KEY (PaymentStateId)
	REFERENCES PaymentState (Id)	