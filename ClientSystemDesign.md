# Introduction #

The client is built using a Model-View-ViewModel pattern and the source code is organized into namespaces and folders that match the layers of the architecture. Those layers are:
  1. Views
  1. View Models
  1. Business Logic
  1. Messages
  1. Model and data access

We try to reduce coupling by using dependency injection, specifically constructor dependency injection. This improves testability and the ability to replace components in the future. We also try to depend on interfaces instead of concrete classes.

Remember to create SOLID classes:
  * [\*S\*ingle responsibility](http://www.tomdalling.com/blog/software-design/solid-class-design-the-single-responsibility-principle)
  * [\*O\*pen-closed principle](http://www.tomdalling.com/blog/software-design/solid-class-design-the-open-closed-principle)
  * [\*L\*iskov substitution principle](http://www.tomdalling.com/blog/software-design/solid-class-design-the-liskov-substitution-principle)
  * [\*I\*nterface segregation principle](http://www.tomdalling.com/blog/software-design/solid-class-design-the-interface-segregation-principle)
  * [\*D\*ependency inversion principle](http://www.tomdalling.com/blog/software-design/solid-class-design-the-dependency-inversion-principle)

# Details #

The layers and their dependencies are depicted in the following diagram.

![http://cs3750g4.googlecode.com/svn/wiki/LayerDiagram1.png](http://cs3750g4.googlecode.com/svn/wiki/LayerDiagram1.png)

## Views ##
Views consist of the .xaml files, (and associated code-behind files), that define windows and user controls.

Views observe view models through WPF bindings and they also may invoke methods on view models in response to events caused by user actions. Typically the view does not pass parameters to these view model methods, instead the view model can read whatever data it needs from its own data-bound properties. When command bindings are used, it is often useful to bind the command parameter which will be passed to the view model's command-handling methods.

_Only in limited cases,_ a view may handle a message from the system-wide message bus when no view model can effectively manage it.

Views also may include bindings in order to read and set simple properties of model objects, however, views _must not_ interact with the data access service.

## View Models ##
View models are simple C# classes that derive from ViewModelBase which implements the INotifyPropertyChanged interface. These are intended to contain the bulk of the processing in this application. Specifically they should manage all of the user interface workflows. They also translate data and events when necessary between the views and the lower layers of the system.

View Models call the data access service to retrieve and save data in the form of model objects.

View Models can both send and handle messages from the system-wide message bus.

View Models use objects from the Business Logic layer when there is code that should be shared among several view models.

## Business Logic ##

For now this includes only two classes:
  * TimeProvider which abstracts the system clock so that automated tests can manipulate the virtual clock for objects which depend on the current time. _This class is the only one in the system that should call DateTime.Now or DateTime.UtcNow._ Other classes should instead depend on IClientTimeProvider and access its UtcNow or Now properties instead.
  * SecurityContext which exposes the currently logged-in user. It also handles the UserLoggedIn and UserLoggingOut message.

Eventually a proxy for the credit card processing service will be in this layer.

## Messages ##
This layer includes the system-wide message bus and the simple message classes whose instances carry event-specific information through the system.

The Messages sometimes will refer to ViewModels but this should be kept to a minimum. Normally messages will contain no data, simple properties of primitive data types items or references to model objects.

## Models and data access ##
This layer includes the BBQRMSEntities data service class and all the generated model classes in the BBQRMSSolution.ServerProxy namespace.

The Model and data access layer is the lowest layer in the client system and does not depend on or use any code from any higher layer. This layer, and specifically the BBQRMSEntities data access service is the only one that should connect to the server.