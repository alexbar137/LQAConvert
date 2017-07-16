using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using LQAConverter.Model.DataConnectors;

namespace LQAConverter.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        //Команды
        public RelayCommand<DragEventArgs> DragDropCommand { get; private set; }
        public RelayCommand OpenFileDialogCommand { get; private set; }
        public RelayCommand CreateReportCommand { get; private set; }


        //Свойства
        public ObservableCollection<FileData> Files { get; private set; }
        public LQAHeader Header { get; set; }

        //Внутренние переменные
               
        //Конструктор
        public MainWindowViewModel()
        {
            
            //Инициализация команд
            DragDropCommand = new RelayCommand<DragEventArgs>(DragDrop);
            OpenFileDialogCommand = new RelayCommand(OpenDialog);
            CreateReportCommand = new RelayCommand(CreateReport);
            
            //Инициализация свойств
            Files = new ObservableCollection<FileData>();
            Header = new LQAHeader() { Date = DateTime.Today};

            //Инициализация модели
            DataConnector DC = new DataConnector();
            
            
        }

        private void CreateReport()
        {
            Debug.WriteLine("Свойства заголовка");
            Debug.WriteLine(String.Format("Дата: {0}", Header.Date));
            Debug.WriteLine(String.Format("Менеджер: {0}", Header.Manager));
            Debug.WriteLine(String.Format("Количество слов: {0}", Header.WordsChecked));
            Debug.WriteLine(String.Format("Клиент: {0}", Header.Customer));
        }



        //Открытие файлов через диалог
        private void OpenDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel files | *.xlsx;*.xls";
            dialog.Multiselect = true;

            if (dialog.ShowDialog() != true) return;
            AddFiles(dialog.FileNames);
        }


        //Методы команд

        //Перетаскивание файлов
        private void DragDrop(DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
            AddFiles(filenames);

        }


        //Добавление файлов
        private void AddFiles(string[] filenames)
        {
            foreach (string s in filenames)
            {
                FileData file = new FileData();
                file.FileName = Path.GetFileName(s);
                file.Ext = Path.GetExtension(s);
                file.Path = Path.GetFullPath(s);

                //Проверка расширения файла
                if (file.Ext != ".xlsx" && file.Ext != ".xls")
                {
                    MessageBox.Show(string.Format("Unsupported file type:\r\n{0}", file.FileName), "Cannot add file");
                    continue;
                }

                //Проверка уникальности файла
                if (Files.Any(y => y.Path == file.Path)) continue;

                Files.Add(file);
                
            }
            RaisePropertyChanged("Files");
        }



        //Оповещение вида об изменении свойств
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }

       
        //Класс для информации о файлах
        public class FileData
        {
            public string FileName { get; set; }
            public string Path { get; set; }
            public string Ext { get; set; }
        }

        //Класс для информации о заголовке отчета
        public class LQAHeader
        {
            public DateTime Date { get; set; }
            public string Manager { get; set; }
            public string Customer { get; set; }
            public string Account { get; set; }
            public string SourceLang { get; set; }
            public string TargetLang { get; set; }
            public string ProjectCode { get; set; }
            public string Team { get; set; }
            public string Activity { get; set; }
            public string Vendor { get; set; }
            public string SubjectArea { get; set; }
            public string Container { get; set; }
            public string Application { get; set; }
            public int WordsChecked { get; set; }
            public string Reviewer { get; set; }
        }
    }

}
