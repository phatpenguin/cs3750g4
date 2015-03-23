# Code Templates #
Views and View Models should follow certain conventions. In order to help with that, there are Visual Studio 2010 item templates for each of those in source control which you can import into your personal templates folder.

To use these item templates, you can copy the two .zip files from the "Visual Studio Stuff" folder under source control to your personal item templates folder which, on Windows 7, is at C:\Users\...(username)...\Documents\Visual Studio 2010\Templates\ItemTemplates\Visual C#. The next time you go to "Add New Item", and select "Visual C#" at the top of the list, you will see "BBQRMS View Model" and "BBQRMS View".

# Coding Conventions #
For those of you with ReSharper 5.1 installed, under its options, under Languages/Common/Code Style Sharing, you should select "Shared across the team, per solution" which should cause it to use the settings file which has now been checked-in to source control. This settings file includes variable naming conventions.

It also includes a Live Template "propvm" which will create a view model property that correctly raises the PropertyChanged event through the ViewModelBase class.

It also includes a File Template for a new view model class. The template matches the Visual Studio item template, so you can use either.