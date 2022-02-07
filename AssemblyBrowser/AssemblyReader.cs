using AssemblyGetInfoLib;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace AssemblyBrowser
{
    public class AssemblyReader : INotifyPropertyChanged //Для уведомления системы об изменениях свойств модель AssemblyReader реализует интерфейс INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; private set; }
        private ObservableCollection<Node> root;
        private IGetInfo assemblyInfoGetter;

        public AssemblyReader(ObservableCollection<Node> root, IGetInfo infoGetter)
        {
            this.root = root;
            assemblyInfoGetter = infoGetter;
            RelayCommand = new RelayCommand(OpenHandler);
        }

        public event PropertyChangedEventHandler PropertyChanged; //Событие, которое будет вызвано при изменении модели
        public void OnPropertyChanged([CallerMemberName] string prop = "") //Метод, который скажет ViewModel, что нужно передать виду новые данные
                                                                           //атрибут CallerMemberName, чтобы не указывать имя члена в виде аргумента String вызываемому методу
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void OpenHandler(object param)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(*.exe,*.dll)|*.exe;*.dll"; ;
            if (dialog.ShowDialog() == true)
            {
                root.Clear();
                root.Add(assemblyInfoGetter.GetInfoFromFile(dialog.FileName));
                OnPropertyChanged(); //Если свойство меняется, вызывается метод, который уведомляет об изменении модели
            }
        }
    }
}