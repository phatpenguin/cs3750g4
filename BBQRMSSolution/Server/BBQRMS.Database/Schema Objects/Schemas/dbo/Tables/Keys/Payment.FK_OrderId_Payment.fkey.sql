ALTER TABLE [dbo].[Payment]
	ADD CONSTRAINT [FK_OrderId_Payment] 
	FOREIGN KEY (OrderId)
	REFERENCES "Order" (Id)