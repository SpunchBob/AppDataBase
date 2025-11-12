using AppDataBaseView;
using AppDataBaseView.Models;
using Microsoft.EntityFrameworkCore;
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

namespace AppDataBaseView.pages.EmployeesPages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPageDelete.xaml
    /// </summary>
    public partial class EmployeesPageDelete : Page
    {
        Employee employee;
        public static FormWindow formWindow { get; set; }
        public class ComboBoxItem_Custom : ComboBoxItem
        {
            public Employee EmployeeLink { get; set; }
        }
        public EmployeesPageDelete()
        {
            InitializeComponent();
            GetEmployees();
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
                            Content = emp.Fcs,
                            EmployeeLink = emp
                        }
                    );
            }
        }

        public void EmployeeComboBox_SelctionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem_Custom item = comboBox.SelectedItem as ComboBoxItem_Custom;
            employee = item.EmployeeLink;
        }
        public void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if (employee == null)
            {
                MessageBox.Show("Не выбрана запись для удаления");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Вы уверены, что хотите удалить этого сотрудника?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (DataBaseContext Context = new DataBaseContext())
                    {
                        var employeeWithFlights = Context.Employees
                            .Include(e => e.Flights)
                            .FirstOrDefault(e => e.EmployeeCode == employee.EmployeeCode);

                        if (employeeWithFlights != null)
                        {
                            Context.Employees.Remove(employeeWithFlights);

                            try
                            {
                                Context.SaveChanges();
                            }
                            catch (Exception ex) 
                            {
                                MessageBox.Show(ex.Message);
                            }

                            MessageBox.Show("Сотрудник успешно удален");
                            formWindow.Close();
                            Scripts.EnableAllButtons();
                        }
                        else
                        {
                            MessageBox.Show("Запись не найдена в базе данных");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                }
            }
        }
    }
}
