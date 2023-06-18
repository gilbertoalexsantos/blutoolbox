using System.IO;

namespace BluToolbox.Editor
{
    public static class FileUtils
    {
        public static string GetTmpFilePath()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
        }

        public static void SafeDeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}