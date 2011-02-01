ALTER TABLE [dbo].[OrderPayment]
	ADD CONSTRAINT [FK_OrderPayment_Order] 
	FOREIGN KEY (OrderId)
	REFERENCES "Order" (Id)	
