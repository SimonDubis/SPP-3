using AssemblyGetInfoLib;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssemblyBrowser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var root = new ObservableCollection<Node>();
            AssemblyTreeView.ItemsSource = root; //ItemsSource: ссылка на источник данных
            DataContext = new AssemblyReader(root, new AssemblyGetInfo()); //сообщаем приложению откуда брать данные
        }
    }
}
