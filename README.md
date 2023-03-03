# Health API

Health API is a simple RESTful API built with ASP.NET Core that allows you to manage doctors and patients. It provides endpoints for creating, retrieving, updating, and deleting doctors and patients.

<!-- ABOUT THE PROJECT -->
## Getting started
To get started, you'll need to clone the repository and open the project in Visual Studio or your preferred IDE. Then, run the application and it should start up on http://localhost:5000.


### Prerequisites

* .NET 5.0 SDK or later
* Visual Studio 2019 or later (optional)

### Running the application

You can run the application using the following command in a terminal or command prompt:

  ```
  dotnet run

  ```
Once the application is running, you can access the API at http://localhost:5000.



## Endpoints

The following endpoints are available:

* GET /doctors: Returns a list of doctors that have at least one patient.

* GET /doctors/{id}: Returns a single doctor by ID, along with their associated patients.

* POST /doctors: Creates a new doctor.
  
* PUT /doctors/{id}: Updates a doctor's email address.
  
* DELETE /doctors/{id}: Deletes a doctor if they have no patients.
  
* GET /patients: Returns a list of all patients.
  
* GET /patients/{id}: Returns a single patient by ID.
  
* POST /patients: Creates a new patient.
  
* PUT /patients/{id}: Updates a patient's information.
  
* DELETE /patients/{id}: Deletes a patient.


## Testing

Health API includes some unit tests to ensure the functionality of the service. You can run these tests using the following command in a terminal or command prompt:


  ```
  dotnet test
  ```

## Contributing

Contributions are welcome! If you'd like to contribute to Health API, please fork the repository and create a pull request.
