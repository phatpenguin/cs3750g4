# The .sql files #
**1)** Make your changes in the .sql files contained in the BBQRMS.Database project (in the Server folder in Solution Explorer). You can use Solution Explorer to find the file you need or you can use the Schema View window (**View** menu, **Database Schema View**) then double-click the table to open its definition file.

**2)** Deploy the database project. You can right-click the BBQRMS.Database project in solution explorer and choose "Deploy". Make sure there are no errors displayed in the Output window (**View** menu, **Output**, Show output from: **Build**). Ignore the "Cannot grant, deny, or revoke permissions..." message that will appear near the top.

# The Server-side EDMX file #
**3)** Open the BBQRMS.edmx file in the BBQRMS.Services project. Select all the tables and relationships (Ctrl+A) and remove them. Right-click on the now empty space and choose "Update Model from Database...". Then on the "Add" tab, select all the tables except "`_RefactorLog`".

# The client-side ServerProxy reference #
**4)** Run BBQRMS.Server.Console. You can right-click it in solution explorer, then choose "Debug" and "Start new instance". Once it is running, you will need to detach the debugger from it so that Visual Studio will allow you to make changes to the client code. Choose the "Debug" menu, "Detach All". Then right click on ServerProxy in the gray Service References folder in the BBQRMS.Client project in Solution Explorer and choose "Update Service Reference".

**5)** Exit the server console which is still running otherwise you won't be able to build it the next time.