using AppDataBaseView.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppDataBaseView.MainWindowHandlers
{
    public class ListBoxItemsHandlers
    {
        public static DataBaseContext? Context { get; set; }
        public static Window? Window { get; set; }
        public static DataGrid? Data { get; set; }

        public static List<MouseButtonEventHandler> GetHandlers()
        {
            List<MouseButtonEventHandler> handlers = new List<MouseButtonEventHandler>()
            {
                EmployeesListBoxItem_OnDoubleClick,
                FlightsListBoxItem_OnDoubleClick,
                LoadsListBoxItem_OnDoubleClick,
                TypesAutoListBoxItem_OnDoubleClick,
                TypesLoadsListBoxItem_OnDoubleClick
            };

            return handlers;
        }

        private static void EmployeesListBoxItem_OnDoubleClick(object? sender, MouseButtonEventArgs eventArgs) 
        {
            if (Data != null && Context != null)
                Data.ItemsSource = Context.Employees.ToList();
        }

        private static void FlightsListBoxItem_OnDoubleClick(object sender, MouseButtonEventArgs eventArgs)
        {
            if (Data != null && Context != null)
                Data.ItemsSource = Context.Flights.ToList();
        }

        private static void LoadsListBoxItem_OnDoubleClick(object sender, MouseButtonEventArgs eventArgs)
        {
            if (Data != null && Context != null)
                Data.ItemsSource = Context.Loads.ToList();
        }

        private static void TypesAutoListBoxItem_OnDoubleClick(object sender, MouseButtonEventArgs eventArgs)
        {
            if (Data != null && Context != null)
                Data.ItemsSource = Context.TypesAuto.ToList();
        }

        private static void TypesLoadsListBoxItem_OnDoubleClick(object sender, MouseButtonEventArgs eventArgs)
        {
            if (Data != null && Context != null)
                Data.ItemsSource = Context.TypesLoads.ToList();
        }

    }
}
