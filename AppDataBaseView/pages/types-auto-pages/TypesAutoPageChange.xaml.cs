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

namespace AppDataBaseView.pages.types_auto_pages
{
    /// <summary>
    /// Логика взаимодействия для TypesAutoPageChange.xaml
    /// </summary>
    public partial class TypesAutoPageChange : Page
    {
        public class ComboBoxItem_TypeAuto : ComboBoxItem 
        {
            public TypesAuto TALink { get; set; }
        }
        public TypesAutoPageChange()
        {
            InitializeComponent();
            GetData();
        }
        public static FormWindow formWindow { get; set; }

        private void typesAuto_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem_TypeAuto item = comboBox.SelectedItem as ComboBoxItem_TypeAuto;

            code_tb.Text     = item.TALink.AutoTypeCode.ToString();
            name_tb.Text     = item.TALink.Name.ToString();
            describe_tb.Text = item.TALink.Describe.ToString();
            
        }

        private void GetData() 
        {
            DataBaseContext Context = new DataBaseContext();
            foreach (TypesAuto ta_item in Context.TypesAutos.ToList<TypesAuto>()) 
            {
                typesAuto_cb.Items.Add(
                    new ComboBoxItem_TypeAuto()
                    {
                        Content = $"{ta_item.AutoTypeCode} - {ta_item.Name}",
                        TALink = ta_item
                    });
            }
        }

        private void change_btn_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();
            if (string.IsNullOrEmpty(code_tb.Text) || string.IsNullOrEmpty(name_tb.Text) ||
                string.IsNullOrEmpty(describe_tb.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены");
            }
            else 
            {
                Context.Update(new TypesAuto()
                {
                    AutoTypeCode = Convert.ToInt32(code_tb.Text),
                    Name = name_tb.Text,
                    Describe = describe_tb.Text
                });
                Context.SaveChanges();
                MessageBox.Show("Изменение прошло успешно");
                formWindow.Close();
            }
        }
    }
}
