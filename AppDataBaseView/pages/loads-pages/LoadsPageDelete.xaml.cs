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
using static AppDataBaseView.pages.loads_pages.LoadsPageChange;

namespace AppDataBaseView.pages.loads_pages
{
    /// <summary>
    /// Логика взаимодействия для LoadsPageDelete.xaml
    /// </summary>
    public partial class LoadsPageDelete : Page
    {
        public class ComboBoxItem_Load : ComboBoxItem
        {
            public Load LoadLink { get; set; }
        }
        public static FormWindow formWindow { get; set; }
        public LoadsPageDelete()
        {
            InitializeComponent();
            GetData();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem_Load item = loads_cb.SelectedItem as ComboBoxItem_Load;

            if (item?.LoadLink == null)
            {
                MessageBox.Show("Не выбрана запись для удаления");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Вы уверены, что хотите удалить этот груз?",
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
                        var loadWithFlights = Context.Loads
                            .Include(l => l.Flights)
                            .FirstOrDefault(l => l.LoadCode == item.LoadLink.LoadCode);

                        if (loadWithFlights != null)
                        {
                            Context.Loads.Remove(loadWithFlights);
                            try
                            {
                                Context.SaveChanges();
                            }
                            catch (Exception ex) 
                            {
                                MessageBox.Show(ex.Message);
                            }

                            MessageBox.Show("Груз успешно удален");
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
        private void GetData()
        {
            DataBaseContext Context = new DataBaseContext();

            foreach (Load load in Context.Loads.ToList<Load>())
            {
                loads_cb.Items.Add(
                        new ComboBoxItem_Load()
                        {
                            Content = $"{load.LoadCode} - {load.Name}",
                            LoadLink = load
                        }
                    );
            }
        }
    }
}
