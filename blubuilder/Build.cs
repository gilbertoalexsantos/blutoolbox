using System.Collections.Generic;
using System.Linq;
using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.Utilities;
using Serilog;

class Build : NukeBuild
{
  public static int Main() => Execute<Build>(x => x.HelloWorld);

  [Parameter(Name = "release-type")] readonly ReleaseType ReleaseParam;

  [GitRepository] readonly GitRepository Repository;

  Target HelloWorld => _ => _
    .Executes(() =>
    {
      Log.Information("Hello World!");
    });

  Target Release => _ => _
    .Requires(() => ReleaseParam != null)
    .Executes(async () =>
    {
      GitService git = new(Repository.LocalDirectory);
      await git.Fetch();


      // Applying new tag
      List<string> tags = await git.GetTags();
      string lastTag = tags.Last().IsNullOrEmpty() ? "0.0.0" : tags.Last();
      SemanticVersion semanticVersion = SemanticVersion.Parse(lastTag).IncrementVersion(ReleaseParam);
      string newTag = semanticVersion.ToString();
      Log.Information("New tag to apply: {0}", newTag);


      // Apply new tag
      Assert.False(tags.Any(tag => tag == newTag), "Tag already exists");
      await git.ApplyTag(newTag);
      await git.PushTags();
    });
}