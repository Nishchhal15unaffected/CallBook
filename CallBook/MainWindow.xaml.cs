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
using CallBook.Classes;
using SQLite;

namespace CallBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts = new List<Contact>();
        public MainWindow()
        {
            InitializeComponent();

            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow= new NewContactWindow();
            newContactWindow.ShowDialog();
            ReadDatabase();
        }

        void ReadDatabase()
        {
            using(SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Contact>();
                contacts = (conn.Table<Contact>().ToList()).OrderBy(c => c.Name).ToList();
            }
            if(contacts != null)
            {
                contactListView.ItemsSource = contacts;
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            var filterList = contacts.Where(c => c.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            contactListView.ItemsSource = filterList;
        }
    }
}
