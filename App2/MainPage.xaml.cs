using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App2.Annotations;
using App2.Entity;
using App2.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public static string DB_PATH =
            Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "User.sqlite"));
        
        public MainPage()
        {
        
        DataAccess.CreateDatabase(DB_PATH);
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private User _formUser = new User();

        public User FormUser
        {
            get => _formUser;
            set
            {
                if (_formUser != value)
                {
                    _formUser = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<User> _users = Model.UserModel.GetAllUsers(DB_PATH);
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged();
                }
            }
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            UserModel.AddUser(DB_PATH, _formUser);
        }

        private void User_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private void UserChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView list = (ListView) sender;
            User u = (User) list.SelectedItem;
            if (u != null)
            {
                nameDetail.Text = u.FullName;
                emailDetail.Text = u.Email;
                phoneDetail.Text = u.Phone;
                addressDetail.Text = u.Address;
                Detail.Visibility = Visibility.Visible;
            }
            
        }

       
        private void BtnSearch(object sender, RoutedEventArgs e)
        {
            Detail.Visibility = Visibility.Collapsed;
            string field = (string)((ComboBoxItem)Select.SelectedItem).Content;
            string key = Keyword.Text;
            UWPConsole.Console.WriteLine(field + ", " + key);
            Users = Model.UserModel.SearchUsers(DB_PATH, key, field);
        }
    }
}