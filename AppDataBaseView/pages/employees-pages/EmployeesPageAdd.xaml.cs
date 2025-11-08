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
using AppDataBaseView;

namespace AppDataBaseView.pages.EmployeesPages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPageAdd.xaml
    /// </summary>
    public partial class EmployeesPageAdd : Page
    {
        public static DataBaseContext Context = new DataBaseContext();
        public static FormWindow formWindow { get; set; }
        public EmployeesPageAdd()
        {
            InitializeComponent();
            SetCurrentPosiitons();
        }

        public void WriteButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(code_tb.Text) || string.IsNullOrEmpty(age_tb.Text) ||
                string.IsNullOrEmpty(fcs_tb.Text) || (((bool)is_male_rb.IsChecked) == false && ((bool)is_female_rb.IsChecked) == false) ||
                string.IsNullOrEmpty(addres_tb.Text) || string.IsNullOrEmpty(phone_tb.Text) ||
                string.IsNullOrEmpty(passport_tb.Text) || position_code_cb.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Все поля должны быть заполнены");
            }
            else 
            {
                Context.Employees.Add(
                    new Models.Employee()
                    {
                        EmployeeCode = Convert.ToInt32(code_tb.Text),
                        Fcs = fcs_tb.Text,
                        Age = Convert.ToInt32(age_tb.Text),
                        Gender = (bool)is_male_rb.IsChecked ? "М" : "Ж",
                        Addres = addres_tb.Text,
                        Phonenumber = phone_tb.Text,
                        Passport = passport_tb.Text,
                        Position = Convert.ToInt32(position_code_cb.SelectedItem)
                    }
                );
                try
                {
                    Context.SaveChanges();
                }
                catch 
                {
                    
                }
                Console.WriteLine(Context.Employees.Count());
                System.Windows.MessageBox.Show("Добавление прошло успешно");
                formWindow.Close();
                Scripts.EnableAllButtons();
            }

        }

        public void SetCurrentPosiitons()
        {
            position_code_cb.Items.Add(0);
            position_code_cb.Items.Add(1);
            position_code_cb.Items.Add(2);
            position_code_cb.Items.Add(3);
            position_code_cb.Items.Add(4);
        }
    }
}
