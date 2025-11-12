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
using AppDataBaseView.Models;

namespace AppDataBaseView.pages.FlightsPages
{
    public partial class FlightsPageAdd : Page
    {
        public class ComboBoxItem_Load : ComboBoxItem
        {
            public Load LoadLink { get; set; }
        }
        public class ComboBoxItem_Employee : ComboBoxItem
        {
            public Employee EmployeeLink { get; set; }
        }
        public static FormWindow formWindow { get; set; }
        public FlightsPageAdd()
        {
            InitializeComponent();
            GetData();
        }
        public void WriteButton_Click(object sender, RoutedEventArgs e)
        {
            DataBaseContext Context = new DataBaseContext();
            
            ComboBoxItem_Load load = load_code_cb.SelectedItem as ComboBoxItem_Load;
            ComboBoxItem_Employee emp = employee_code_cb.SelectedItem as ComboBoxItem_Employee;

            if (string.IsNullOrEmpty(code_tb.Text) || string.IsNullOrEmpty(customer_tb.Text) || string.IsNullOrEmpty(from_tb.Text) ||
                string.IsNullOrEmpty(Where_tb.Text) || string.IsNullOrEmpty(send_date_tb.Text) || string.IsNullOrEmpty(arve_date_tb.Text) ||
                string.IsNullOrEmpty(price_tb.Text) || load_code_cb.SelectedItem == null || employee_code_cb.SelectedItem == null)
            {
                info_lb.Content = "Обязательно выберите код груза и код сотрудника";
            }
            else
            {
                Context.Flights.Add(
                        new Flight
                        {
                            FlightCode = Convert.ToInt32(code_tb.Text),
                            Customer = customer_tb.Text,
                            From = from_tb.Text,
                            Where = Where_tb.Text,
                            SendDate = send_date_tb.Text,
                            AriveData = arve_date_tb.Text,
                            LoadCode = load.LoadLink.LoadCode,
                            Price = Convert.ToInt32(price_tb.Text),
                            IsBought = is_bought_rb.IsChecked.ToString(),
                            IsRefund = is_refund_rb.IsChecked.ToString(),
                            EmployeeCode = emp.EmployeeLink.EmployeeCode,
                        }
                    );
                try
                {
                    Context.SaveChanges();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("ОШИБКА ДОБАВЛЕНИЯ");
                }
                System.Windows.MessageBox.Show("Добавление прошло успешно");
                formWindow.Close();
                Scripts.EnableAllButtons();
            }
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
        }
    }
}
