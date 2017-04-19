using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace EncryptionTest
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

        private void button_Click(object sender, RoutedEventArgs e)
        {


           

            // byte[] encrytBytes = Encrypt(publicKey, textBox.Text);

            // textBox1.Text = System.Text.Encoding.ASCII.GetString(encrytBytes);

            //textBox2.Text = Decrypt(privateKey, encrytBytes);
     //       string inputHash = textBox3.Text;

//            string hashedString = sha256_hash(inputHash);

    //        textBox4.Text = hashedString;



            //encrypt
            //lijn 1 van word
            byte[] key = CreateAesKey();

            //lijn 2 van word (alice)
            var alice = CreateKeyPair();
            string alicePrivate = alice.Item1;
            string alicePublic = alice.Item2;

            //lijn 3 van word (bob)
            var bob = CreateKeyPair();
            string bobPrivate = bob.Item1;
            string bobPublic = bob.Item2;

            //lijn 4 van word
            string inputAlice = textBox.Text;

            //lijn 5 van word
            byte[] salt = GenSalt();
            string file1 = EncryptAes(inputAlice, salt, key);

            //lijn 6 van 
            //omzetten van byte array naar sting kan fout zijn
            byte[] file2 = Encrypt(bobPublic, System.Text.Encoding.Default.GetString(key));

            //lijn 7
            string hashedInputAlice = sha256_hash(inputAlice);

            //lijn 8
            byte[] file3 = Encrypt(alicePrivate, hashedInputAlice);


            //decrypt
            //lijn1
            //gwn ophalen ma ik heb de gegevens al

            //lijn2

            string decryptedSymKey = Decrypt(bobPrivate, file2);

            //lijn3
            //kan fout zijn
            byte[] toBytes = Encoding.Default.GetBytes(decryptedSymKey);
            textBox3.Text = System.Text.Encoding.Default.GetString(key);
            textBox4.Text = decryptedSymKey;

            if (textBox3.Text.Equals(textBox4.Text))
            {
                MessageBox.Show("het zelfde");
            }
            else
            {
                MessageBox.Show("niet zelfde");
            } 


            // key zou eigelijk tobytes meoten zijn
            textBox1.Text = DecryptAes(file1, salt, toBytes);

            //lijn 4
            string checkHash = sha256_hash(textBox1.Text);

            if (hashedInputAlice.Equals(checkHash))
            {
                textBox2.Text = "true";
            }
            else
            {
                textBox2.Text = "false";
            }



            //   textBox1.Text = file1;

            //  textBox2.Text = DecryptAes(file1, salt , key);




        }

        //create AES key
        public static byte[] CreateAesKey()
        {
            System.Security.Cryptography.AesCryptoServiceProvider crypto =
                new System.Security.Cryptography.AesCryptoServiceProvider();

            crypto.KeySize = 256;
            crypto.GenerateKey();
            byte[] key = crypto.Key;

            return key;

        }


        // string strKey = System.Text.Encoding.ASCII.GetString(key);

        //  textBox1.Text = strKey;


        // create keypair
        public static Tuple<string, string> CreateKeyPair()
        {
            CspParameters cspParams = new CspParameters {ProviderType = 1};

            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(1024, cspParams);

            string publicKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(false));
            string privateKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(true));

            return new Tuple<string, string>(privateKey, publicKey);



        }

        //encrypite met public key
        public static byte[] Encrypt(string publicKey, string data)
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 };
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParams);

            rsaProvider.ImportCspBlob(Convert.FromBase64String(publicKey));

            byte[] plainBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = rsaProvider.Encrypt(plainBytes, false);

            return encryptedBytes;
        }

        //decript met private key
        public static string Decrypt(string privateKey, byte[] encryptedBytes)
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 };
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParams);

            rsaProvider.ImportCspBlob(Convert.FromBase64String(privateKey));

            byte[] plainBytes = rsaProvider.Decrypt(encryptedBytes, false);

            string plainText = Encoding.UTF8.GetString(plainBytes, 0, plainBytes.Length);

            return plainText;
        }

        //genarate salt
        public Byte[] GenSalt()
        {
            // Put random bytes into this array.
            byte[] array = new byte[8];
            byte[] salt = new byte[8];
            // Use Random class and NextBytes method.
            // ... Display the bytes with following method.
            Random random = new Random();
            random.NextBytes(array);
            int i = 0;
            foreach (byte value in array)
            {
                salt[i] = value;
                i++;
            }

            return salt;
        }

        //second try encryot with aes
        private string EncryptAes(string clearText , byte[] salt, byte[] encreptionKey)
        {
          

           // string EncryptionKey = "MAKV2SPBNI9921";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encreptionKey, salt, 1);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        //second try decrypt with aes
        private string DecryptAes(string cipherText,  byte[] salt, byte[] encreptionKey)
        {
           // string EncryptionKey = "MAKV2SPBNI9921";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encreptionKey, salt, 1);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        //hashing

        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        ////encrypt with AES
        //public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        //{
        //    byte[] encryptedBytes = null;

        //    // Set your salt here, change it to meet your flavor:
        //    // The salt bytes must be at least 8 bytes.
        //    byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (RijndaelManaged AES = new RijndaelManaged())
        //        {
        //            AES.KeySize = 256;
        //            AES.BlockSize = 128;

        //            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        //            AES.Key = key.GetBytes(AES.KeySize / 8);
        //            AES.IV = key.GetBytes(AES.BlockSize / 8);

        //            AES.Mode = CipherMode.CBC;

        //            using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
        //                cs.Close();
        //            }
        //            encryptedBytes = ms.ToArray();
        //        }
        //    }

        //    return encryptedBytes;
        //}

        ////deincrypt with aes
        //public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        //{
        //    byte[] decryptedBytes = null;

        //    // Set your salt here, change it to meet your flavor:
        //    // The salt bytes must be at least 8 bytes.
        //    byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (RijndaelManaged AES = new RijndaelManaged())
        //        {
        //            AES.KeySize = 256;
        //            AES.BlockSize = 128;

        //            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        //            AES.Key = key.GetBytes(AES.KeySize / 8);
        //            AES.IV = key.GetBytes(AES.BlockSize / 8);

        //            AES.Mode = CipherMode.CBC;

        //            using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
        //                cs.Close();
        //            }
        //            decryptedBytes = ms.ToArray();
        //        }
        //    }

        //    return decryptedBytes;
        //}
    }
}

