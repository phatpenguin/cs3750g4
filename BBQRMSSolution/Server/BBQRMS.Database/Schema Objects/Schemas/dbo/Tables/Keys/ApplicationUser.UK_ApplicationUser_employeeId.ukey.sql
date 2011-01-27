ALTER TABLE [dbo].[ApplicationUser]
    ADD CONSTRAINT [UK_ApplicationUser_EmployeeId]
    UNIQUE (EmployeeId)