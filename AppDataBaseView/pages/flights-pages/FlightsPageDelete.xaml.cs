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
using static AppDataBaseView.pages.FlightsPages.FlightsPageChange;

namespace AppDataBaseView.pages.FlightsPages
{
    /// <summary>
    /// Логика взаимодействия для FlightsPageDelete.xaml
    /// </summary>
    public partial class FlightsPageDelete : Page
    {
        public static FormWindow formWindow { get; set; }
        public class ComboBoxItem_Flight : ComboBoxItem
        {
            public Flight FlightLink { get; set; }
        }
        public FlightsPageDelete()
        {
            InitializeComponent();
            GetData();
        }
        private void GetData() 
        {
            DataBaseContext Context = new DataBaseContext();
            foreach (Flight fli in Context.Flights.ToList<Flight>())
            {
                flights_cb.Items.Add(
                        new ComboBoxItem_Flight()
                        {
                            Content = $"{fli.FlightCode} - {fli.Customer}",
                            FlightLink = fli
                        }
                    );
            }
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem_Flight item = flights_cb.SelectedItem as ComboBoxItem_Flight;

            if (item?.FlightLink == null)
            {
                MessageBox.Show("Не выбрана запись для удаления");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Вы уверены, что хотите удалить этот рейс?",
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
                        var flight = Context.Flights
                            .FirstOrDefault(f => f.FlightCode == item.FlightLink.FlightCode);

                        if (flight != null)
                        {
                            Context.Flights.Remove(flight);
                            try
                            {
                                Context.SaveChanges();
                            }
                            catch (Exception ex) 
                            {
                                MessageBox.Show(ex.Message);
                            }

                            MessageBox.Show("Рейс успешно удален");
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
