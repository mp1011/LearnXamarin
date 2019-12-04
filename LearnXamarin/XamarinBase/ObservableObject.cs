using System.Collections.Generic;
using System.ComponentModel;

namespace LearnXamarin.XamarinBase
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(string propertyName, ref T currentPropertyValue, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(currentPropertyValue, newValue))
                return false;

            OnPropertyChanging(propertyName);
            currentPropertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
