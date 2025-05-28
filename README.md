# Schedule App
This project implements a scheduling platform using a microservices architecture inspired by Calendly. The system is composed of several independent services, each responsible for a distinct domain.

## User Service
### Overview:
Handles user registration, authentication, profile management, roles, and permissions.
### Technologies:
* Jwt
* PostgreSQL
* Hangfire
* Mapster
* gRpc
* redis
* fluent validation

## Schedule Service
### Overview:
Manages availability templates, working hours, and scheduling rules.
### Technologies:
* Jwt
* MongoDb (with replicas)
* RabbitMQ (Consumer)
* redis
* mediatoR

## Meeting Service
### Overview:
Manages meeting booking, rescheduling, cancellation, and notifications.
### Technologies:
* Jwt
* PostgreSQL
* Hangfire
* Mapster
* SignalR
* RabbitMQ
* gRpc
* redis
* mediatoR

# Secrets
## in root <b>.env</b> for docker containers
```.env
  #ApiGateway
  API_GATEWAY_SERVICE_PORTS=""

  #UserService 
  USER_SERVICE_PORTS=""

  #ScheduleService
  SCHEDULE_SERVICE_PORTS=""

  #MeetingService 
  MEETING_SERVICE_PORTS=""

  #PgAdmin
  PGADMIN_DEFAULT_EMAIL=
  PGADMIN_DEFAULT_PASSWORD=
  PGADMIN_PORTS=

  #PostgreSQL for UserService
  USER_SERVICE_POSTGRES_USER=
  USER_SERVICE_POSTGRES_PASSWORD=
  USER_SERVICE_POSTGRES_DB=
  USER_SERVICE_POSTGRES_PORTS=

  #PostgreSQL for MeetingService
  MEETING_SERVICE_POSTGRES_USER=
  MEETING_SERVICE_POSTGRES_PASSWORD=
  MEETING_SERVICE_POSTGRES_DB=
  MEETING_SERVICE_POSTGRES_PORTS=

  #Redis for UserService
  USER_SERVICE_REDIS_PORTS=6379:6379

  #Redis for ScheduleService
  SCHEDULE_SERVICE_REDIS_PORTS=

  #Redis for MeetingService
  MEETING_SERVICE_REDIS_PORTS=

  #MongoDB
  MONGO_INITDB_ROOT_USERNAME=
  MONGO_PASSWORD=
  MONGO_PORTS=

  #MongoDB ReplicaPorts
  MONGO_PORTS_1 = 
  MONGO_PORTS_2 = 
  MONGO_PORTS_3 = 

  #MongoDB Express
  MONGO_EXPRESS_PORT=8081:8081
  MONGO_EXPRESS_USER=admin
  MONGO_EXPRESS_PASSWORD=expresspass

  #RabbitMQ
  RABBITMQ_DEFAULT_USER=
  RABBITMQ_DEFAULT_PASS=
  RABBITMQ_PORT_AMQP=
  RABBITMQ_PORT_UI=
  RABBITMQ_SERVER_ADDITIONAL_ERL_ARGS=-rabbit log_levels [{connection,error},{default,error}] disk_free_limit 2147483648

  # Elasticsearch
  ELASTIC_PORTS=
  ELASTIC_URL=

  # Logstash
  LOGSTASH_PORTS=

  # Kibana
  KIBANA_PORTS=
```
#  in .Api projects <b>secrets.json</b> for services
## User Service
```json
  {
    "ConnectionStrings": {
    "UserServiceDbContext": "connection string", 
    "Redis": "localhost:", 
    "Hangfire": "connection string"
    },
    "JWTSecretKey": "Jwt secret key",
      "EmailSettings": {
        "SmtpServer": "smtp.gmail.com",
        "SmtpPort": 465,
        "SmtpUsername": "email",
        "SmtpPassword": "key",
        "FromName": "Schedule App",
        "FromAddress": "email",
        "EnableSsl": true
      }
  }
```

## User Service gRpc
```json
  {
    "ConnectionStrings": {
    "UserServiceDbContext": "connection string"
    }
  }
```

## Schedule Service
```json
  {
  "MongoDbSettings": {
    "MongoDatabaseName": "schedule_service_db",
    "MongoConnectionString": "connection string"
  },
  "JWTSecretKey": "key",
  "MongoCollections": {
    "AvailabilityTemplates": "availability_templates",
    "Meetings": "meetings"
  },
  "RabbitMQ": {
    "Hostname": "hostname",
    "Username": "username",
    "Password": "password",
    "Port": 5672,
    "SubscriptionQueueName": "queue name"
  },
  "ConnectionStrings": {
    "Redis": "connection string"
  }
}
```

## Meeting Service
```json
{
  "ConnectionStrings": {
    "MeetingServiceDbContext": "connection string",
    "Hangfire": "connection string",
    "Redis": "connection string"
  },
  "JWTSecretKey": "key",
  "RabbitMQ": {
    "Hostname": "hostname",
    "Username": "username",
    "Password": "password",
    "Port": 5672,
    "SubscriptionQueueName": "queue name"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 465,
    "SmtpUsername": "email",
    "SmtpPassword": "key",
    "FromName": "Schedule App",
    "FromAddress": "email",
    "EnableSsl": true
  },
  "UserGrpcUrl": "http://hostname:port"
}
```