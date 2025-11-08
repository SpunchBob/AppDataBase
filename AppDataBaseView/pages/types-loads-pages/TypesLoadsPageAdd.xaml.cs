using AppDataBaseView.Models;
using Microsoft.EntityFrameworkCore.Storage;
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

namespace AppDataBaseView.pages.types_loads_pages
{
    /// <summary>
    /// Логика взаимодействия для TypesLoadsPageAdd.xaml
    /// </summary>
    public partial class TypesLoadsPageAdd : Page
    {
        public static FormWindow formWindow { get; set; }
        public class ComboBoxItem_AutoType : ComboBoxItem 
        {
            public TypesAuto TypeLoadLink { get; set; } 
        }
        public TypesLoadsPageAdd()
        {
            InitializeComponent();
            GetData();
        }

        private void write_btn_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();
            ComboBoxItem_AutoType item = auto_type_code_cb.SelectedItem as ComboBoxItem_AutoType;
            Context.TypesLoads.Add(
                new TypesLoad() 
                {
                    LoadTypeCode = Convert.ToInt32(code_tb.Text),
                    Name = name_tb.Text,
                    Describe = describe_tb.Text,
                    AutoTypeCode = item.TypeLoadLink.AutoTypeCode,
                }
                );
            Context.SaveChanges();
            MessageBox.Show("Добавление прошло успешно");
            formWindow.Close();
            Scripts.EnableAllButtons();
        }

        private void GetData() 
        {
            DataBaseContext Context = new DataBaseContext();
            foreach (TypesAuto type in Context.TypesAutos.ToList<TypesAuto>()) 
            {
                auto_type_code_cb.Items.Add(
                    new ComboBoxItem_AutoType() 
                    {
                        Content = $"{type.AutoTypeCode} - {type.Name}",
                        TypeLoadLink = type
                    }
                    );
            }
        }
    }
}
