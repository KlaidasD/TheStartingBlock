# The Starting Block V2

This project is an API for managing events and participants called "The Starting Block". It provides endpoints to manage Events, Participants and Results.

Functionality (V1) of the API includes the following features : CRUD operations for events including creation, retrieval, retrieval by ID, update and adding participants to events.

End points:\
POST\
/controller/AddEvent\
GET\
/controller/GetAllEvents\
POST\
/controller/GetEventById\
PUT\
/controller/UpdateEvent\
DELETE\
/controller/DeleteEvent\
POST\
/controller/AddParticipantToEvent

Functionality (V2) : adds the following features : CRUD operations for participants including creation, retrieval, retrieval by personalId, update and generating random participants for testing purposes.

End points:
POST\
/controller/AddParticipant\
GET\
/controller/GetAllParticipants\
POST\
/controller/GetParticipantByPersonalId\
PUT\
/controller/UpdateParticipant\
DELETE\
/controller/DeleteParticipant\
POST\
/controller/GenerateRandomParticipant

Technologies Used\
Framework: .NET Core\
Databases: SQL Server and mongoDB for caching.\
ORM: Entity Framework Core\
Logging: Serilog\
API Documentation: Swagger
