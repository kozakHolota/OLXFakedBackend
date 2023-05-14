using System;
namespace OLXFakedBackend.Utils
{
	public class FileUtils
	{
        public static string GetPath(params string[] pathParts)
        {
            pathParts.Append(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            return Path.Join(pathParts);
        }
    }
}

