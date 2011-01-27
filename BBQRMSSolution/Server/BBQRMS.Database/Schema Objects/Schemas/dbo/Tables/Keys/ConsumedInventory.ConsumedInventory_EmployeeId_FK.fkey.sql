ALTER TABLE [dbo].[ConsumedInventory]
	ADD CONSTRAINT [FK_EmployeeId_ConsumedInventory] 
	FOREIGN KEY (EmployeeId)
	REFERENCES Employee (Id);	

