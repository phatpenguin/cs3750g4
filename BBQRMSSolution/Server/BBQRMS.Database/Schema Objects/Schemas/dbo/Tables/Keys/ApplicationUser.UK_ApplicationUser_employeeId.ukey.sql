ALTER TABLE [dbo].[ApplicationUser]
    ADD CONSTRAINT [UK_ApplicationUser_employeeId]
    UNIQUE (employeeId)