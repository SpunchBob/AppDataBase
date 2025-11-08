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
        public SelectionPage()
        {
            InitializeComponent();
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
            ;
        }
    }
}
