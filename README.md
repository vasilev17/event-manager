# Introduction 
A project about finding Evends and Activities

# Getting Started
1.  Create a appsettings.Development.json file
2.	Copy the connection string part from appsettings.json into it
3.	Enter the missing values from the connection string for your MySQL server instalation
4.	Run Update-Database command to update your database
5.  Set up JWT
    5.1 Copy the "Jwt" section from appsettings.json to appsettings.Develoment.json
    5.2 Generate a signing key and put it in your appsettings.Development.json
    5.3 Put token duration in time span format
    5.4 Issuer and audience are the localhost adreses of the back-end and front-end
6.  Set up the email sender
    6.1 Go to (https://www.twilio.com/en-us/blog/send-emails-using-the-sendgrid-api-with-dotnetnet-6-and-csharp) and follow the steps for creating an api key
    6.2 Copy the "EmailSender" section from appsettings.json to appsettings.Develoment.json 
    6.3 Ener the needed keys.


# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)