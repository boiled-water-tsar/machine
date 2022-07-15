Welcome to Machine Repository.

To run start a db with docker (or edit connection string):

`docker run --rm --name database -e POSTGRES_PASSWORD=docker -d -p 5432:5432 postgres`

Then compile and start the program with

`dotnet run`

Then navigate to https://localhost:5001/swagger/index.html
