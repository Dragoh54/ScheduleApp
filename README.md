# Schedule App
### Features(in progress)
* User Service
* Schedule Service
* Meeting Service
* ApiGateway (Ocelot)

## Secrets
### in root <b>.env</b> for docker containers
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

  #Redis for ScheduleService
  SCHEDULE_SERVICE_REDIS_PORTS=

  #Redis for MeetingService
  MEETING_SERVICE_REDIS_PORTS=

  #MongoDB
  MONGO_INITDB_ROOT_USERNAME=
  MONGO_PASSWORD=
  MONGO_PORTS=

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
###  in .Api projects <b>secrets.json</b> for services
```json
  {
    "ConnectionStrings": {
    "UserServiceDbContext": "connection string"
    }
  }
```
