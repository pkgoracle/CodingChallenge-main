**Considerations**
Removed existing code since it was only for reference and not aligned with requirements.
If an existing DbContext and migration are required, follow the current structure instead of introducing a new one.

**Implementation**
Code Segregation for Clean Architecture

API Layer: Controllers and frontend business logic. Added Api versioning for backword compatabilities. Instead of Get/Put/Delete using Post only for all actions for better security of request data.
Data Layer: All models and DTOs.
Service Layer: Business logic and security (e.g., encryption, decryption, masking).
EF Layer: Entity Framework context and database execution.

**Future Enhancements (with more time and flexibility)**
Global error handling via custom middleware and catch exception logging in api controllers
API request/response logging using action filters(if required).
Store database connection strings in Key Vault instead of appsettings.json for better security.
Dynamic validation rules sourced from the database.
Localization support for messages.
Validation limits and ranges configurable from the database.
CreatedBy and ModifiedBy values read from token in BaseApiController (not required from frontend api calls and validation).
