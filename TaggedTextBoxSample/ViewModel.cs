using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaggedTextBoxSample
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<TagItem> _selectedTags = new List<TagItem>();
        public List<TagItem> SelectedTags
        {
            get { return _selectedTags; }
            set
            {
                _selectedTags = value;
                if (_selectedTags != value)
                    OnPropertyChanged("SelectedTags");
            }
        }

        public ViewModel()
        {
            this.SelectedTags = new List<TagItem>() { new TagItem("news"), new TagItem("priority") };
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
