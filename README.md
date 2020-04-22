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
+ Chrome **81**.
+ Windows OS. If you use an another OS, please update the PactNet package (TAContract.tests) according to your machine (not tested).

## I don't have the 81 version of Chrome
You will see this error message after the test execution in the TASystem.test project:
```
InvalidOperationException : session not created: This version of ChromeDriver only supports Chrome version XX (SessionNotCreated)
```
In this case just update the ```Selenium.WebDriver.ChromeDriver``` package with the version corresponding to your Chrome browser.  
https://www.nuget.org/packages/Selenium.WebDriver.ChromeDriver



_This solution has been written with Visual Studio 2019 (version 16.5.3)_
