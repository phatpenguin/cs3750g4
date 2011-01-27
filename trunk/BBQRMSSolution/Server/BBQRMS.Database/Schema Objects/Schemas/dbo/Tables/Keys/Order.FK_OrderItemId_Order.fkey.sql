ALTER TABLE [dbo].[Order]
	ADD CONSTRAINT [FK_OrderItemId_Order] 
	FOREIGN KEY (OrderItemId)
	REFERENCES OrderItem (Id)	

