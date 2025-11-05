#:sdk Cake.Sdk

Task("Restore")
    .Does(()=>DotNetRestore("src"));

Task("Build")
    .IsDependentOn("Restore")
    .Does(()=>DotNetBuild("src"));

Task("Test")
    .IsDependentOn("Build")
    .Does(()=>DotNetTest("src"));

Task("Pack")
    .IsDependentOn("Test")
    .Does(()=>DotNetPack(
        "src",
        new DotNetPackSettings
        {
            OutputDirectory = "./artifacts"
        }));

Task("UploadArtifact")
    .IsDependentOn("Pack")
    .WithCriteria(GitHubActions.IsRunningOnGitHubActions)
    .Does(()=>GitHubActions.Commands.UploadArtifact(
        Directory("./artifacts"),
        "NuGet"
    ));

Task("GitHubActions")
    .IsDependentOn("UploadArtifact");

RunTarget(Argument("target", "Test"));