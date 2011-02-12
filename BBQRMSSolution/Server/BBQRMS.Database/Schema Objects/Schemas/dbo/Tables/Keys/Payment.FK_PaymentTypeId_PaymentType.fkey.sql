ALTER TABLE [dbo].[Payment]
	ADD CONSTRAINT [FK_PaymentTypeId_Payment] 
	FOREIGN KEY (PaymentTypeId)
	REFERENCES PaymentType (Id)