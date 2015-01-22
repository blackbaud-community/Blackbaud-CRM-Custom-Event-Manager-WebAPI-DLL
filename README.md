Blackbaud-CRM-Custom-Event-Manager-WebAPI-DLL
=============================================

## What You Will Build ##

This customization demonstrates how to create a custom user interface to communicate with a hosted Blackbaud Altru application and to retrieve a list of events from the Infinity database. This code sample can be modified for on premise or hosted Blackbaud CRM instances, as well.  All communication with Infinity-based applications utilize the Blackbaud AppFx Web Service either directly through web service calls to AppFxWebService.asmx or through one of the various middleware "wrappers" that call the AppFxWebService.asmx on our behalf.

## Prerequisites ##

This customization requires you to intsall the Infinity SDK for your hosted version of Altru or another Infinity application. The Blackbaud.AppFx.WebAPI.dll that this customization uses to ease communication to the AppFxWebService.asmx has a dependency on .NET Framework 4.0.

Review code CustomEventManagerWebAPIDll\CustomerEventManager\Form.vb.  you will need to provide the following infomration to point to the correct web service and database.

1. Within InitializeAppFxWebService() provide the following:
- AppFxWebService.asmx URL for your specific environment
- ClientAppName which will be logged in the Infinity database and used for auditing purposes
- Provide a key which identifies the database
2. Within GetNetworkCredentials() provide the following:
- Ensure you have a user within the application with sufficient rights to the features used in this code sample.
- Provide tne appropriate user name and password



##Resources##
* See the [Blackbaud CRM Read Me](https://github.com/blackbaud-community/Blackbaud-CRM/blob/master/README.md)
* [Step by Step Instructions](https://www.blackbaud.com/files/support/guides/infinitydevguide/infsdk-developer-help.htm#../Subsystems/inwebapi-developer-help/Content/InfinityWebAPI/coExampleConsumingTheBlackbaudAppFxWebServiceUsingANETWinFormsClient.htm) for consuming the Blackbaud AppFx Web Service
* [Infinity Web API](https://www.blackbaud.com/files/support/guides/infinitydevguide/infsdk-developer-help.htm#../Subsystems/inwebapi-developer-help/Content/InfinityWebAPI/WelcomeInfinityWebAPI.htm) Chapter within Developer Guides

##Contributing##

Third-party contributions are how we keep the code samples great. We want to keep it as easy as possible to contribute changes that show others how to do cool things with Blackbaud SDKs and APIs. There are a few guidelines that we need contributors to follow.

For more information, see our [canonical contributing guide](https://github.com/blackbaud-community/Blackbaud-CRM/blob/master/CONTRIBUTING.md) in the Blackbaud CRM repo which provides detailed instructions, including signing the [Contributor License Agreement](http://developer.blackbaud.com/cla).
