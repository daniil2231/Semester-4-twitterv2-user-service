# twitterv2-user-service
- The user service is responsible for handling CRUD operations for users.
- Has its own database.
- Receives API calls through an API gateway.
- Uses Kafka to produce messages when a change to a user account occurs in order to notify the tweet and auth services so that they can be updated accordingly.

![Architecture Diagram](https://github.com/daniil2231/twitterv2-user-service/blob/main/c4_user.png)
