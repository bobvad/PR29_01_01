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

namespace PR29_Degtinnikov.Pages.Clubs
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        Models.Clubs Club;
        Pages.Clubs.Main Main;
        public Add(Pages.Clubs.Main Main, Models.Clubs Club = null)
        {
            InitializeComponent();
            this.Main = Main;
            this.Club = Club;
            if(Club != null)
            {
                this.Club = Club;
                this.Name.Text = Club.Name;
                this.Address.Text = Club.Address;
                this.WorkTime.Text = Club.WorkTime;
                BthAdd.Content = "Изменить";
            }
        }

        private void AddClub(object sender, RoutedEventArgs e)
        {
            if (Main == null)
            {
                var tempContext = new CompUsersContext();

                if (Club == null)
                {
                    Club = new Models.Clubs();
                    tempContext.Clubs.Add(Club);
                }

                Club.Name = this.Name.Text;
                Club.Address = this.Address.Text;
                Club.WorkTime = this.WorkTime.Text;

                tempContext.SaveChanges();
            }
            else
            {
                if (Club == null)
                {
                    Club = new Models.Clubs();
                    this.Main.AllClub.Clubs.Add(Club);
                }
                else
                {
                    Club.Name = this.Name.Text;
                    Club.Address = Address.Text;
                    Club.WorkTime = WorkTime.Text;
                }
                this.Main.AllClub.SaveChanges();
            }

            MainWindow.init.OpenPages(new Pages.Clubs.Main());
        }
    }
}
