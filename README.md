# NetStack

Microservice project, initially started from Muhammad Khoirudin's tutorial on Medium.com (restaurant app using microservices):

https://medium.com/@mk.muhammadkhoirudin/microservice-implementation-using-asp-net-core-6-part-1-38e0766ab137

## Food Service
- Responsible for food & food categories logic. 
- Uses MongoDb as database
- Shows how to register settings (configs) as classes and register them in the dependency injection container (Data namespace)
- Shows how to upload a file in a MongoDb database (FoodController.cs)
- Acts as a publisher in the publisher-subscriber pattern usign RabbitMQ queues

## User Service 
- Responsible for the users (username and passwords) related to the customer info.
- Uses MySQL as database
- Shows how to use fluent API for model configuration (instead of using annotations - Configurations namespace)
- Shows how to implement (Repositories namespace) and use (Controllers) the Unit of Work design pattern

## Order Service 
- Responsible for the orders placed through the system.
- Uses SqlServer as databse
- Acts as a subscriber in the publisher-subscriber pattern using RabbitMQ queues

## Product Service
- Responsible for scheduling jobs for the products. (It only logs different messages for each type of job scheduled).
- Uses Hangfire for job scheduling and execution (see the ProductsController)

## ToDo Service 
- Responsible for fetching and marking todo items from the database.
- Uses SQL Server and EF Core
- Updates multiple items in the database concurrently, by creating multiple scopes within the original request

## FeatureService
- Responsible for simulating various features
- Displays multiple dependency injection features & use cases:
  * Register multiple implementations for an interface and resolve the dependency conditionally (see **ServiceFactory**, **IService**, **Service1**, **Service2**)
  * Register service with multiple interfaces and resolve just one instance (see **Storage**, **IReadStorage**, **IWriteStorage**)
  * Use logic from injected service at runtime (see **IStartupLogic**, **StartupLogic**, **Program**)
  * Dispose multiple services in created scopes (see **IComplexService**, **ComplexService**)
