using NuGet.Versioning;

public static class Extensions
{
  public static SemanticVersion IncrementVersion(this SemanticVersion semanticVersion, ReleaseType releaseType)
  {
    if (releaseType == ReleaseType.Major)
    {
      semanticVersion = new SemanticVersion(semanticVersion.Major + 1, 0, 0);
    }
    else if (releaseType == ReleaseType.Minor)
    {
      semanticVersion = new SemanticVersion(semanticVersion.Major, semanticVersion.Minor + 1, 0);
    }
    else
    {
      semanticVersion = new SemanticVersion(semanticVersion.Major, semanticVersion.Minor, semanticVersion.Patch + 1);
    }
    return semanticVersion;
  }
}