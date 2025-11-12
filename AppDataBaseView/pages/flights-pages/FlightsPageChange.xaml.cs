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

namespace AppDataBaseView.pages.FlightsPages
{
    /// <summary>
    /// Логика взаимодействия для FlightsPageChange.xaml
    /// </summary>
    public partial class FlightsPageChange : Page
    {
        public class ComboBoxItem_Load : ComboBoxItem
        {
            public Load LoadLink { get; set; }
        }
        public class ComboBoxItem_Employee : ComboBoxItem
        {
            public Employee EmployeeLink { get; set; }
        }
        public class ComboBoxItem_Flight : ComboBoxItem
        {
            public Flight FlightLink { get; set; }
        }
        public static FormWindow formWindow { get; set; }

        public FlightsPageChange()
        {
            InitializeComponent();
            GetData();
        }
        private void GetData()
        {
            DataBaseContext Context = new DataBaseContext();

            foreach (Load load in Context.Loads.ToList<Load>())
            {
                load_code_cb.Items.Add(
                        new ComboBoxItem_Load()
                        {
                            Content = $"{load.LoadCode} - {load.Name}",
                            LoadLink = load
                        }
                    );
            }

            foreach (Employee emp in Context.Employees.ToList<Employee>())
            {
                employee_code_cb.Items.Add(
                        new ComboBoxItem_Employee()
                        {
                            Content = $"{emp.EmployeeCode} - {emp.Fcs}",
                            EmployeeLink = emp
                        }
                    );
            }

            foreach (Flight fli in Context.Flights.ToList<Flight>())
            {
                employees_cb.Items.Add(
                        new ComboBoxItem_Flight()
                        {
                            Content = $"{fli.FlightCode} - {fli.Customer}",
                            FlightLink = fli
                        }
                    );
            }
        }
        private void FlightsComboBox_SelctionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();
            System.Windows.Controls.ComboBox comboBox = sender as System.Windows.Controls.ComboBox;
            ComboBoxItem_Flight flightItem = comboBox.SelectedItem as ComboBoxItem_Flight;
            
                code_tb.Text        = flightItem.FlightLink.FlightCode.ToString();
                customer_tb.Text    = flightItem.FlightLink.Customer.ToString(); 
                from_tb.Text        = flightItem.FlightLink.From.ToString();
                Where_tb.Text       = flightItem.FlightLink.Where.ToString();
                send_date_tb.Text   = flightItem.FlightLink.SendDate.ToString();
                arve_date_tb.Text   = flightItem.FlightLink.AriveData.ToString();
                price_tb.Text       = flightItem.FlightLink?.Price.ToString();

                if (flightItem.FlightLink.IsBought == true.ToString())
                    is_bought_rb.IsChecked = true;

                if (flightItem.FlightLink.IsRefund == true.ToString()) 
                    is_refund_rb.IsChecked = true;


                foreach (ComboBoxItem_Load loadItem in load_code_cb.Items) 
                {
                    if (loadItem.LoadLink.LoadCode == flightItem.FlightLink.LoadCode) 
                    { 
                        load_code_cb.SelectedItem = loadItem;
                    }
                }

                foreach (ComboBoxItem_Employee employeeItem in employee_code_cb.Items)
                {
                    if (employeeItem.EmployeeLink.EmployeeCode == flightItem.FlightLink.EmployeeCode)
                    {
                        employee_code_cb.SelectedItem = employeeItem;
                    }
                }
            
        }

        private void change_btn_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();

            ComboBoxItem_Load loadItem = load_code_cb.SelectedItem as ComboBoxItem_Load;
            ComboBoxItem_Employee employeeItem = employee_code_cb.SelectedItem as ComboBoxItem_Employee;

            if (string.IsNullOrEmpty(code_tb.Text) || string.IsNullOrEmpty(customer_tb.Text) || string.IsNullOrEmpty(from_tb.Text) ||
                string.IsNullOrEmpty(Where_tb.Text) || string.IsNullOrEmpty(send_date_tb.Text) || string.IsNullOrEmpty(arve_date_tb.Text) ||
                string.IsNullOrEmpty(price_tb.Text) || load_code_cb.SelectedItem == null || employee_code_cb.SelectedItem == null)
            {
                info_lb.Content = "Обязательно выберите изменяемую запись";
            }
            else
            {
                Context.Update(
                new Flight()
                {
                    FlightCode = Convert.ToInt32(code_tb.Text),
                    Customer = customer_tb.Text,
                    From = from_tb.Text,
                    Where = Where_tb.Text,
                    SendDate = send_date_tb.Text,
                    AriveData = arve_date_tb.Text,
                    LoadCode = loadItem.LoadLink.LoadCode,
                    Price = Convert.ToInt32(price_tb.Text),
                    IsBought = is_bought_rb.IsChecked.ToString(),
                    IsRefund = is_refund_rb.IsChecked.ToString(),
                    EmployeeCode = employeeItem.EmployeeLink.EmployeeCode
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
