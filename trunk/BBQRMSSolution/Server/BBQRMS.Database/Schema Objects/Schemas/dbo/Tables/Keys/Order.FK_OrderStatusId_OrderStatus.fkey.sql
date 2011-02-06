ALTER TABLE [dbo].[Order]
	ADD CONSTRAINT [FK_OrderStatusId_Order] 
	FOREIGN KEY (OrderStatusId)
	REFERENCES OrderStatus (Id)	