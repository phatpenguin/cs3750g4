# Details #
.xaml files should include
```
<Views:UserControlBase
	x:Class="BBQRMSSolution.Views.XXXXX"
	x:TypeArguments="ViewModels:YYYYY"
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
	mc:Ignorable="d"
	xmlns:ViewModels="clr-namespace:BBQRMSSolution.ViewModels"
	xmlns:Views="clr-namespace:BBQRMSSolution.Views"
	xmlns:c="clr-namespace:Controls;assembly=Controls"
	d:DataContext="{d:DesignInstance Type=ViewModels:YYYYY, IsDesignTimeCreatable=True}">

```

XXXXX is the name of this view (the same as the name of the xaml file).

YYYYY is the name of the view model class that will be bound to this view.

The `d:DataContext=` attribute is ignored at runtime and only used by the designer (both Blend 4 and Visual Studio 2010). By specifying `IsDesignTimeCreatable=True`, the designer will create an instance using the default (parameterless) constructor.

Therefore, whatever properties your viewmodel sets in its default constructor will be reflected in the designer. The designer, only creates your viewmodel from the last compiled version, however, so if you change your view model class, you have to build again before the designer shows the changes.