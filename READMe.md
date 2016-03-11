# User Management API : Generate a one-time password and Validate 

### Question 
Please write a program or API that can generate a one-time password and verify if the password is valid for one user only. 
The input of the program should be: 
- User ID to generate the password
- Use the User ID and the password to verify the validity of the password.
- Every generated password should be valid for 30 seconds.

You are free to use a Web, MVC, Console or Class Library project in order to accomplish the requirement

### Solution
Implemented User Management API contains following API methods. 
##### API Method 1: GetOTPasswordForUser
- Parameter: userId
- Route - api/user/getOTPasswordForUser/{userId}
- Example http://localhost:28981/api/user/getOTPasswordForUser/TestUser1
- Return Model 
    ```sh
    {
        "UserId": "TestUser1",
        "OTPassword": "609371",
        "IsOTPasswordValid": true,
        "Message": "OTPassword: 609371 is valid until 30 seconds"
   }
    ```

##### API Method 2: ValidateOTPasswordForUser
- Parameter: userId, password
- Route - api/user/validateOTPasswordForUser/{userId}/{password}
- Example http://localhost:28981/api/user/validateOTPasswordForUser/TestUser1/510842
- Return Model 
    ```sh
    {
        "UserId": "TestUser1",
        "OTPassword": "510842",
        "IsOTPasswordValid": false,
        "Message": "OTPassword is Invalid"
    }
    ```

#### Logic
Time-based One-time Password Algorithm
- Ref - https://en.wikipedia.org/wiki/Time-based_One-time_Password_Algorithm
- Ref - http://tools.ietf.org/html/rfc4226#section-5.4

### Tech
- ASP.NET WebAPI, C#
- Dependency Injection - Unity / Mapping - AutoMapper
- UnitTest - NUnit
