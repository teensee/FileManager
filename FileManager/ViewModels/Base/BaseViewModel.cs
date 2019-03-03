using System.ComponentModel;

namespace FileManager
{
    /// <summary>
    /// A base view model that fires <see cref="PropertyChanged"/> events as needed
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        
    }
}