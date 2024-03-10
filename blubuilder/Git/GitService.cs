using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using Nuke.Common.Utilities.Collections;

public class GitService
{
  private readonly string _repositoryPath;

  public GitService(string repositoryPath)
  {
    _repositoryPath = repositoryPath;
  }

  public async Task<List<string>> GetTags()
  {
    BufferedCommandResult result = await ExecuteCommand("tag");
    List<string> tags = result.StandardOutput.TrimEnd('\n').Split('\n').ToList();
    return tags;
  }

  public async Task Fetch()
  {
    await ExecuteCommand("fetch", ["--prune", "--prune-tags", "--verbose"]);
  }

  public async Task ApplyTag(string tagName)
  {
    await ExecuteCommand("tag", tagName);
  }

  public async Task PushTags()
  {
    await ExecuteCommand("push", "origin", "--tags");
  }

  private string GetOutput(BufferedCommandResult result)
  {
    string standardOutput = result.StandardOutput.IsNullOrEmpty() ? "No output" : result.StandardOutput.TrimEnd('\n');
    string standardError = result.StandardError.IsNullOrEmpty() ? "No output" : result.StandardError.TrimEnd('\n');
    return $"StandardOutput:\n{standardOutput}\n\nStandardError:\n{standardError}";
  }

  private void Log(string title, string msg)
  {
    StringBuilder builder = new();
    builder.AppendLine($"{title}");
    builder.AppendLine("-----");
    builder.AppendLine(string.IsNullOrEmpty(msg) ? "No output" : msg.TrimEnd('\n'));
    builder.AppendLine("-----");
    Console.Write(builder.ToString());
  }

  private async Task<BufferedCommandResult> ExecuteCommand(string command, params string[] arguments)
  {
    BufferedCommandResult result = await GitCommand()
      .WithArguments([command, .. arguments])
      .WithValidation(CommandResultValidation.None)
      .ExecuteBufferedAsync();

    Log(command, GetOutput(result));

    if (!result.IsSuccess)
    {
      throw new Exception($"Failed to execute {command}");
    }

    return result;
  }

  private Command GitCommand()
  {
    return Cli.Wrap("git").WithWorkingDirectory(_repositoryPath);
  }
}