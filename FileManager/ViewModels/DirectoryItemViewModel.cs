using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FileManager
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name => this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStucture.GetFileFolderName(this.FullPath);

        /// <summary>
        /// A list of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand => this.Type != DirectoryItemType.File;

        /// <summary>
        /// Indicates if the current item is expanded ot not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }

            set
            {
                //If UI tells us to expand
                if (value == true)
                    //Find all children
                    Expand();
                //If the UI tells us to close
                else
                    ClearChildren();
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullpath">full path of this item</param>
        /// <param name="type">The type of this item</param>
        public DirectoryItemViewModel(string fullpath, DirectoryItemType type)
        {
            ExpandCommand = new RelayCommand(Expand);
            FullPath = fullpath;
            Type = type;

            ClearChildren();
        }

        #endregion

        /// <summary>
        /// Removes all children from the list
        /// And adding a dummy item to show the expand icon
        /// </summary>
        private void ClearChildren()
        {
            //Clear items;
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            //Add dummy item
            if(this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        /// <summary>
        /// Expand the directory and finds all children
        /// </summary>
        private void Expand()
        {
            //We cannot expand the file
            if (Type == DirectoryItemType.File)
                return;

            //Find all children
            var structure = DirectoryStucture.GetDirectoryContent(FullPath);
            Children = new ObservableCollection<DirectoryItemViewModel>(
                            structure.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
