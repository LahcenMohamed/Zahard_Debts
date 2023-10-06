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

namespace Zahard_Debts
{
    /// <summary>
    /// Interaction logic for add_edit.xaml
    /// </summary>
    public partial class add_edit : Window
    {
        ZDDBEntities zd = new ZDDBEntities();
        Debts debs = new Debts();
        public Customers cus = new Customers();
        bool its_edit;
        int id;
        int id1;
        public add_edit(int id)
        {

            InitializeComponent();
            its_edit = false;
            id1 = id;
            tex_Date.Text = DateTime.Now.Date.ToString();
            if (Properties.Settings.Default.langs == "ar")
            {
                text_note.FlowDirection = FlowDirection.RightToLeft;
                text_size.FlowDirection = FlowDirection.RightToLeft;
                text_type.FlowDirection = FlowDirection.RightToLeft;
                tex_Date.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                text_note.FlowDirection = FlowDirection.LeftToRight;
                text_size.FlowDirection = FlowDirection.LeftToRight;
                text_type.FlowDirection = FlowDirection.LeftToRight;
                tex_Date.FlowDirection = FlowDirection.LeftToRight;
            }
            button_add_or_edit.Content = "add";
        }
        public add_edit(Debts deb)
        {
            InitializeComponent();

            id = deb.Debt_id;
            text_type.Text = deb.Debt_type;
            text_size.Text = deb.size.ToString();

            //TextRange(text_note.Document.ContentStart, text_note.Document.ContentEnd).Text = deb.note;

            text_note.Document.Blocks.Clear(); // Clear existing content
            text_note.Document.Blocks.Add(new Paragraph(new Run(deb.note))); // Set new content
            text_type.Text = deb.Debt_type;
            tex_Date.Text = deb.Debt_date.ToString();
            id1 = deb.Customer_id;


            cus = zd.Customers.Where(x => x.Customer_id == id1).FirstOrDefault();
            if (debs.Debt_type == "borrows")
            {
                cus.total_debts = cus.total_debts - deb.size;
            }
            else
                cus.total_debts = cus.total_debts + deb.size;


            zd.Entry(cus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            zd.SaveChanges();



            button_add_or_edit.Content = "Edit";
            its_edit = true;

            if (Properties.Settings.Default.langs == "ar")
            {
                text_note.FlowDirection = FlowDirection.RightToLeft;
                text_size.FlowDirection = FlowDirection.RightToLeft;
                text_type.FlowDirection = FlowDirection.RightToLeft;
                tex_Date.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                text_note.FlowDirection = FlowDirection.LeftToRight;
                text_size.FlowDirection = FlowDirection.LeftToRight;
                text_type.FlowDirection = FlowDirection.LeftToRight;
                tex_Date.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void Button_add_or_edit_Click(object sender, RoutedEventArgs e)
        {
            if (its_edit)
            {
                try
                {
                    debs.Debt_id = id;
                    if (Properties.Settings.Default.langs != "-en-us")
                    {
                        if (text_type.Text == "إرجاع" || text_type.Text == "retours")
                            debs.Debt_type = "returns";
                        else
                            debs.Debt_type = "borrows";

                    }
                    else
                        debs.Debt_type = text_type.Text;


                    
                    debs.Customer_id = id1;
                    //debs.Debt_type = text_type.Text;
                    debs.size = Convert.ToInt32(text_size.Text);
                    debs.Debt_date = Convert.ToDateTime(tex_Date.Text);
                    string text = new TextRange(text_note.Document.ContentStart, text_note.Document.ContentEnd).Text;
                    debs.note = text;

                    cus = zd.Customers.Where(x => x.Customer_id == debs.Customer_id).FirstOrDefault();
                    
                    if (debs.Debt_type == "borrows")
                    {
                        cus.total_debts = cus.total_debts + debs.size;
                    }
                    else
                        cus.total_debts = cus.total_debts - debs.size;


                    zd.Entry(cus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    
                    zd.Entry(debs).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

                    //debs.Debt_id = id;
                    debs.Customer_id = id1;
                    debs.Debt_type = text_type.Text;
                    debs.size = Convert.ToInt32(text_size.Text);
                    debs.Debt_date = Convert.ToDateTime(tex_Date.Text);
                    string text = new TextRange(text_note.Document.ContentStart, text_note.Document.ContentEnd).Text;
                    debs.note = text;

                    cus = zd.Customers.Where(x => x.Customer_id == debs.Customer_id).FirstOrDefault();
                    if (debs.Debt_type == "borrows")
                    {
                        cus.total_debts = cus.total_debts + debs.size;
                    }
                    else
                        cus.total_debts = cus.total_debts - debs.size;


                    zd.Entry(cus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    zd.SaveChanges();

                    zd.Entry(debs).State = Microsoft.EntityFrameworkCore.EntityState.Added;
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
