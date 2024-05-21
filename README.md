 SettlementBookingAgent_NET6.0.API

## Overview

SettlementBookingAgent_NET6.0.API is a .NET 6.0 Web API for managing bookings. 
This API allows users to create and retrieve bookings:
1.each booking will be one hour ,
2.booking time must between 09:00 to 16:00.
3.ensuring that no more than 4 bookings at the same time.
4.at this version, all bookings will be placed in today if the booking is happened between 09:00 to 16:00.

## Technologies Used

- .NET 6.0
- Entity Framework Core
- In-Memory Database
- ASP.NET Core Web API
- NUNIT Test

## Project Structure

![image](https://github.com/57tannerpl/BookingAgentSystem_NET6.0/assets/170209495/469f8a83-1475-47a4-94b3-a2a1a7125bef)

## Endpoints

### Get All Bookings

- **URL:** `GET /api/Booking`
- **Response:**
  - **Status Code:** 200 OK
  - **Body:**
    ```json
    {
        "statusCode": 200,
        "success": true,
        "message": "Bookings retrieved successfully.",
        "data": [
            "bookings": [
      {
        "bookingId": "46ab68e5-9df3-4d71-b7b8-527641d1afb7",
        "bookingTime": "12:35",
        "organizer": "string",
        "attendee": "string",
        "purchaseType": "string",
        "clientName": "string",
        "createdAt": "0001-01-01T00:00:00"
      },
      {
        "bookingId": "424dbfa8-a6b6-4251-a33f-e389a7b12c96",
        "bookingTime": "12:35",
        "organizer": "string",
        "attendee": "string",
        "purchaseType": "string",
        "clientName": "string",
        "createdAt": "0001-01-01T00:00:00"
      },
        ]
    }
    ```
  - **Empty Response:**
    ```json
    {
        "statusCode": 200,
        "success": true,
        "message": "No booking yet.",
        "data": null
    }
    ```

### Create a New Booking

- **URL:** `POST /api/Booking`
- **Request Body:**
  ```json
  {
    "bookingTime": "HH:mm",
    "organizer": "string",
    "attendee": "string",
    "purchaseType": "string",
    "clientName": "string",
    "createdAt": "2024-05-20T01:23:52.629Z"
  }
  ```

 - **ok Response:**
 ```json
{
  "status": 200,
  "success": true,
  "message": "Booking successfully.",
  "data": {
    "bookingId": "46ab68e5-9df3-4d71-b7b8-527641d1afb7"
  },
  "errors": null
}
```
- **Conflict Response:**
 ```json
{
  "Status": 409,
  "Success": false,
  "Message": "Conflict error",
  "Data": null,
  "Errors": [
    "Maximum reservation number 4 at the same time reached"
  ]
}
```
- **Bad Request Response:**
 ```json
{
  "Status": 400,
  "Success": false,
  "Message": "Bad request error",
  "Data": null,
  "Errors": [
    "Booking time must be between 9:00 and 16:00."
  ]
}
```
- **Bad Request Response:**
 ```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-1b52c04de74baa4db950c9d24bdbeca6-728eb78152aed60c-00",
  "errors": {
    "BookingTime": [
      "Invalid booking time format or value. The booking time format should be 'HH:MM' like '09:30' and in full half-hour format (e.g., 9:00, 9:30, 10:00, etc.)."
    ]
  }
}
```

### Running the Project:
- **1.Clone the repository.**
- **2.Open the solution in Visual Studio or your preferred IDE.**
- **3.Build the project to restore the dependencies.**
- **4.Run the project. The API will be available at https://localhost:7249/api/Booking**
