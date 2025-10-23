using AppDataBaseView.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppDataBaseView
{
    public partial class MainWindow : Window
    {
        public static DbSet<dynamic>? current { get; set; }
        public DataBaseContext Context { get; set; } 

        public MainWindow()
        {
            InitializeComponent();

            Context = new DataBaseContext();

            MainWindowHandlers.ButtonsHandlers.Context = this.Context;
            MainWindowHandlers.ButtonsHandlers.Data = data;
            MainWindowHandlers.ButtonsHandlers.Window = Application.Current.MainWindow;

            MainWindowHandlers.ListBoxItemsHandlers.Context = this.Context;
            MainWindowHandlers.ListBoxItemsHandlers.Data = data;
            MainWindowHandlers.ListBoxItemsHandlers.Window = Application.Current.MainWindow;

            writeButton.Click += MainWindowHandlers.ButtonsHandlers.WriteButton_OnClick;
            changeButton.Click += MainWindowHandlers.ButtonsHandlers.ChangeButton_OnClick;
            deleteButton.Click += MainWindowHandlers.ButtonsHandlers.DeleteButton_OnClick;

            Scripts.AddListBoxHandlers(tablesListBox, MainWindowHandlers.ListBoxItemsHandlers.GetHandlers());
        }
    }
}