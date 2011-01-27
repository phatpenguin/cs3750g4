ALTER TABLE [dbo].[ConsumedInventory]
	ADD CONSTRAINT [FK_MasterInventoryId_ConsumedInventory] 
	FOREIGN KEY (MasterInventoryId)
	REFERENCES MasterInventory (Id);

