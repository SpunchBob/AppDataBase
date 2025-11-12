using Microsoft.EntityFrameworkCore;
using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDataBaseView
{
    public class LinkedListBoxItem : ListBoxItem 
    {
        public string Link { get; set; }
    }

    public static class Scripts
    {
        public static MainWindow MainWindow { get; set; }
        public static void AddListBoxItemsHandlers(System.Windows.Controls.ListBox? list, List<MouseButtonEventHandler>? pointers, RoutedEventHandler handler) 
        {
            if (list != null && pointers != null)
            { 
                for (int index = 0; index < list.Items.Count; index++)
                {
                    ListBoxItem item = (ListBoxItem) list.Items[index];
                    item.MouseDoubleClick += pointers[index];
                    item.Selected += handler;
                }
            }
        }

        public static void AppendTablesList(ListBox list, MainWindow window) 
        {
            List<string> names = new List<string>() 
            {
                "Сотрудники",
                "Перевозки",
                "Загрузки",
                "Типы авто",
                "Типы грузов"
            };

            List<string> links = new List<string>()
            {
                "emp",
                "fli",
                "loa",
                "tau",
                "tlo"
            };

            for (int index = 0; index < 5; index++) 
            {
                StackPanel stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                };

                CheckBox checkBox = new CheckBox() 
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                checkBox.Checked += MainWindowHandlers.CheckBoxHandlers.CheckBox_Checked;
                checkBox.Unchecked += MainWindowHandlers.CheckBoxHandlers.CheckBox_Unchecked;

                Label label = new Label()
                {
                    Content = names[index]
                };

                stackPanel.Children.Add(checkBox);
                stackPanel.Children.Add(label);

                LinkedListBoxItem item = new LinkedListBoxItem()
                {
                    Content = stackPanel,
                    FontSize = 16,
                    Link = links[index]
                };

                list.Items.Add(item);
            }
            
        }

        public static void SetCurrent(ListBox list, MainWindow window, CheckBox checkBox) 
        {
            if (list != null)
            {
                foreach (LinkedListBoxItem item in list.Items) 
                {
                    StackPanel stackPanel = (StackPanel)item.Content;
                    if (stackPanel.Children.Contains(checkBox)) 
                    {
                        window.Current = item.Link;
                        break;
                    }
                }
            }
        }
        
        public static void DisableAnother(ListBox list, CheckBox checkBox) 
        {
            if (list != null) 
            {
                foreach (ListBoxItem item in list.Items) 
                {
                    StackPanel stackPanel = (StackPanel)item.Content;
                    if (stackPanel.Children.Contains(checkBox))
                    {
                        continue;
                    }

                    CheckBox box = (CheckBox)stackPanel.Children[0];
                    box.IsEnabled = false;
                }
            }
        }

        public static void DisableAllButtons() 
        {
            MainWindow.writeButton.IsEnabled    = false;
            MainWindow.changeButton.IsEnabled   = false;
            MainWindow.deleteButton.IsEnabled   = false;
            MainWindow.selection_btn.IsEnabled  = false;
            MainWindow.union_btn.IsEnabled      = false;
            MainWindow.function_btn.IsEnabled   = false;
        }

        public static void EnableAllButtons()
        {
            MainWindow.writeButton.IsEnabled    = true;
            MainWindow.changeButton.IsEnabled   = true;
            MainWindow.deleteButton.IsEnabled   = true;
            MainWindow.selection_btn.IsEnabled  = true;
            MainWindow.union_btn.IsEnabled      = true;
            MainWindow.function_btn.IsEnabled   = true;
        }

        public static void EnableAll(ListBox list) 
        {
            if (list != null)
            {
                foreach (ListBoxItem item in list.Items)
                {
                    StackPanel stackPanel = (StackPanel)item.Content;
                    CheckBox box = (CheckBox)stackPanel.Children[0];
                    box.IsEnabled = true;
                }
            }
        }

        public static FormWindow CreateFormWindow_change(string token) 
        {
            FormWindow window = FormWindow.getInstance();

            Grid grid = new Grid() 
            {
            
            };

            Frame mainFrame = new Frame()
            {
                Background = new SolidColorBrush(Colors.Transparent)
            };
            if (string.IsNullOrEmpty(token)) 
            {
            
            }
            switch (token) 
            {
                case "emp":
                    mainFrame.Navigate(new pages.EmployeesPages.EmployeesPageChange());
                    pages.EmployeesPages.EmployeesPageChange.formWindow = window; 
                    break;
                case "fli":
                    mainFrame.Navigate(new pages.FlightsPages.FlightsPageChange());
                    pages.FlightsPages.FlightsPageChange.formWindow = window;
                    break;
                case "loa":
                    mainFrame.Navigate(new pages.loads_pages.LoadsPageChange());
                    pages.loads_pages.LoadsPageChange.formWindow = window;
                    break;
                case "tau":
                    mainFrame.Navigate(new pages.types_auto_pages.TypesAutoPageChange());
                    pages.types_auto_pages.TypesAutoPageChange.formWindow = window;
                    break;
                case "tlo":
                    mainFrame.Navigate(new pages.types_loads_pages.TypesLoadsPageChange());
                    pages.types_loads_pages.TypesLoadsPageChange.formWindow = window;
                    break;
            }

            grid.Children.Add(mainFrame);

            window.Content = grid;
            return window;
        }

        public static FormWindow CreateFormWindow_add(string token) 
        {
            //FormWindow window = new FormWindow() 
            //{
            //    Width = 440,
            //    Height = 600,
            //};

            FormWindow window = FormWindow.getInstance();

            Grid grid = new Grid() 
            {

            };
            
            Frame mainFrame = new Frame()
            {
                Background = new SolidColorBrush(Colors.Transparent),
            };

            switch (token) 
            {
                case "emp":
                    mainFrame.Navigate(new pages.EmployeesPages.EmployeesPageAdd());
                    pages.EmployeesPages.EmployeesPageAdd.formWindow = window;
                    break;
                case "fli":
                    mainFrame.Navigate(new pages.FlightsPages.FlightsPageAdd());
                    pages.FlightsPages.FlightsPageAdd.formWindow = window;
                    break;
                case "loa":
                    mainFrame.Navigate(new pages.loads_pages.LoadsPageAdd());
                    pages.loads_pages.LoadsPageAdd.formWindow = window; 
                    break;
                case "tau":
                    mainFrame.Navigate(new pages.types_auto_pages.TypesAutoPage());
                    pages.types_auto_pages.TypesAutoPage.formWindow = window;
                    break;
                case "tlo":
                    mainFrame.Navigate(new pages.types_loads_pages.TypesLoadsPageAdd());
                    pages.types_loads_pages.TypesLoadsPageAdd.formWindow = window;
                    break;
            }

            grid.Children.Add(mainFrame);

            window.Content = grid;
            return window;
        }

        public static FormWindow CreateFormWindow_delete(string token) 
        {
            FormWindow window = FormWindow.getInstance();

            window.Width = 440;
            window.Height = 260;

            Grid grid = new Grid()
            {

            };

            Frame mainFrame = new Frame()
            {
                Background = new SolidColorBrush(Colors.Transparent)
            };

            switch (token)
            {
                case "emp":
                    mainFrame.Navigate(new pages.EmployeesPages.EmployeesPageDelete());
                    pages.EmployeesPages.EmployeesPageDelete.formWindow = window;
                    break;
                case "fli":
                    mainFrame.Navigate(new pages.FlightsPages.FlightsPageDelete());
                    pages.FlightsPages.FlightsPageDelete.formWindow = window;
                    break;
                case "loa":
                    mainFrame.Navigate(new pages.loads_pages.LoadsPageDelete());
                    pages.loads_pages.LoadsPageDelete.formWindow = window;
                    break;
                case "tau":
                    mainFrame.Navigate(new pages.types_auto_pages.TypesAutoPageDelete());
                    pages.types_auto_pages.TypesAutoPageDelete.formWindow = window;
                    break;
                case "tlo":
                    mainFrame.Navigate(new pages.types_loads_pages.TypesLoadsPageDelete());
                    pages.types_loads_pages.TypesLoadsPageDelete.formWindow = window;
                    break;
            }

            grid.Children.Add(mainFrame);

            window.Content = grid;
            return window;
        }

        public static FormWindow CreateSelectionWindowForm() 
        {
            FormWindow window = FormWindow.getInstance();

            window.Height = 600;
            window.Width = 800;

            Grid grid = new Grid()
            {
            };

            Frame frame = new Frame()
            {
                Background = new SolidColorBrush(Colors.Transparent)
            };
            frame.Navigate(new pages.SelectionPage());
            grid.Children.Add(frame);
            window.Content = grid;
            return window;
        }  
    }
}
