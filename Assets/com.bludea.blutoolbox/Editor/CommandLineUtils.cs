using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BluToolbox.Editor
{
  public static class CommandLineUtils
  {
    public static string GetArg(string name, List<string> args)
    {
      for (int i = 0; i < args.Count; i++)
      {
        if (args[i] == $"-{name}")
        {
          return args[i + 1];
        }
      }

      throw new Exception($"Missing arg {name}");
    }

    public static bool HasArg(string name, List<string> args)
    {
      for (int i = 0; i < args.Count; i++)
      {
        if (args[i] == $"-{name}")
        {
          return true;
        }
      }

      return false;
    }

    public static void RunBashCommand(string command)
    {
      StringBuilder shFileContentBuilder = new();
      shFileContentBuilder.AppendLine("#!/bin/bash");

      shFileContentBuilder.AppendLine("if [ -f ~/.profile ]; then");
      shFileContentBuilder.AppendLine("source ~/.profile");
      shFileContentBuilder.AppendLine("fi");

      shFileContentBuilder.AppendLine("if [ -f ~/.bash_profile ]; then");
      shFileContentBuilder.AppendLine("source ~/.bash_profile");
      shFileContentBuilder.AppendLine("fi");

      shFileContentBuilder.AppendLine("if [ -f ~/.zshrc ]; then");
      shFileContentBuilder.AppendLine("source ~/.zshrc");
      shFileContentBuilder.AppendLine("fi");

      shFileContentBuilder.AppendLine(command);

      string tmpFilePath = FileUtils.GetTmpFilePath();
      try
      {
        File.WriteAllText(tmpFilePath, shFileContentBuilder.ToString());
        ProcessStartInfo psi = new()
        {
          FileName = "/bin/bash",
          Arguments = tmpFilePath,
          UseShellExecute = false,
          RedirectStandardInput = true,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          CreateNoWindow = true
        };

        using Process process = Process.Start(psi);
        if (process == null)
        {
          throw new NullReferenceException("Process is null");
        }

        process.WaitForExit();
        string output = process.StandardError.ReadToEnd();
        if (process.ExitCode != 0)
        {
          throw new Exception($"Failed to execute command\nCommand: '{command}'\nError: \n{output}");
        }
      }
      finally
      {
        FileUtils.SafeDeleteFile(tmpFilePath);
      }
    }
  }
}