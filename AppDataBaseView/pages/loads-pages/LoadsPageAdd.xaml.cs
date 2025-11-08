using AppDataBaseView.Models;
using System;
using System.Collections.Generic;
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

namespace AppDataBaseView.pages.loads_pages
{
    /// <summary>
    /// Логика взаимодействия для LoadsPageAdd.xaml
    /// </summary>
    public partial class LoadsPageAdd : Page
    {
        public static FormWindow formWindow { get; set; }
        public class ComboBoxItem_LoadType : ComboBoxItem 
        {
            public TypesLoad LoadTypeLink { get; set; }
        }
        public LoadsPageAdd()
        {
            InitializeComponent();
            GetData();
        }
        public void WriteButton_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();
            ComboBoxItem_LoadType item = load_type_code_cb.SelectedItem as ComboBoxItem_LoadType;

            if (string.IsNullOrEmpty(code_tb.Text) || string.IsNullOrEmpty(name_tb.Text) ||
                string.IsNullOrEmpty(exp_date_tb.Text) || string.IsNullOrEmpty(describe_tb.Text) ||
                load_type_code_cb.SelectedItem == null)
            {
                Context.Loads.Add(
                    new Models.Load()
                    {
                        LoadCode = Convert.ToInt32(code_tb.Text),
                        Name = name_tb.Text,
                        ExpDate = exp_date_tb.Text,
                        Describe = describe_tb.Text,
                        LoadTypeCode = item.LoadTypeLink.LoadTypeCode
                    }
                );
                Context.SaveChanges();
                MessageBox.Show("Добавление прошло успешно");
                formWindow.Close();
                Scripts.EnableAllButtons();
            }
            else MessageBox.Show("Все поля должны быть заполнены");
        }
        private void GetData() 
        {
            DataBaseContext Context = new DataBaseContext();

            foreach (TypesLoad load in Context.TypesLoads.ToList<TypesLoad>())
            {
                load_type_code_cb.Items.Add(
                        new ComboBoxItem_LoadType()
                        {
                            Content = $"{load.LoadTypeCode} - {load.Name}",
                            LoadTypeLink = load
                        }
                    );
            }
        }
    }
}
