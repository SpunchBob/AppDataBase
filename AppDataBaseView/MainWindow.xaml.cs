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
using AppDataBaseView.Models;
using AppDataBaseView.pages.EmployeesPages;

namespace AppDataBaseView
{
    public partial class MainWindow : Window
    {
        public string? Current { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MainWindowHandlers.ButtonsHandlers.Data    = data;
            MainWindowHandlers.ButtonsHandlers._window = (MainWindow)System.Windows.Application.Current.MainWindow;
            MainWindowHandlers.CheckBoxHandlers.Window = (MainWindow)System.Windows.Application.Current.MainWindow;
            
            Scripts.MainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;

            writeButton.Click   += MainWindowHandlers.ButtonsHandlers.WriteButton_OnClick;
            changeButton.Click  += MainWindowHandlers.ButtonsHandlers.ChangeButton_OnClick;
            deleteButton.Click  += MainWindowHandlers.ButtonsHandlers.DeleteButton_OnClick;
            readButton.Click    += MainWindowHandlers.ButtonsHandlers.ReadButton_OnClick;
            selection_btn.Click += MainWindowHandlers.ButtonsHandlers.Selection_btn_Click;
            union_btn.Click     += MainWindowHandlers.ButtonsHandlers.join_btn;
            function_btn.Click  += MainWindowHandlers.ButtonsHandlers.stats_btn;

            Scripts.AppendTablesList(tablesListBox, (MainWindow)Application.Current.MainWindow);
            // Scripts.AddListBoxItemsHandlers(tablesListBox, MainWindowHandlers.ListBoxItemsHandlers.GetHandlers(), MainWindowHandlers.ListBoxItemsHandlers.ListBoxItem_Selected);

            using (DataBaseContext Context = new DataBaseContext()) 
            {
                if (Context.Database.CanConnect()) 
                {
                    connect_circl.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));
                    connect_tblock.Text = "Подключено";
                    current_path_tblock.Text = Context.Database.GetConnectionString();
                    var tableCount = Context.GetType()
                                    .GetProperties()
                                    .Where(p => p.PropertyType.IsGenericType &&
                                                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                                    .Count();
                    tables_count_tblock.Text = $"Таблиц: {tableCount}";
                    state_tb.Text = "Таблиц найдены";
                }
            }

            time_tblock.Text = $"Дата входа: {DateTime.Now}";
        }
    }
}