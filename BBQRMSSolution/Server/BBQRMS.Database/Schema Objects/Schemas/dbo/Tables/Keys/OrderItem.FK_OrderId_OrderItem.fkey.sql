ALTER TABLE [dbo].[OrderItem]
	ADD CONSTRAINT [FK_OrderId_OrderItem] 
	FOREIGN KEY (OrderId)
	REFERENCES [Order] (Id)	

