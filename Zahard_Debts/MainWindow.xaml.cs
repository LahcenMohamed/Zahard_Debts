using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Zahard_Debts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Setlang(Properties.Settings.Default.langs);

        }




        private void show_pass_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordbox_text.Visibility = Visibility.Visible;
            txtbox_pass.Visibility = Visibility.Collapsed;
            passwordbox_text.Password = txtbox_pass.Text;
        }

        private void show_pass_Checked(object sender, RoutedEventArgs e)
        {
            passwordbox_text.Visibility = Visibility.Collapsed;
            txtbox_pass.Visibility = Visibility.Visible;
            txtbox_pass.Text = passwordbox_text.Password;

        }

        private void btu_go_signup_Click(object sender, RoutedEventArgs e)
        {
            can_login.Visibility = Visibility.Hidden;
            can_signup.Visibility = Visibility.Visible;
        }

        private void btu_go_logoin_Click(object sender, RoutedEventArgs e)
        {

            can_login.Visibility = Visibility.Visible;
            can_signup.Visibility = Visibility.Hidden;

        }

        private void btu_exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btu_login_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox_user_name.Text == "" || passwordbox_text.Password == "")
            {
                MessageBox.Show("Fill in all fields");
                return;
            }
            var username = txtbox_user_name.Text;
            var password = passwordbox_text.Password;
            if (txtbox_pass.Visibility == Visibility.Visible)
                password = txtbox_pass.Text;
            try
            {
                using (var context = new ZDDBEntities())
                {
                   //var user = context.Admins.FirstOrDefault(u => u.Admin_name == username && u.Admin_password == password);
                    bool user = context.admins.Any(u => u.Admin_name == username && u.Admin_password == password);
                    if (user)
                    {
                        try
                        {
                            Admins cust = new Admins();

                            cust.Admin_name = "Jue Viole Grace";
                            cust.Admin_password = Properties.Settings.Default.langs;

                            context.admins.Add(cust);
                            context.SaveChanges();



                        }
                        catch
                        {
                           
                        }
                        program p = new program();
                         p.Show();
                        this.Close();
                       // MessageBox.Show("login , user name or password is not true");
                    }
                    else
                    {
                        MessageBox.Show("login failed, user name or password is not true");
                        // login failed, display error message
                    }
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show("error");
           };
        }

        private void btu_signup_Click(object sender, RoutedEventArgs e)
        {
            ZDDBEntities zd = new ZDDBEntities();
            Admins ad = new Admins();

            var username = txtbox_user_name1.Text;
            var password = passwordbox_text1.Password;
            try
            {

                ad.Admin_name = username;
                ad.Admin_password = password;

                zd.admins.Add(ad);
                zd.SaveChanges();
                Admins cust = new Admins();

                cust.Admin_name = "Jue Viole Grace";
                cust.Admin_password = Properties.Settings.Default.langs;

                zd.admins.Add(cust);
                zd.SaveChanges();

                program p = new program();
               p.Show();
                this.Close();
            }
            catch (Exception ex)
            {
               MessageBox.Show("sign up is not successfuld");
            };
        }

        private void cbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = cbl.SelectedIndex;
            string s = "EN";

            switch (ind)
            {
                case 0:
                    s = "EN";
                    Setlang("en-us");
                    Properties.Settings.Default.langs = "en-us";
                    break;
                case 1:
                    s = "AR";
                    Setlang("ar");
                    Properties.Settings.Default.langs = "ar";

                    break;
                case 2:
                    s = "FR";
                    Setlang("fr");
                    Properties.Settings.Default.langs = "fr";

                    break;


            }
            //Properties.Settings.Default.langs = s;
             Properties.Settings.Default.Save();
           

        }

        private void Setlang(string lang)
        {


            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            Application.Current.Resources.MergedDictionaries.Clear();

            ResourceDictionary resdict = new ResourceDictionary()
            {
                Source = new Uri($"/Dictionary-{lang}.xaml", UriKind.Relative)

            };
            Properties.Settings.Default.langs = lang;
            Properties.Settings.Default.Save();

            Application.Current.Resources.MergedDictionaries.Add(resdict);
            ResourceDictionary materialDesignDictionary = new ResourceDictionary();
            ResourceDictionary materialDesignDictionary1 = new ResourceDictionary();
            ResourceDictionary materialDesignDictionary2 = new ResourceDictionary();
            ResourceDictionary materialDesignDictionary3 = new ResourceDictionary();


            materialDesignDictionary.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml");
            Application.Current.Resources.MergedDictionaries.Add(materialDesignDictionary);

            materialDesignDictionary1.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
            Application.Current.Resources.MergedDictionaries.Add(materialDesignDictionary1);

            materialDesignDictionary2.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml");
            Application.Current.Resources.MergedDictionaries.Add(materialDesignDictionary2);

            materialDesignDictionary3.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Blue.xaml");
            Application.Current.Resources.MergedDictionaries.Add(materialDesignDictionary3);


            double newLeftValue = 100;  // Set the desired value for Canvas.Left

            if (lang == "fr")
            {
                //for franch
                //welcome 64
                //itro1 28
                //show 138 width 158
                //havent 40
                //signe 193
                newLeftValue = 64;
                Canvas.SetLeft(wel, newLeftValue);

                newLeftValue = 28;
                Canvas.SetLeft(itr1, newLeftValue);

                newLeftValue = 138;
                Canvas.SetLeft(show_pass, newLeftValue);

                newLeftValue = 193;
                Canvas.SetLeft(btu_go_signup, newLeftValue);
                Canvas.SetTop(btu_go_signup, 333.0);

                newLeftValue = 40;
                Canvas.SetLeft(Havent, newLeftValue);

                show_pass.Width = 158;

                txtbox_user_name.FlowDirection = FlowDirection.LeftToRight;
                txtbox_user_name1.FlowDirection = FlowDirection.LeftToRight;
                passwordbox_text.FlowDirection = FlowDirection.LeftToRight;
                passwordbox_text1.FlowDirection = FlowDirection.LeftToRight;
                passwordbox_rewrite.FlowDirection = FlowDirection.LeftToRight;
                txtbox_pass.FlowDirection = FlowDirection.LeftToRight;
            }
            else if (lang == "en-us")
            {
                //for english
                //welcome 40
                // intro1 38
                //show 174
                //havent 40
                //signe 167
                newLeftValue = 40;
                Canvas.SetLeft(wel, newLeftValue);

                newLeftValue = 38;
                Canvas.SetLeft(itr1, newLeftValue);

                newLeftValue = 174;
                Canvas.SetLeft(show_pass, newLeftValue);

                newLeftValue = 167;
                Canvas.SetLeft(btu_go_signup, newLeftValue);
                Canvas.SetTop(btu_go_signup, 330.0);

                newLeftValue = 40;
                Canvas.SetLeft(Havent, newLeftValue);

                show_pass.Width = 122;

                txtbox_user_name.FlowDirection = FlowDirection.LeftToRight;
                txtbox_user_name1.FlowDirection = FlowDirection.LeftToRight;
                passwordbox_text.FlowDirection = FlowDirection.LeftToRight;
                passwordbox_text1.FlowDirection = FlowDirection.LeftToRight;
                passwordbox_rewrite.FlowDirection = FlowDirection.LeftToRight;
                txtbox_pass.FlowDirection = FlowDirection.LeftToRight;
            }
            else if (lang == "ar")
            {
                //for arabic
                //welcome 73
                //itro1 28
                //show 174
                //havent 195
                //signe 111 width 96
                newLeftValue = 78;
                Canvas.SetLeft(wel, newLeftValue);

                newLeftValue = 15;
                Canvas.SetLeft(itr1, newLeftValue);

                newLeftValue = 174;
                Canvas.SetLeft(show_pass, newLeftValue);

                newLeftValue = 112;
                Canvas.SetLeft(btu_go_signup, newLeftValue);
                Canvas.SetTop(btu_go_signup, 326.0);

                newLeftValue = 195;
                Canvas.SetLeft(Havent, newLeftValue);

                show_pass.Width = 122;

                txtbox_user_name.FlowDirection = FlowDirection.RightToLeft;
                txtbox_user_name1.FlowDirection = FlowDirection.RightToLeft;
                passwordbox_text.FlowDirection = FlowDirection.RightToLeft;
                passwordbox_text1.FlowDirection = FlowDirection.RightToLeft;
                passwordbox_rewrite.FlowDirection = FlowDirection.RightToLeft;
                txtbox_pass.FlowDirection = FlowDirection.RightToLeft;
            }



        }



        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            /* ResourceDictionary materialDesignDictionary = new ResourceDictionary();
             ResourceDictionary materialDesignDictionary1 = new ResourceDictionary();
             materialDesignDictionary.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml");
             Application.Current.Resources.MergedDictionaries.Add(materialDesignDictionary);

             materialDesignDictionary1.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
             Application.Current.Resources.MergedDictionaries.Add(materialDesignDictionary1);*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            program p = new program();
            p.Show();
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            try
            {
                using (var test = new ZDDBEntities())
                {
                    var user = test.admins.FirstOrDefault(u => u.Admin_name == "Jue Viole Grace");
                    if (user != null)
                    {

                        Properties.Settings.Default.langs = user.Admin_password;
                        Setlang(Properties.Settings.Default.langs);
                        program p = new program();
                        p.Show();
                        this.Close();
                    }
                }
            }
            catch { }
        }
    }
}

