# Car Rental System API

This project involves creating a **Car Rental System API** using C# and the Entity Framework. It follows best practices for building RESTful APIs and includes functionalities such as car management, user authentication, booking services, and rental history.

---

## **Project Features**

### **Models**
1. **Car Model**
   - **Properties**: 
     - `Id`
     - `Make`
     - `Model`
     - `Year`
     - `PricePerDay`
     - `IsAvailable` (indicates if the car is available for rental)
2. **User Model**
   - **Properties**:
     - `Id`
     - `Name`
     - `Email`
     - `Password`
     - `Phone Number`
     - `Role` (either Admin or User)

---

### **Services**
1. **Car Rental Service**
   - **Core Logic**:
     - `RentCar`: Handles car rental processes.
     - `CheckCarAvailability`: Validates if a car is available for booking.
     - `CalculatePrice`: Calculates rent amount for car booked.
     - `getBookingHistory`: Provides Booking History for authorized user.

2. **User Service**
   - **Core Logic**:
     - `RegisterUser`: Registers a new user.
     - `AuthenticateUser`: Authenticates a user and returns a JWT token.

---

### **Repositories**
1. **Car Repository**
   - **Methods**:
     - `AddCar`: Add a new car.
     - `GetCarById`: Retrieve a car by ID.
     - `GetAvailableCars`: List all available cars.
     - `UpdateCarAvailability`: Update the availability status of a car.

2. **User Repository**
   - **Methods**:
     - `AddUser`: Register a new user.
     - `GetUserByEmail`: Retrieve a user by their email.
     - `GetUserById`: Retrieve a user by their ID.

---

### **API Filters for Validation**
- **Validation Methods**:
  - Used data annotations like `[Required]` and `[EmailAddress]` to validate inputs automatically.
  - Used custom Filter to validate car year.

---


### **Controllers for CRUD Operations**
1. **Car Controller**
   - **Endpoints**:
     - `GET /cars`: List all available cars.
     - `GET /cars/{id}`: Get the car based on id.
     - `POST /cars/rent`:Book a car and sent sms
     - `GET/cars/bookingHistory`:List history of cars booked by user.
     - `POST /cars`: Add a new car.
     - `PUT /cars`: Update car details or availability.
     - `DELETE /cars/{id}`: Remove a car from the system.

2. **User Controller**
   - **Endpoints**:
     - `POST /users/register`: Register a new user.
     - `POST /users/login`: Login and retrieve a JWT token.

---

### **Notification System**
- **Email Notification**:
  - Sends an SMS confirmation for successful car bookings using a service  **Twilio**.
  - Includes details like car make, rental duration,price and user name.

---

### **Authentication and Authorization**
- **JWT-Based Security**:
  - Users log in to obtain a JWT token.
  - Secures endpoints for actions like car rentals.
  - Implements role-based access for admin and user functionalities.

---

### **Testing**
- Use **Postman** to test the API endpoints.
  - Test Cases:
    - User registration and login (validate JWT tokens).
    - Viewing, renting, adding, and updating cars.

---

### For detailed documentation, please refer to the [Car Rental System API Documentation](https://github.com/swethashivannagari/CarRentalSystem/blob/master/carRentalDoc.pdf).
