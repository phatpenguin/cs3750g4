ALTER TABLE [dbo].[ApplicationUser]
    ADD CONSTRAINT [UK_ApplicationUser_IdPart]
    UNIQUE (IdPart)