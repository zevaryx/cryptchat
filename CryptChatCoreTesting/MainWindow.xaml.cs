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

using CryptChatCore.Security;
using CryptChatCore.Security.Boxes;
using Sodium;

namespace CryptChatCoreTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnMemKey_Click(object sender, RoutedEventArgs e)
        {
            Utils.LoadMemoryKey();
            txtMemKey.Text = Convert.ToBase64String(Utils.MemoryKey.ToArray());
        }

        private void btnPassHash_Click(object sender, RoutedEventArgs e)
        {
            string salt = Password.GenerateSalt();
            txtSalt.Text = salt;
            txtPassHash.Text = Password.HashPassword(txtPassHash.Text, salt);
        }

        private void btnKeypair_Click(object sender, RoutedEventArgs e)
        {
            KeyPair keys = Asymmetric.GenerateKeyPair();
            txtPub.Text = Convert.ToBase64String(keys.PublicKey);
            txtPriv.Text = Convert.ToBase64String(keys.PrivateKey);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            App.Connect(txtAddress.Text);
        }
    }
}
