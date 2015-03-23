# Note: #
This document is intended to be used as a reference for developers who would like to install this system for demonstration purposes. This document is not intended to be a comprehensive in its coverage and the reader may need to seek additional assistance from from other sources including, but not limited to, the administrator of this project.

This document should be used in conjunction with the DevelopmentEnvironmentSetup page.

# Server install #

This can be installed on Windows XP SP3, Windows Vista, Windows 7, Windows Server 2003, Windows Server 2008 or Windows Server 2008 [R2](https://code.google.com/p/cs3750g4/source/detail?r=2).

**NOTE**: Windows XP and Windows Server 2003 with IIS listening on port 80 is _not_ supported. Windows Vista, Windows 7, Windows Server 2008 and Windows Server 2008 [R2](https://code.google.com/p/cs3750g4/source/detail?r=2) will share port 80 between the RMS server and IIS.

  1. install the Microsoft .Net Framework 4 (get link)
  1. install Microsoft SQL Server 2008 [R2](https://code.google.com/p/cs3750g4/source/detail?r=2) (get link)
    * Express edition or higher
    * It is suggested to use windows integrated authentication and not SQL server authentication.
  1. deploy the database (see http://msdn.microsoft.com/en-us/library/dd193413.aspx):
    1. run `VSDBCMD /a:Deploy /dd:+ /manifest:BBQRMS.Database.deploymanifest`
      * all the files in the BBQRMS.Database\sql\debug folder are necessary
    1. run `DataImporter.exe "Scripts\Post-Deployment\ReferenceData\ReferenceDataTables.csv" "Data Source=.\sqlexpress;Integrated Security=True;Pooling=False;Initial Catalog=BBQRMS"`
      * DataImporter.exe and all the contents of BBQRMS.Database\Scripts\Post-Deployment\ReferenceData are necessary
      * This currently does not create any default administrator user. It would currently be necessary to import the TestData as well.
  1. place the server binaries into any folder on the machine
    * BBQRMS.ServerConsole.exe
    * BBQRMS.ServerConsole.exe.config
    * BBQRMS.WCFServices.dll

The server can currently only be run as a console application. It must be run under the windows credentials of a user who has been granted SQL Server access and at least datareader and datawriter roles in the BBQRMS database. If necessary, the database connection string can be changed in the BBQRMS.ServerConsole.exe.config file.

Once an actual installer is created, the server will be installed as a windows service which will start automatically when the system boots.

Start the server by running BBQRMS.ServerConsole.exe

# Client installation #

This can be installed on Windows XP SP3, Windows Vista, Windows 7, Windows Server 2003, Windows Server 2008 or Windows Server 2008 [R2](https://code.google.com/p/cs3750g4/source/detail?r=2). It can be installed on the same machine as the server or a TCP/IP network connection is required from each client to the server. This could be a wired or wireless connection.

  1. install the Microsoft .Net Framework 4 (unless already installed)
  1. install the Microsoft Point of Service SDK
    * only the runtime components are necessary
    * this provides a simulated cash drawer and receipt printer
  1. install the MagTek MSR software
  1. place the client binaries in any folder
    * AxInterop.KbdWedgeOCX.dll
    * BBQRMSSolution.exe
    * BBQRMSSolution.exe.config
    * Controls.dll
    * Interop.KbdWedgeOCX.dll
    * Microsoft.Expression.Interactions.dll
    * Microsoft.ReportViewer.Common.dll
    * Microsoft.ReportViewer.WinForms.dll
    * System.Windows.Interactivity.dll
  1. If the client is installed on a different machine from the server, modify BBQRMSSolution.exe.config and change the IP address to the IP address or hostname of the machine where the server will run.

Start the client by running BBQRMSSolution.exe