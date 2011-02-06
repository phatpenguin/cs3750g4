ALTER TABLE [dbo].[Order]
	ADD CONSTRAINT [FK_OrderTypeId_Order] 
	FOREIGN KEY (OrderTypeId)
	REFERENCES OrderType (Id)	