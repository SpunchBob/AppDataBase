using AppDataBaseView.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Логика взаимодействия для TypesAutoPage.xaml
    /// </summary>
    public partial class TypesAutoPage : Page
    {
        public static FormWindow formWindow { get; set; }
        public TypesAutoPage()
        {
            InitializeComponent();
        }

        private void write_btn_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();
            Context.Add(new TypesAuto()
            {
                AutoTypeCode = Convert.ToInt32(code_tb.Text),
                Name = name_tb.Text,
                Describe = describe_tb.Text
            });
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("ОШИБКА ДОБАВЛЕНИЯ");
            }
            MessageBox.Show("Добавление прошло успешно");
            formWindow.Close();
            Scripts.EnableAllButtons();
        }
    }
}
