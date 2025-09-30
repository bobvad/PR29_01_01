using PR29_Degtinnikov.Classes;
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

namespace PR29_Degtinnikov.Pages.Users
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Classes.CompUsersContext AllClub = new CompUsersContext();
        Models.Users User;
        Pages.Users.Main Main;

        public Add(Pages.Users.Main Main, Models.Users User = null)
        {
            InitializeComponent();
            this.Main = Main;
            foreach (Models.Clubs Club in AllClub.Clubs)
                Clubs.Items.Add(Club.Name);

            Clubs.Items.Add("Выберите ...");

            if (User != null)
            {
                this.User = User;
                this.FIO.Text = User.FIO;
                this.RentStart.Text = User.RentStart.ToString("dd.MM.yyyy");
                this.RentTime.Text = User.RentStart.ToString("HH:mm");
                this.Duration.Text = User.Duration.ToString();
                Clubs.SelectedItem = AllClub.Clubs.Where(x => x.Id == User.IdClub).First().Name;
                BthAdd.Content = "Изменить";
            }
        }

        private void AddUser(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!ValidateUserFields())
                return;

            DateTime DIRentStart = new DateTime();

            if (!DateTime.TryParse(this.RentStart.Text, out DIRentStart))
            {
                MessageBox.Show("Некорректный формат даты!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TimeSpan rentTime;
            if (!TimeSpan.TryParse(this.RentTime.Text, out rentTime))
            {
                MessageBox.Show("Некорректный формат времени!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DIRentStart = DIRentStart.Add(rentTime);

            int duration;
            if (!int.TryParse(this.Duration.Text, out duration) || duration <= 0)
            {
                MessageBox.Show("Продолжительность должна быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new CompUsersContext())
                {
                    if (this.User == null)
                    {
                        User = new Models.Users();
                        User.FIO = this.FIO.Text.Trim();
                        User.RentStart = DIRentStart;
                        User.Duration = duration;
                        var selectedClub = context.Clubs.FirstOrDefault(x => x.Name == Clubs.SelectedItem.ToString());
                        if (selectedClub != null)
                        {
                            User.IdClub = selectedClub.Id;
                            context.Users.Add(User);
                        }
                        else
                        {
                            MessageBox.Show("Выбранный клуб не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                    {
                        var existingUser = context.Users.Find(this.User.Id);
                        if (existingUser != null)
                        {
                            existingUser.FIO = this.FIO.Text.Trim();
                            existingUser.RentStart = DIRentStart;
                            existingUser.Duration = duration;

                            var selectedClub = context.Clubs.FirstOrDefault(x => x.Name == Clubs.SelectedItem.ToString());
                            if (selectedClub != null)
                            {
                                existingUser.IdClub = selectedClub.Id;
                            }
                            else
                            {
                                MessageBox.Show("Выбранный клуб не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                    }

                    context.SaveChanges();
                    MessageBox.Show("Пользователь успешно сохранен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow.init.OpenPages(new Pages.Users.Main());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateUserFields()
        {
            if (string.IsNullOrWhiteSpace(FIO.Text))
            {
                MessageBox.Show("Введите ФИО пользователя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                FIO.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(RentStart.Text))
            {
                MessageBox.Show("Введите дату аренды!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                RentStart.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(RentTime.Text))
            {
                MessageBox.Show("Введите время аренды!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                RentTime.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Duration.Text))
            {
                MessageBox.Show("Введите продолжительность!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Duration.Focus();
                return false;
            }

            if (Clubs.SelectedItem == null || Clubs.SelectedItem.ToString() == "Выберите ...")
            {
                MessageBox.Show("Выберите клуб!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Clubs.Focus();
                return false;
            }

            return true;
        }
    }
}
