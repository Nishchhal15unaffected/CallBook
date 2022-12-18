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
using CallBook.Classes;
using SQLite;

namespace CallBook
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            // Save Contact
            Contact contact = new Contact()
            {
                Name= nameTextBox.Text,
                Email= emailTextBox.Text,
                Phone= phoneTextBox.Text,
            };
            string databaseName = "Contact.db";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string databasePath = System.IO.Path.Combine(folderPath,databaseName);
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Insert(contact);
               
            }
            Close();
        }
    }
}
