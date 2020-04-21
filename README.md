# Test Automation demo
Demo showing how to test a web application at different levels:
+ Unit testing (xUnit, MOQ).
+ Integration testing (xUnit, MOQ, Sqlite).
+ Contract testing (xUnit, Pact.Net).
+ System testing (xUnit, Selenium, MSpec).

## Technical specifications
+ .Net Core 3.1 / C#.
+ EF Core 3.1.
+ All dependencies are available via nuget.
+ No physical database (in-memory databases are used).
+ No web server for testing is required (self contained app with Kestrel).

The API uses the InMemory database provider and IIS Express for development purposes. 

## Configuration 
+ Chrome (>=81) must be installed in order to execute system testing.
+ Windows OS. If you use an another OS, please update the PactNet package (TAContract.tests) according to your machine (not tested).

This solution has been written with Visual Studio 2019 (version 16.5.3)
