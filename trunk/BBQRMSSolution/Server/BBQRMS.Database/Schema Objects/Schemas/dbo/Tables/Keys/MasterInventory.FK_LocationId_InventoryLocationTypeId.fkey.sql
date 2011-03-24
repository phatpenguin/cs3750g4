ALTER TABLE [dbo].[MasterInventory]
	ADD CONSTRAINT [FK_MasterInventory_LocationId] 
	FOREIGN KEY (LocationId)
	REFERENCES dbo.InventoryLocationType (Id)	
