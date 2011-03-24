ALTER TABLE [dbo].[ItemSupplierMap]
	ADD CONSTRAINT [FK_ItemSupplier_MasterInventoryId] 
	FOREIGN KEY (MasterInventoryId)
	REFERENCES MasterInventory (Id)	

