using AppDataBaseView.Models;
using System.Windows;
using System.Windows.Controls;

namespace AppDataBaseView.MainWindowHandlers
{
    public static class CheckBoxHandlers
    {
        public static MainWindow? Window { get; set; }
        public static void CheckBox_Checked(object sender, RoutedEventArgs eventArgs) 
        {
            CheckBox box = (CheckBox)sender;
            if (Window != null)
            {
                Scripts.SetCurrent(Window.tablesListBox, Window, box);
                Scripts.DisableAnother(Window.tablesListBox, box);

                using (DataBaseContext Context = new DataBaseContext()) 
                {
                    foreach (LinkedListBoxItem item in Window.tablesListBox.Items) 
                    {
                        StackPanel panel = item.Content as StackPanel;
                        if (panel.Children[0] == box) 
                        {
                            switch (item.Link) 
                            {
                                case "emp":
                                    Window.table_info_tblock.Text = $"Employees: Записей: {Context.Employees.Count<Employee>()}";
                                    break;
                                case "fli":
                                    Window.table_info_tblock.Text = $"Flights: Записей: {Context.Flights.Count<Flight>()}";
                                    break;
                                case "loa":
                                    Window.table_info_tblock.Text = $"Loads: Записей: {Context.Loads.Count<Load>()}";
                                    break;
                                case "tlo":
                                    Window.table_info_tblock.Text = $"TypesLoads: Записей: {Context.TypesLoads.Count<TypesLoad>()}";
                                    break;
                                case "tau":
                                    Window.table_info_tblock.Text = $"TypesAuto: Записей: {Context.TypesAutos.Count<TypesAuto>()}";
                                    break;
                            }
                        }
                    }
                }

            }
        }

        public static void CheckBox_Unchecked(object sender, RoutedEventArgs eventArgs) 
        {
            if (Window != null)
            {
                Scripts.EnableAll(Window.tablesListBox);
                Window.Current = "";
                Window.table_info_tblock.Text = "";
            }
        }
    }
}
