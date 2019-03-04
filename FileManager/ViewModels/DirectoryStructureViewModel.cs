using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    /// <summary>
    /// The view model fot the application main Directory View
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// A list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }


        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            //Get all logical drives on the machine
            var childrenDrives = DirectoryStucture.GetLogicalDrives();

            //Create a view models from the data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                                childrenDrives.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive, false)));
        }

    }
}
