using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManager
{
    /// <summary>
    /// A helper class to querry information about directories
    /// </summary>
    public static class DirectoryStucture
    {
        /// <summary>
        /// Gets all logical drives on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            return Directory.GetLogicalDrives().Select(
                        drive => new DirectoryItem
                                {
                                    FullPath = drive,
                                    Type = DirectoryItemType.Drive
                                }).ToList();
        }


        /// <summary>
        /// Gets the directories top-level content
        /// </summary>
        /// <param name="fullPath">the full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContent(string fullPath)
        {
            var items = new List<DirectoryItem>();

            #region Get Folders

            try
            {
                //Get all directories on selected drive
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch { }

            #endregion

            #region Get Files

           
            try
            {
                //Get all files on selected directory
                var fls = Directory.GetFiles(fullPath);

                if (fls.Length > 0)
                    items.AddRange(fls.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }

            #endregion

            return items;
        }



        #region Helper

        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            //Correct slashes
            var correctedPath = path.Replace('/', '\\');

            //Search last \\ index in the path
            var lastIndex = correctedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;

            return path.Substring(lastIndex + 1);
        }

        #endregion
    }
}
