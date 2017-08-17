# HotelBookingApp Description
This project is developed as part of capstone project for distributed software development course. In this project, there exists multiple hotel suppliers and travel agencies acting independently on their own threads, where suppliers emit priceCut event and travel agencies raises a booking request based on the event. The suppliers validates and process the orders.


## Execution of the project
To execute the program, press ctrl+F5 after opening the Homework2.sln file.

## Expected output:
A console will be opened and the orders received and orders completed are shown.
Note that there are 2 hotel suppliers and 4 travel agencies (can be modified in program.cs).
Each hotel supplier will make 2 price cuts (can be modified in HotelSupplier.cs) each and for each price cut, all travel agencies are notified.
Once they are notified the order are placed by the agencies and satisfied by the suppliers.
