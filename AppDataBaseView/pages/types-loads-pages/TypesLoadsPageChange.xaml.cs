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
using static AppDataBaseView.pages.types_loads_pages.TypesLoadsPageAdd;

namespace AppDataBaseView.pages.types_loads_pages
{
    /// <summary>
    /// Логика взаимодействия для TypesLoadsPageChange.xaml
    /// </summary>
    public partial class TypesLoadsPageChange : Page
    {
        public static FormWindow formWindow { get; set; }
        public TypesLoadsPageChange()
        {
            InitializeComponent();
            GetData();
        }
        public class ComboBoxItem_AutoType : ComboBoxItem
        {
            public TypesAuto TypeAutoLink { get; set; }
        }

        public class ComboBoxItem_LoadType : ComboBoxItem 
        {
            public TypesLoad TypeLoadLink { get; set; }
        }

        private void typesLoad_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            ComboBox box = sender as ComboBox;
            ComboBoxItem_LoadType loadItem = box.SelectedItem as ComboBoxItem_LoadType;

            code_tb.Text = loadItem.TypeLoadLink.LoadTypeCode.ToString();
            name_tb.Text = loadItem.TypeLoadLink.Name.ToString();
            describe_tb.Text = loadItem.TypeLoadLink.Describe.ToString();

            foreach (ComboBoxItem_AutoType autoItem in auto_type_code_cb.Items) 
            {
                if (autoItem.TypeAutoLink.AutoTypeCode == loadItem.TypeLoadLink.AutoTypeCode) 
                {
                    auto_type_code_cb.SelectedItem = autoItem;
                }
            }
        }

        private void change_btn_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();
            ComboBoxItem_AutoType item = auto_type_code_cb.SelectedItem as ComboBoxItem_AutoType;
            Context.TypesLoads.Update(
                new TypesLoad() 
                {
                    LoadTypeCode = Convert.ToInt32(code_tb.Text),
                    Name = code_tb.Text,
                    Describe = describe_tb.Text,
                    AutoTypeCode = item.TypeAutoLink.AutoTypeCode,
                });
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
        private void GetData()
        {
            DataBaseContext Context = new DataBaseContext();
            foreach (TypesAuto type in Context.TypesAutos.ToList<TypesAuto>())
            {
                auto_type_code_cb.Items.Add(
                    new ComboBoxItem_AutoType()
                    {
                        Content = $"{type.AutoTypeCode} - {type.Name}",
                        TypeAutoLink = type
                    }
                    );
            }
            foreach (TypesLoad load in Context.TypesLoads.ToList<TypesLoad>()) 
            {
                typesLoad_cb.Items.Add(
                    new ComboBoxItem_LoadType() 
                    {
                        Content = $"{load.LoadTypeCode} - {load.Name}",
                        TypeLoadLink = load
                    }
                    );
            }
        }
    }
}
