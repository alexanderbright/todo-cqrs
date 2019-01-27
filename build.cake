#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Task("Default")
    .Does(() => {
        Informatio("Completed")
    });

RunTarget(target);
