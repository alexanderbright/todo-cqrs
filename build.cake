
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var distDirectory = MakeAbsolute(Directory("./dist"));
var solution = MakeAbsolute(Directory("./src"));
var webProject = solution.Combine("web/TodoCQRS.Web/TodoCQRS.Web.csproj");

Task("BuildAndTest")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

// The default task to run if none is explicitly specified. In this case, we want
// to run everything starting from Clean, all the way up to Publish.
Task("Default")
    .IsDependentOn("BuildAndTest")
    .IsDependentOn("PublishWeb");

Task("Clean")
.Does(() => {
        CleanDirectory(distDirectory);
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
        var projects = GetFiles("./test/**/*.csproj");
        foreach(var project in projects)
        {
            Information("Testing project " + project);
            DotNetCoreTest(
                project.ToString(),
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true,
                    ArgumentCustomization = args => args.Append("--no-restore"),
                });
        }
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

RunTarget(target);
