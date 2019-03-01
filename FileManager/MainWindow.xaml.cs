using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FileManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region OnLoaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Search all drives in a machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create a new item for it
                var item = new TreeViewItem
                {
                    //Set the header
                    Header = drive,
                    //Set the full path
                    Tag = drive,
                };

                //Add null-item in list for item being expand
                item.Items.Add(null);

                //When we want to expand..
                item.Expanded += Folder_Expanded;

                //Add out item in main Tree-View
                FolderView.Items.Add(item);
            }
        }

        #endregion

        #region Folder Expanding

        /// <summary>
        /// When a folder is expanded, find the sub folders/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Check

            var item = (TreeViewItem)sender;

            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            //Clear dummy data
            item.Items.Clear();

            var fullPath = (string)item.Tag;

            #endregion

            #region Get Folders

            //Create a blank list for directories
            var directories = new List<string>();

            try
            {
                //Get all directories on selected drive
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            //For each directory
            directories.ForEach(directoryPath =>
            {
                //Create a new directory item
                var subItem = new TreeViewItem
                {
                    //Set the header
                    Header = GetFileFolderName(directoryPath),
                    //Set the full path
                    Tag = directoryPath,
                };

                //Add dummy data...
                subItem.Items.Add(null);

                subItem.Expanded += Folder_Expanded;

                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files

            //Create a blank list for file
            var files = new List<string>();

            try
            {
                //Get all files on selected directory
                var fls = Directory.GetFiles(fullPath);

                if (fls.Length > 0)
                    files.AddRange(fls);
            }
            catch { }

            //For each file
            files.ForEach(filePath =>
            {
                //Create a new file
                var subItem = new TreeViewItem
                {
                    //Set the header
                    Header = GetFileFolderName(filePath),
                    //Set the full path
                    Tag = filePath,
                };


                item.Items.Add(subItem);
            });

            #endregion
        }

        #endregion

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
