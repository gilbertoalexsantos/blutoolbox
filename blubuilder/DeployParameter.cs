using System.ComponentModel;
using Nuke.Common.Tooling;

[TypeConverter(typeof(TypeConverter<DeployParameter>))]
public class DeployParameter : Enumeration
{
  public static DeployParameter Major = new()
  {
    Value = nameof(Major)
  };

  public static DeployParameter Minor = new()
  {
    Value = nameof(Minor)
  };

  public static DeployParameter Patch = new()
  {
    Value = nameof(Patch)
  };

  public static implicit operator string(DeployParameter deployParameter)
  {
    return deployParameter.Value;
  }
}