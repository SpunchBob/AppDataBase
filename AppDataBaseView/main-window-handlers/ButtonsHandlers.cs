using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AppDataBaseView.Models;
using AppDataBaseView.Models;


namespace AppDataBaseView.MainWindowHandlers
{
    public class ButtonsHandlers
    {
        public static MainWindow? _window { get; set; }
        public static DataGrid? Data { get; set; }
        
        public static void WriteButton_OnClick(object sender, RoutedEventArgs eventArgs) 
        {
            if (_window != null)
            {
                if (!string.IsNullOrEmpty(_window.Current))
                {
                    FormWindow formWindow = Scripts.CreateFormWindow_add(_window.Current);
                    formWindow.Show();
                    Scripts.DisableAllButtons();
                }
                else MessageBox.Show("Выберите таблицу для добавления");
            }
        }

        public static void ChangeButton_OnClick(object sender, RoutedEventArgs eventArgs) 
        {
            if (_window != null) 
            {
                if (!string.IsNullOrEmpty(_window.Current))
                {
                    FormWindow formWindow = Scripts.CreateFormWindow_change(_window.Current);
                    formWindow.Show();
                    Scripts.DisableAllButtons();
                }
                else MessageBox.Show("Выберите таблицу для изменения");
            }
        }

        public static void DeleteButton_OnClick(object sender, RoutedEventArgs eventArgs) 
        {
            if (_window != null)
            {
                if (!string.IsNullOrEmpty(_window.Current))
                {
                    FormWindow formWindow = Scripts.CreateFormWindow_delete(_window.Current);
                    formWindow.Show();
                    Scripts.DisableAllButtons();
                }
                else MessageBox.Show("Выберите таблицу для изменения");
            }
        } 

        public static void ReadButton_OnClick(object sender, RoutedEventArgs eventArgs) 
        {
            DataBaseContext Context = new DataBaseContext();
            if (_window != null && Data != null)
            {
                switch (_window.Current)
                {
                    case "emp":
                        Data.ItemsSource = Context.Employees.ToList();
                        break;

                    case "fli":
                        Data.ItemsSource = Context?.Flights.ToList();
                        break;

                    case "loa":
                        Data.ItemsSource = Context?.Loads.ToList();
                        break;

                    case "tau":
                        Data.ItemsSource = Context.TypesAutos.ToList();
                        break;

                    case "tlo":
                        Data.ItemsSource = Context?.TypesLoads.ToList();
                        break;
                }
            }
        }

        public static void Selection_btn_Click(object sender, RoutedEventArgs e) 
        {
            FormWindow formWindow = Scripts.CreateSelectionWindowForm();
            formWindow.Show();
            Scripts.DisableAllButtons();
        }

        public static void join_btn(object sender, RoutedEventArgs e) 
        {
            using (DataBaseContext Context = new DataBaseContext()) 
            {
                var flightEmployeeJoin = Context.Flights
                                        .Join(Context.Employees,
                                            flight => flight.EmployeeCode,
                                            employee => employee.EmployeeCode,
                                            (flight, employee) => new
                                            {
                                                FlightCode = flight.FlightCode,
                                                Route = $"{flight.From} → {flight.Where}",
                                                Customer = flight.Customer,
                                                Price = flight.Price,
                                                EmployeeName = employee.Fcs,
                                                EmployeePhone = employee.Phonenumber,
                                                EmployeePosition = employee.Position
                                            })
                                        .ToList();
                Data.ItemsSource = flightEmployeeJoin;
            }
            MessageBox.Show("Соединение таблиц: EMPLOYEES INNER JOIN FLIGHTS");
        }
        public static void stats_btn(object sender, RoutedEventArgs e)
        {
            using (DataBaseContext Context = new DataBaseContext()) 
            {
                var routeStats = Context.Flights
                    .GroupBy(f => new { f.From, f.Where })
                    .Select(g => new
                    {
                        From = g.Key.From,
                        To = g.Key.Where,
                        FlightCount = g.Count(),
                        TotalRevenue = g.Sum(f => f.Price ?? 0),
                        AveragePrice = g.Average(f => f.Price ?? 0),
                        MostExpensive = g.Max(f => f.Price ?? 0),
                        Cheapest = g.Min(f => f.Price ?? 0)
                    })
                    .OrderByDescending(x => x.TotalRevenue)
                    .ToList();
                Data.ItemsSource = routeStats;
                MessageBox.Show("Общая информация по маршрутов");
            }
        }
    }
}
