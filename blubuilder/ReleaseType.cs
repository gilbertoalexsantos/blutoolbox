using System.ComponentModel;
using Nuke.Common.Tooling;

[TypeConverter(typeof(TypeConverter<ReleaseType>))]
public class ReleaseType : Enumeration
{
  public static ReleaseType Major = new()
  {
    Value = nameof(Major)
  };

  public static ReleaseType Minor = new()
  {
    Value = nameof(Minor)
  };

  public static ReleaseType Patch = new()
  {
    Value = nameof(Patch)
  };

  public static implicit operator string(ReleaseType releaseType)
  {
    return releaseType.Value;
  }
}