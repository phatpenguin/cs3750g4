# Source #
Retrieve the source from https://cs3750g4.googlecode.com/svn/trunk and place it in any convenient working directory.

The Visual Studio solution is at https://cs3750g4.googlecode.com/svn/trunk/BBQRMSSolution/BBQRMSSolution.sln and includes both the client and server source code.

# IDE #
Visual Studio 2010 with SQL Server Express 2008 [R2](https://code.google.com/p/cs3750g4/source/detail?r=2) is required.

The project is configured to use a default installation of SQL Server Express with a named instance accessible at .\sqlexpress and configured to use windows integrated authentication. The user with which you run Visual Studio must have sysadmin rights in the SQL Express instance.

Installing Visual Studio provides the .Net Framework 4 and SQL Server express that are required to develop and run both the client and server.

# Point of Service #
Microsoft Point of Service SDK 1.12 must be installed.

Download and install http://www.microsoft.com/downloads/en/details.aspx?FamilyID=eaae202a-0fcc-406a-8fde-35713d7841ca

Install both the runtime components and the SDK components. You may have to choose to do a custom install.

# MagTek MSR #
The MagTek MSR Keyboard Wedge OCX control must be installed.

After retrieving the source code, run the setup in BBQRMSSolution/Libraries/MagTek MSR/setup.exe

# Running under the debugger #
You should configure your solution with multiple startup projects. Have it start BBQRMS.Client and BBQRMS.Server.Console.

When running the solution configuration "Debug DefaultSQLExpress", Visual studio will deploy (or recreate) the BBQRMS database in your SQL Express instance and populate it with seed and test data.