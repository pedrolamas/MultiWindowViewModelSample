namespace MultiWindowViewModelSample
{
    public class MainViewModel : MultiWindowViewModelBase
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value) return;

                _text = value;
                OnPropertyChanged();
            }
        }
    }
}
