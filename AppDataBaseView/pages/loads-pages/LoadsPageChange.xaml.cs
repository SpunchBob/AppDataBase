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
    /// Логика взаимодействия для LoadsPageChange.xaml
    /// </summary>
    public partial class LoadsPageChange : Page
    {
        public class ComboBoxItem_Load : ComboBoxItem
        {
            public Load LoadLink { get; set; }
        }
        public class ComboBoxItem_LoadTypes : ComboBoxItem
        {
            public TypesLoad LoadTypesLink { get; set; }
        }
        public static FormWindow formWindow { get; set; }
        public LoadsPageChange()
        {
            InitializeComponent();
            GetData();
        }
        private void GetData()
        {
            DataBaseContext Context = new DataBaseContext();

            foreach (Load load in Context.Loads.ToList<Load>())
            {
                loads_cb.Items.Add(
                        new ComboBoxItem_Load()
                        {
                            Content = $"{load.LoadCode} - {load.Name}",
                            LoadLink = load
                        }
                    );
            }

            foreach (TypesLoad typesLoad in Context.TypesLoads.ToList<TypesLoad>())
            {
                load_type_code_cb.Items.Add(
                        new ComboBoxItem_LoadTypes()
                        {
                            Content = $"{typesLoad.LoadTypeCode} - {typesLoad.Name}",
                            LoadTypesLink = typesLoad
                        }
                    );
            }
        }

        public void load_type_code_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();
            ComboBox box = sender as ComboBox;
            ComboBoxItem_Load loadItem = box.SelectedItem as ComboBoxItem_Load;

            info_lb.Content = "Обязательно выберите изменяемую запись";

            code_tb.Text = loadItem.LoadLink.LoadCode.ToString();
            name_tb.Text = loadItem.LoadLink.Name.ToString();
            exp_date_tb.Text = loadItem.LoadLink.ExpDate.ToString();
            describe_tb.Text = loadItem.LoadLink.Describe.ToString();
            
            foreach (ComboBoxItem_LoadTypes loadTypesItem in load_type_code_cb.Items) 
            {
                if (loadTypesItem.LoadTypesLink.LoadTypeCode == loadItem.LoadLink.LoadTypeCode) 
                {
                    load_type_code_cb.SelectedItem = loadTypesItem;
                }
            }


        }

        public void change_btn_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();
            ComboBoxItem_LoadTypes item = load_type_code_cb.SelectedItem as ComboBoxItem_LoadTypes;

            if (string.IsNullOrEmpty(code_tb.Text) || string.IsNullOrEmpty(name_tb.Text) ||
                string.IsNullOrEmpty(exp_date_tb.Text) || string.IsNullOrEmpty(describe_tb.Text) ||
                load_type_code_cb.SelectedItem == null || item == null)
            {
                MessageBox.Show("Все поля должны быть заполнены");
            }
            else
            {
                Context.Update(
                        new Load()
                        {
                            LoadCode = Convert.ToInt32(code_tb.Text),
                            Name = name_tb.Text,
                            LoadTypeCode = item.LoadTypesLink.LoadTypeCode,
                            ExpDate = exp_date_tb.Text,
                            Describe = describe_tb.Text,
                        }
                        );
                try
                {
                    Context.SaveChanges();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("Изменение прошло успешно");
                formWindow.Close();
                Scripts.EnableAllButtons();
            }
        }
    }
}
