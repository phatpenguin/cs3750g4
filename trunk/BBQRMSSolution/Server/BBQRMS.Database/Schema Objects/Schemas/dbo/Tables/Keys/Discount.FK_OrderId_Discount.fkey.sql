ALTER TABLE [dbo].[Discount]
	ADD CONSTRAINT [FK_OrderId_Discount] 
	FOREIGN KEY (OrderId)
	REFERENCES "Order" (Id)