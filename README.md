# AGDATA_Testing
This repository includes the automation testing suite for the AGDATA application. 

# Contents
1. API Automation Testing
2. UI Automation Testing

# Pre-requisites
* Windows OS
* Visual Studio2022

# Get Started
Git clone the repository

# Installation
1. Visual Studio2022
2. Open the solution file"AGDATA_Application.sln" with Visual Studio2022 under the repository fold.

# How to run the tests
1. Run in Visual Studio: open 'Test Explore', right click the desired tests and click 'Run'.
2. Run from command line: open Windows Terminal from Windows start menu, navigate to project folder.
   1). For API automation, use: dotnet test AGDATAPITesting.csproj
   2). For UI automation, use: dotnet test AGDATAUIAutomationTest.csproj. It will execute all tests parllelly.

# Where is the log/report
1. API automation, the log is here: C:\Temp. You can customize this location in app.config file under the API project folder.
2. For UI automation, the log/report is 'Report' - under the project folder.  


