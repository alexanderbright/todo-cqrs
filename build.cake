#tool "xunit.runner.console"
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var distDirectory = MakeAbsolute(Directory("./artifacts"));
var solution = MakeAbsolute(Directory("./src"));
var webProject = solution.Combine("web/TodoCQRS.Web/TodoCQRS.Web.csproj");
var pack = MakeAbsolute(Directory("./pack"));
var version = "1.0.0";

Task("BuildAndTest")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

// The default task to run if none is explicitly specified. In this case, we want
// to run everything starting from Clean, all the way up to Publish.
Task("Default")
    .IsDependentOn("BuildAndTest")
    .IsDependentOn("PublishWeb")
    .IsDependentOn("Package");

Setup(context => {
    version = XmlPeek(webProject.FullPath, "/Project/PropertyGroup/Version/text()");
    Information($"Version detected {version}");
});

Task("Clean")
.Does(() => {
        CleanDirectory(distDirectory);
        CleanDirectory(pack);
    });

Task("Restore")
    .Does(() =>{
        DotNetCoreRestore(solution.FullPath);
    });

Task("Build")
    .Does(() => {
        DotNetCoreBuild(solution.FullPath,
            new DotNetCoreBuildSettings()
            {
                Configuration = configuration,
                ArgumentCustomization = args => args.Append("--no-restore"),
            });
    });

Task("Test")
    .Does(() =>
    {
        XUnit2("./test/**/*.dll",
            new XUnit2Settings()
            {
                HtmlReport = true,                    
            });
    });

Task("PublishWeb")
    .Does(() =>
    {
        DotNetCorePublish(webProject.FullPath,
            new DotNetCorePublishSettings()
            {
                Configuration = configuration,
                OutputDirectory = distDirectory,
                ArgumentCustomization = args => args.Append("--no-restore"),
            });
    });

Task("Package")
    .IsDependentOn("Build")
    .Does(() => {
        Zip(distDirectory, distDirectory.CombineWithFilePath($"publish.{version}.zip"));
    });

RunTarget(target);
