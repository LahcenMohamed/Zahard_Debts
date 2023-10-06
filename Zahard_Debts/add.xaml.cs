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
using Microsoft.EntityFrameworkCore;


namespace Zahard_Debts
{
    /// <summary>
    /// Interaction logic for add.xaml
    /// </summary>
    public partial class add : Window
    {
        ZDDBEntities zd = new ZDDBEntities();
        Customers cust = new Customers();
        bool its_edit;
        int id;
        int total;
        public add()
        {
            InitializeComponent();
            its_edit = false;
            button_add_or_edit.Content = "Add";
            text_total.IsReadOnly = false;

            if (Properties.Settings.Default.langs == "ar")
            {
                text_name.FlowDirection = FlowDirection.RightToLeft;
                text_email.FlowDirection = FlowDirection.RightToLeft;
                text_phone.FlowDirection = FlowDirection.RightToLeft;
                text_total.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                text_name.FlowDirection = FlowDirection.LeftToRight;
                text_email.FlowDirection = FlowDirection.LeftToRight;
                text_phone.FlowDirection = FlowDirection.LeftToRight;
                text_total.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public add(Customers custe)
        {
            InitializeComponent();
            its_edit = true;

            text_name.Text = custe.Customer_name;
            text_email.Text = custe.email;
            text_phone.Text = custe.phone;
            text_total.Text = custe.total_debts.ToString();
            id = custe.Customer_id;

            button_add_or_edit.Content = "Edit";
            text_total.IsReadOnly = true;

            if (Properties.Settings.Default.langs == "ar")
            {
                text_name.FlowDirection = FlowDirection.RightToLeft;
                text_email.FlowDirection = FlowDirection.RightToLeft;
                text_phone.FlowDirection = FlowDirection.RightToLeft;
                text_total.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                text_name.FlowDirection = FlowDirection.LeftToRight;
                text_email.FlowDirection = FlowDirection.LeftToRight;
                text_phone.FlowDirection = FlowDirection.LeftToRight;
                text_total.FlowDirection = FlowDirection.LeftToRight;
            }

        }


        private void Window_Activated(object sender, EventArgs e)
        {
            //IsDarkTheme = IsDarkTheme1;


        }
        private void Button_add_or_edit_Click(object sender, RoutedEventArgs e)
        {
            if (its_edit)
            {
                try
                {

                    cust.Customer_id = id;
                    cust.Customer_name = text_name.Text;
                    cust.email = text_email.Text;
                    cust.phone = text_phone.Text;
                    cust.total_debts = Convert.ToInt32(text_total.Text);
                    if (Properties.Settings.Default.langs != "-en-us")
                    {

                    }
                    zd.Entry(cust).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    zd.SaveChanges();

                    MessageBox.Show("Edit is successful");
                }
                catch
                {
                    MessageBox.Show("Edit isn't successful,please check all information");
                }

            }
            else
            {
                try
                {

                    cust.Customer_name = text_name.Text;
                    cust.email = text_email.Text;
                    cust.phone = text_phone.Text;
                    cust.total_debts = Convert.ToInt32(text_total.Text);

                    zd.Entry(cust).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    zd.SaveChanges();
                    MessageBox.Show("Add is successful");
                }
                catch
                {
                    MessageBox.Show("Add isn't successful,please check all information");
                }
            }
        }
    }
}
