ALTER TABLE [dbo].[Order]
	ADD CONSTRAINT [FK_OrderStateId_Order] 
	FOREIGN KEY (OrderStateId)
	REFERENCES OrderState (Id)	