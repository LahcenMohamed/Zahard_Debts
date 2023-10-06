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
using MaterialDesignThemes.Wpf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.IO;
using System.Data;

namespace Zahard_Debts
{
    /// <summary>
    /// Interaction logic for program.xaml
    /// </summary>
    public partial class program : Window
    {
        
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        public ZDDBEntities db = new ZDDBEntities();
        public Customers cus = new Customers();
        public Debts de = new Debts();
        public int id;
        public program()
        {
            InitializeComponent();
            if (Properties.Settings.Default.langs == "ar")
            {
                datax.FlowDirection = FlowDirection.RightToLeft;
                datax1.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                datax.FlowDirection = FlowDirection.LeftToRight;
                datax1.FlowDirection = FlowDirection.LeftToRight;
            }
           
        }

        private void btu_exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btu_max_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
                datax.Width = this.Width;
                datax.Height = this.Height - 161;
                datax1.Width = datax.Width;
                datax1.Height = datax.Height;
                button_export.RenderTransform = new TranslateTransform(100, 120);
                button_add.RenderTransform = new TranslateTransform(100, 120);
                button_add_Copy.RenderTransform = new TranslateTransform(100, 120);
                return;
            }
            this.WindowState = WindowState.Normal;
            datax.Width = this.Width;
            datax.Height = this.Height - 161;
            button_export.RenderTransform = new TranslateTransform(0, 0);
            button_add.RenderTransform = new TranslateTransform(0, 0);
            button_add_Copy.RenderTransform = new TranslateTransform(0, 0);


            datax1.Width = datax.Width;
            datax1.Height = datax.Height;
        }

        private void btu_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btu_theme_Click(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            paletteHelper.SetTheme(theme);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
            using (var db = new ZDDBEntities())
            {
                var customer = db.Customers.ToList();


                datax.ItemsSource = customer;
                var debss = db.Debts.Where(s => s.Customer_id == id).ToList();
                
                datax1.ItemsSource = debss;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           add adde = new add(cus);
            adde.Owner = this;
            adde.Show();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
           add_edit adde = new add_edit(de);
            adde.Owner = this;
            adde.Show();
        }
        private void Datax_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            try
            {
                var row = datax.SelectedItem;
                id = Convert.ToInt32((datax.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);
                cus = db.Customers.Where(x => x.Customer_id == id).FirstOrDefault();
            }
            catch
            { }
        }
        private void Datax_SelectionChanged_debts(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var row = datax1.SelectedItem;
                int id1 = Convert.ToInt32((datax1.SelectedCells[0].Column.GetCellContent(row) as TextBlock).Text);
                de = db.Debts.Where(x => x.Debt_id == id1).FirstOrDefault();

            }
            catch
            { }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + cus.Customer_name + "?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    //db.Entry(cus).State = System.Data.Entity.EntityState.Deleted;
                    var customersToRemove = db.Debts.Where(x => x.Customer_id == cus.Customer_id).ToList();
                    db.RemoveRange(customersToRemove);
                    db.Remove(cus);
                    db.SaveChanges();
                    MessageBox.Show("Delete is successful");
                }
            }
            catch
            { }
        }

        private void DeleteButton_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    cus = db.Customers.Where(x => x.Customer_id == de.Customer_id).FirstOrDefault();
                    if (de.Debt_type == "borrows")
                    {
                        cus.total_debts = cus.total_debts - de.size;
                    }
                    else
                        cus.total_debts = cus.total_debts + de.size;


                    db.Entry(cus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    db.Remove(de);
                    db.SaveChanges();
                    MessageBox.Show("Delete is successful");
                }
            }
            catch
            { }
        }

        private void text_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (datax1.Visibility == Visibility.Hidden)
            {
                int searchID = 0;
                int searchTotal = 0;


                // Parse input values
                int.TryParse(text_search.Text, out searchID);
                int.TryParse(text_search.Text, out searchTotal);


                string searchTerms = text_search.Text;

                var cuse = db.Customers.Where(s => s.Customer_name.Contains(searchTerms) || s.phone.Contains(searchTerms) || s.email.Contains(searchTerms) || s.Customer_id == searchID || s.total_debts == searchTotal).ToList(); // replace with your DbSet property name and search criteria

                datax.ItemsSource = cuse;
            }
            else
            {
                int searchID = 0;
                int size = 0;
                System.DateTime dates;

                int.TryParse(text_search.Text, out searchID);
                int.TryParse(text_search.Text, out size);
                System.DateTime.TryParse(text_search.Text, out dates);

                string searchTerms = text_search.Text;

                var deb = db.Debts.Where(s => s.Debt_id == searchID || s.Debt_type.Contains(searchTerms) || s.size == size || s.Debt_date == dates).ToList();
                datax1.ItemsSource = deb;
            }
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (datax1.Visibility == Visibility.Hidden)
            {
                add adde = new add();
                adde.Owner = this;
                adde.Show();
            }
            else
            {
                add_edit adde = new add_edit(id);
                adde.Owner = this;
                adde.Show();
            }
        }

        private void button_add_Click1(object sender, RoutedEventArgs e)
        {
            add adde = new add();
            adde.Owner = this;
            adde.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btu_detail_Click(object sender, RoutedEventArgs e)
        {
            datax.Visibility = Visibility.Hidden;
            datax1.Visibility = Visibility.Visible;
            btu_back.Visibility = Visibility.Visible;

            Debts des = new Debts();

            var debss = db.Debts.Where(s => s.Customer_id == id).ToList();
            datax1.ItemsSource = debss;

        }

        private void button_export_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dat = new DataGrid();
            if (datax1.Visibility == Visibility.Hidden)
                dat = datax;
            else
                dat = datax1;


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.FileName = "ExportedData.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Create a new PDF document
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));

                    // Open the document for writing
                    document.Open();

                    // Create a PDF table with the same number of columns as the DataGridView
                    PdfPTable table = new PdfPTable(dat.Columns.Count);

                    // Add table headers
                    foreach (DataGridColumn column in dat.Columns)
                    {
                        table.AddCell(new Phrase(column.Header.ToString()));
                    }

                    // Force the DataGridView to generate its rows and update the layout
                    datax.UpdateLayout();

                    // Add table rows
                    foreach (var item in dat.Items)
                    {
                        DataGridRow row = dat.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            foreach (DataGridColumn column in dat.Columns)
                            {
                                FrameworkElement element = column.GetCellContent(item);
                                if (element != null)
                                {
                                    string cellValue = GetCellValue(element);
                                    table.AddCell(new Phrase(cellValue));
                                }
                            }
                        }
                    }

                    // Add the table to the document
                    document.Add(table);

                    // Close the document
                    document.Close();

                    MessageBox.Show("Export completed successfully.", "Export to PDF", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error exporting to PDF: " + ex.Message, "Export to PDF", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string GetCellValue(FrameworkElement element)
        {
            if (element is TextBlock textBlock)
            {
                return textBlock.Text;
            }
            else if (element is TextBox textBox)
            {
                return textBox.Text;
            }
            // Handle other types of controls as needed

            return string.Empty;
        }

        private void btu_back_Click(object sender, RoutedEventArgs e)
        {
            datax.Visibility = Visibility.Visible;
            datax1.Visibility = Visibility.Hidden;
            btu_back.Visibility = Visibility.Hidden;
        }

        private void btu_theme_Copy_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Log out ", "Log out Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var test = new ZDDBEntities())
                    {
                        var user = test.admins.FirstOrDefault(u => u.Admin_name == "Jue Viole Grace");
                        if (user != null)
                        {
                            Properties.Settings.Default.langs = "en-us";
                            test.Remove(user);
                            test.SaveChanges();
                            Environment.Exit(0);

                        }
                    }
                }
                catch { }
            }
        }
        
    }
}
    

