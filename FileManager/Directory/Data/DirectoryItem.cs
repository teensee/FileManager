namespace FileManager
{
    /// <summary>
    /// Information about a directory item such as a drive/folder/file
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        public bool IsHidden { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name => DirectoryStucture.GetFileFolderName(this.FullPath, this.Type);
    }
}
