#tool "xunit.runner.console"
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var distDirectory = MakeAbsolute(Directory("./artifacts"));
var publishDirectory = distDirectory.Combine("publish");
var solution = MakeAbsolute(Directory("./src"));
var webProject = solution.Combine("web/TodoCQRS.Web/TodoCQRS.Web.csproj");
var webVersion = "1.0.0";

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
    .IsDependentOn("Package")
    .IsDependentOn("CreateNuget");

Setup(context => {
    webVersion = XmlPeek(webProject.FullPath, "/Project/PropertyGroup/Version/text()");
    Information($"Version detected {webVersion}");
});

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
                Verbosity = DotNetCoreVerbosity.Minimal
                , Configuration = configuration
                , ArgumentCustomization = args => args.Append("--no-restore"),
            });
    });

Task("Test")
    .Does(() =>
    {
        XUnit2("./test/**/*.dll",
            new XUnit2Settings()
            {
              OutputDirectory = publishDirectory.FullPath
              ,  HtmlReport = true
            });
    });

Task("PublishWeb")
    .Does(() =>
    {
        DotNetCorePublish(webProject.FullPath,
            new DotNetCorePublishSettings()
            {
                Configuration = configuration,
                OutputDirectory = publishDirectory,
                ArgumentCustomization = args => args.Append("--no-restore"),
            });
    });

Task("Package")
    .IsDependentOn("Build")
    .Does(() => {
        Zip(publishDirectory, distDirectory.CombineWithFilePath($"publish.{webVersion}.zip"));
    });


Task("CreateNuget")
    .IsDependentOn("Build")
    .WithCriteria(Jenkins.IsRunningOnJenkins)
    .Does(() => {

    var nugetPath = solution.CombineWithFilePath("libs/TodoCQRS.Infrastructture.Persistance/TodoCQRS.Infrastructture.Persistance.csproj");
    var versionOld = new Version(XmlPeek(nugetPath.FullPath, "/Project/PropertyGroup/Version/text()"));
    var version = new Version(versionOld.Major, versionOld.Minor, Jenkins.Environment.Build.BuildNumber);

    var nuGetPackSettings = new NuGetPackSettings
    {
    	OutputDirectory = distDirectory
    	, IncludeReferencedProjects = true
    	, Properties = new Dictionary<string, string>
    	{
    		{ "Configuration", "Release" }
    	}
        , Version = version.ToString()
    };
    NuGetPack(nugetPath, nuGetPackSettings);
});

RunTarget(target);
