using System.Linq;
using LibGit2Sharp;
using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.Utilities.Collections;
using Serilog;

class Build : NukeBuild
{
  public static int Main() => Execute<Build>(x => x.HelloWorld);

  [GitRepository] readonly GitRepository Repository;
  [Parameter(Name = "deploy-type")] readonly DeployParameter DeployParam;

  Target HelloWorld => _ => _
    .Executes(() =>
    {
      Log.Information("Hello World!");
    });

  Target Deploy => _ => _
    .Requires(() => !Repository.Tags.IsEmpty())
    .Executes(() =>
    {
      string lastTag = Repository.Tags.Last();
      Log.Information("Last tag: {0}", lastTag);

      SemanticVersion semanticVersion = SemanticVersion.Parse(lastTag).IncrementVersion(DeployParam);
      string newTag = semanticVersion.ToString();
      Log.Information("New tag: {0}", newTag);

      using Repository repo = new(Repository.LocalDirectory);
      repo.NotNull();

      FetchOptions fetchOptions = new()
      {
        TagFetchMode = TagFetchMode.All,
        Prune = true
      };
      repo.Network.Fetch("origin", Enumerable.Empty<string>(), fetchOptions);
      Assert.False(repo.Tags.Any(tag => tag.FriendlyName == newTag), "Tag already exists");
      repo.ApplyTag(newTag);
      repo.Network.Push(repo.Network.Remotes["origin"], $"refs/tags/{newTag}");
      Log.Information("Tag {0} has been pushed to origin", newTag);
    });
}