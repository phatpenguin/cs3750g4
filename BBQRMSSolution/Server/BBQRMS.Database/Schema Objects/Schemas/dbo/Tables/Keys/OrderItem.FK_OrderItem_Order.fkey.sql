ALTER TABLE [dbo].[OrderItem]
	ADD CONSTRAINT [FK_OrderItem_Order] 
	FOREIGN KEY (OrderId)
	REFERENCES "Order" (Id)	
