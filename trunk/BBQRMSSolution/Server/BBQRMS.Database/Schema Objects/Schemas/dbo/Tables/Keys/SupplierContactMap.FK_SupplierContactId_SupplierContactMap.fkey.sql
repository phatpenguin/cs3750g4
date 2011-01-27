ALTER TABLE [dbo].[SupplierContactMap]
	ADD CONSTRAINT [FK_SupplierContactId_SupplierContactMap] 
	FOREIGN KEY (SupplierContactId)
	REFERENCES SupplierContact (Id)	

