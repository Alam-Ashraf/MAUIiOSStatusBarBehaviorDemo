using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUIiOSStatusBarBehaviorDemo
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ColorItemClickedCommand { get; set; }
        public ICommand ChangeStatusBarColorCommand { get; set; }
        private ColorItem _lastSelectedItem;

        private ObservableCollection<ColorItem> _colors;
        public ObservableCollection<ColorItem> Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
                OnPropertyChanged(nameof(Colors));
            }
        }

        public MainPageViewModel()
        {
            ColorItemClickedCommand = new Command<ColorItem>((i) => OnColorItemClicked(i));
            ChangeStatusBarColorCommand = new Command(OnChangeStatusBarColor);

            Colors = new ObservableCollection<ColorItem>()
            {
                new ColorItem(){ Color = Color.FromArgb("#F7DC6F") },
                new ColorItem(){ Color = Color.FromArgb("#7DCEA0") },
                new ColorItem(){ Color = Color.FromArgb("#7FB3D5") },
                new ColorItem(){ Color = Color.FromArgb("#9B59B6") },
                new ColorItem(){ Color = Color.FromArgb("#641E16") },
                new ColorItem(){ Color = Color.FromArgb("#00FF00") },
                new ColorItem(){ Color = Color.FromArgb("#1ABC9C") },
                new ColorItem(){ Color = Color.FromArgb("#1B4F72") },
                new ColorItem(){ Color = Color.FromArgb("#D7DBDD") },
            };
        }

        private void OnColorItemClicked(ColorItem item)
        {
            if (item != null)
            {
                if (_lastSelectedItem != null)
                    _lastSelectedItem.IsSelected = false;

                _lastSelectedItem = item;
                item.IsSelected = true;
            }
        }

        private void OnChangeStatusBarColor()
        {
            if (_lastSelectedItem != null)
            {
                CommunityToolkit.Maui.Core.Platform.StatusBar.SetColor(_lastSelectedItem.Color);
                CommunityToolkit.Maui.Core.Platform.StatusBar.SetStyle(StatusBarStyle.LightContent);
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ColorItem : INotifyPropertyChanged
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
