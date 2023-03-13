# NetStack

Microservice project, initially started from Muhammad Khoirudin's tutorial on Medium.com (restaurant app using microservices):

https://medium.com/@mk.muhammadkhoirudin/microservice-implementation-using-asp-net-core-6-part-1-38e0766ab137

The food service is responsible for food & food categories logic. 
- Uses MongoDb as database
- Shows how to register settings (configs) as classes and register them in the dependency injection container (Data namespace)
- Shows how to upload a file in a MongoDb database (FoodController.cs)
- Acts as a publisher in the publisher-subscriber pattern usign RabbitMQ queues

The user service is responsible for the users (username and passwords) related to the customer info.
- Uses MySQL as database
- Shows how to use fluent API for model configuration (instead of using annotations - Configurations namespace)
- Shows how to implement (Repositories namespace) and use (Controllers) the Unit of Work design pattern

The order service is responsible for the orders placed through the system.
- Uses SqlServer as databse
- Acts as a subscriber in the publisher-subscriber pattern using RabbitMQ queues

The product service is responsible for scheduling jobs for the products. (It only logs different messages for each type of job scheduled).
- Uses Hangfire for job scheduling and execution (see the ProductsController)
