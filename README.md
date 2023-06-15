# Test-Flappy-Bird
The project was given by company to test skills in C# and Unity

By the task I can't use additional plugins, so I tried to use Service locator, to resolve dependency injection.

Service locator is a component/class that encapsulates knowledge of how to obtain services that the Client needs/depend upon. It is a single point of contact for the Client to get services. It is a singleton registry for all services that are used by the client

Another task was to create json file to store high score for each level and volume sound even after exit app. To solve this I create SaveService.