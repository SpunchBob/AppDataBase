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

namespace AppDataBaseView.pages.types_auto_pages
{
    /// <summary>
    /// Логика взаимодействия для TypesAutoPageDelete.xaml
    /// </summary>
    public partial class TypesAutoPageDelete : Page
    {
        public TypesAutoPageDelete()
        {
            InitializeComponent();
            GetData();
        }

        public class ComboBoxItem_TypeAuto : ComboBoxItem
        {
            public TypesAuto TALink { get; set; }
        }

        public static FormWindow formWindow { get; set; }
        private void GetData()
        {
            DataBaseContext Context = new DataBaseContext();
            foreach (TypesAuto ta_item in Context.TypesAutos.ToList<TypesAuto>())
            {
                typeAuto_cb.Items.Add(
                    new ComboBoxItem_TypeAuto()
                    {
                        Content = $"{ta_item.AutoTypeCode} - {ta_item.Name}",
                        TALink = ta_item
                    });
            }
        }
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem_TypeAuto item = typeAuto_cb.SelectedItem as ComboBoxItem_TypeAuto;

            // Проверка выбранного элемента
            if (item?.TALink == null)
            {
                MessageBox.Show("Не выбрана запись для удаления");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Вы уверены, что хотите удалить этот тип авто?",
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
                        // Находим запись в контексте БД
                        var typesAutoToDelete = Context.TypesAutos
                            .FirstOrDefault(ta => ta.AutoTypeCode == item.TALink.AutoTypeCode);

                        if (typesAutoToDelete != null)
                        {
                            Context.TypesAutos.Remove(typesAutoToDelete);
                            try
                            {
                                Context.SaveChanges();
                            }
                            catch (Exception ex) 
                            {
                                MessageBox.Show(ex.Message);
                            }

                            MessageBox.Show("Тип авто успешно удален");
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
