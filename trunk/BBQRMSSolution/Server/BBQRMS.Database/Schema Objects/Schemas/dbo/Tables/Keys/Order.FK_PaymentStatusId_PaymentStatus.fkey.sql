ALTER TABLE [dbo].[Order]
	ADD CONSTRAINT [FK_PaymentStatusId_Order] 
	FOREIGN KEY (PaymentStatusId)
	REFERENCES PaymentStatus (Id)	