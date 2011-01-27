ALTER TABLE [dbo].[ConsumedInventory]
	ADD CONSTRAINT [FK_ConsumptionTypeId_ConsumedInventory] 
	FOREIGN KEY (ConsumptionTypeId)
	REFERENCES [dbo].[ConsumptionType] (Id);

