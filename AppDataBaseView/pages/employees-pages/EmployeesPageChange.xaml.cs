using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
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
using AppDataBaseView.Models;
using Microsoft.EntityFrameworkCore;

namespace AppDataBaseView.pages.EmployeesPages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPageChange.xaml
    /// </summary>

    public partial class EmployeesPageChange : Page
    {
        public class ComboBoxItem_Custom : ComboBoxItem
        {
            public Employee EmployeeLink { get; set; }
        }
        public static FormWindow formWindow { get; set; }
        public EmployeesPageChange()
        {
            InitializeComponent();
            GetEmployees();
            SetCurrentPositions();
        }
        private void GetEmployees()
        {
            DataBaseContext Context = new DataBaseContext();
            List<Employee> list = Context.Employees.ToList<Employee>();
            foreach (Employee emp in list)
            {
                employees_cb.Items.Add(
                        new ComboBoxItem_Custom()
                        {
                            Content = $"{emp.EmployeeCode} - {emp.Fcs}",
                            EmployeeLink = emp
                        }
                    );
            }
        }
        public void EmployeeComboBox_SelctionChanged(object sender, RoutedEventArgs e) 
        {
            System.Windows.Controls.ComboBox box = sender as System.Windows.Controls.ComboBox;
            ComboBoxItem_Custom item = box.SelectedItem as ComboBoxItem_Custom;

            code_tb.Text = item.EmployeeLink.EmployeeCode.ToString();
            age_tb.Text = item.EmployeeLink?.Age.ToString();
            fcs_tb.Text = item.EmployeeLink?.Fcs.ToString();

            if (item.EmployeeLink.Gender == "М" || item.EmployeeLink.Gender == "M")
            {
                is_male_rb.IsChecked = true;
            }
            if (item.EmployeeLink.Gender == "Ж")
            {
                is_female_rb.IsChecked = true;
            }

            addres_tb.Text = item.EmployeeLink.Addres;
            phone_tb.Text = item.EmployeeLink.Phonenumber;
            passport_tb.Text = item.EmployeeLink.Passport;
            position_code_cb.SelectedItem = item.EmployeeLink.Position;
        }

        public void SetCurrentPositions()
        {
            position_code_cb.Items.Add(0);
            position_code_cb.Items.Add(1);
            position_code_cb.Items.Add(2);
            position_code_cb.Items.Add(3);
            position_code_cb.Items.Add(4);
        }

        private void change_btn_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();

            if (string.IsNullOrEmpty(code_tb.Text) || string.IsNullOrEmpty(age_tb.Text) ||
                string.IsNullOrEmpty(fcs_tb.Text) || (((bool)is_male_rb.IsChecked) == false && ((bool)is_female_rb.IsChecked) == false) ||
                string.IsNullOrEmpty(addres_tb.Text) || string.IsNullOrEmpty(phone_tb.Text) ||
                string.IsNullOrEmpty(passport_tb.Text) || position_code_cb.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Все поля должны быть заполнены");
            }
            else 
            {
                Context.Employees.Update(
                    new Employee()
                    {
                        EmployeeCode = Convert.ToInt32(code_tb.Text),
                        Fcs = fcs_tb.Text,
                        Age = Convert.ToInt32(age_tb.Text),
                        Gender = is_male_rb.IsChecked == true ? "М" : "Ж",
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
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
                System.Windows.MessageBox.Show("Изменение прошло успешно");
                formWindow.Close();
                Scripts.EnableAllButtons();
            }
        }
    }
}
