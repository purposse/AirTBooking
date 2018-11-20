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
using System.Windows.Shapes;

namespace lebedev
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private Dictionary<string, string> Users;

        public LoginForm()
        {
            InitializeComponent();
            Users = new Dictionary<string, string>
            {
                {"admin","admin"},
                { "user","user"}
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isAuth = false;
            Dictionary<string, string> authUser = new Dictionary<string, string>();
            foreach(var user in Users)
            {
                if (TextBox1.Text == user.Key && TextBox2.Password == user.Value)
                {
                    MessageBox.Show(String.Format("Добро пожаловать, {0}.", user.Value));
                    authUser.Add(user.Key, user.Value);
                    isAuth = true;
                }
            }
            if (isAuth)
            {
                if (authUser.FirstOrDefault().Value == "admin")
                {
                    AdminWindow AdminWindow = new AdminWindow();
                    AdminWindow.Show();
                    Close();
                }
                else
                {
                    MainWindow MainWindow = new MainWindow();
                    MainWindow.Show();
                    Close();
                }
            }
            else
                MessageBox.Show("Неверный логин и(или) пароль!");
        }
    }
}
