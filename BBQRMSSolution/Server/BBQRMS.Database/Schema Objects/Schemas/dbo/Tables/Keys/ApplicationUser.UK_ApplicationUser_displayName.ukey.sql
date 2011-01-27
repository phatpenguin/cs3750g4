ALTER TABLE [dbo].[ApplicationUser]
    ADD CONSTRAINT [UK_ApplicationUser_displayName]
    UNIQUE (DisplayName)