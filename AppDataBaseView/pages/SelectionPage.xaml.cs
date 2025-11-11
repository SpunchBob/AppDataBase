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

namespace AppDataBaseView.pages
{
    /// <summary>
    /// Логика взаимодействия для SelectionPage.xaml
    /// </summary>
    public partial class SelectionPage : Page
    {
        public class ComboBoxItem_Emp : ComboBoxItem {
            
            public Employee link { get; set; }

        }
        public SelectionPage()
        {
            InitializeComponent();
            GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (DataBaseContext Context = new DataBaseContext())
            {
                if (selection_cb.SelectedIndex == 0) 
                {
                    var flight = Context.Flights
                        .Where(f => f.Price >= 
                                    Convert.ToInt32(selection_tb.Text));
                    data.ItemsSource = flight.ToList();
                }

                if (selection_cb.SelectedIndex == 1) 
                {
                    var flight = Context.Flights
                        .Where(f => f.Price <=
                                    Convert.ToInt32(selection_tb.Text));
                    data.ItemsSource = flight.ToList();
                }
            }
        }
        private void GetData() 
        {
            using (DataBaseContext Context = new DataBaseContext()) 
            {
                foreach (Employee employee in Context.Employees.ToList()) 
                {
                    selection_emp_cb.Items.Add(
                            new ComboBoxItem_Emp()
                            {
                                Content = $"{employee.EmployeeCode} - {employee.Fcs}",
                                link = employee
                            }
                        );
                }
            }
        }
        private void Button_Emp_Click(object sender, RoutedEventArgs e) 
        {
            using (DataBaseContext Context = new DataBaseContext())
            {
                if (selection_emp_cb.SelectedItem == null)
                {
                    MessageBox.Show("Выберите пользователя для совершения выборки!");
                }
                else 
                {
                    ComboBoxItem_Emp item = selection_emp_cb.SelectedItem as ComboBoxItem_Emp;
                    var flights = Context.Flights
                        .Where(f => f.EmployeeCode == item.link.EmployeeCode);
                    data_emp.ItemsSource = flights.ToList();
                }
            }
        }

        private void Button_Date_Click(object sender, RoutedEventArgs e) 
        {
            using (DataBaseContext Context = new DataBaseContext()) 
            {
                if (string.IsNullOrEmpty((selection_date_cb.Text)))
                {
                    MessageBox.Show("Выберете дату для совершеня выборки!");
                }
                else 
                {
                    var flights = Context.Flights
                        .Where(f => DateTime.Parse(f.SendDate) == DateTime.Parse(selection_date_cb.Text));
                    data_date.ItemsSource = flights.ToList();
                }
            }
        }
    }
}
    