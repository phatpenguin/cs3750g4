ALTER TABLE [dbo].[Order]
	ADD CONSTRAINT [FK_PaymentTypeId_Order] 
	FOREIGN KEY (PaymentTypeId)
	REFERENCES PaymentType (Id)	

