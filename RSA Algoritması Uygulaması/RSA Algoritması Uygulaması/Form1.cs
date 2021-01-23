using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace RSA_Algoritması_Uygulaması
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); //Constructor
        }

        public static byte[] Encryption(byte[] girilenVeri, RSAParameters RSAKey, bool EnIyiAsimetrikSifrelemePadding)
        {
            // Encryption adli byte arrayi dondurecek olan statik fonksiyon acilmistir.
            // Bu fonksiyon sifrelenecek veri, System.Security.Cryptography kutuphanesine bagli anahtar ve OAEP bool degerlerini alir.

            try
            {
                // Sifrelenecek veri alinir. System.Security.Cryptography kutuphanesi kullanilarak sifreleme yapilir. Sifrelenmis veri dondurulur.

                byte[] verininSifrelenmisHali; 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey); 
                    verininSifrelenmisHali = RSA.Encrypt(girilenVeri, EnIyiAsimetrikSifrelemePadding);
                }
                return verininSifrelenmisHali;
            }
            catch (CryptographicException e) // Try kisim dogru calismadigi takdirde CryptographicException classindaki e mesajini gosterir.
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static byte[] Decryption(byte[] girilenVeri, RSAParameters RSAKey, bool EnIyiAsimetrikSifrelemePadding)
        {
            // Decryption adli byte arrayi dondurecek olan statik fonksiyon acilmistir.
            // Bu fonksiyon sifrelenecek veri, System.Security.Cryptography kutuphanesine bagli anahtar ve OAEP bool degerlerini alir.

            try
            {
                // Desifrelenecek veri alinir. System.Security.Cryptography kutuphanesi kullanilarak desifreleme yapilir. Desifrelenmis veri dondurulur.

                byte[] verininDesifrelenmisHali;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    verininDesifrelenmisHali = RSA.Decrypt(girilenVeri, EnIyiAsimetrikSifrelemePadding);
                }
                return verininDesifrelenmisHali;
            }
            catch (CryptographicException e) // Try kisim dogru calismadigi takdirde CryptographicException classindaki e mesajini gosterir.
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        UnicodeEncoding ByteConverter = new UnicodeEncoding(); // Bayt dizisi ve string arasında dönüştürmek için bir Unicode Kodlayıcı oluşturulur.
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(); // Yeni bir RSACryptoServiceProvider örneği oluşturulur.
        byte[] plaintext;
        byte[] encryptedtext;

        private void button1_Click(object sender, EventArgs e)
        {
            // Orijinal, şifrelenmiş ve şifresi çözülmüş verileri tutmak için plaintext adli bayt dizisi oluşturulur.
            // Yukaridaki Encryption fonksiyonu kullanilarak encryptedtext txtencrypt'e aktarilir.

            plaintext = ByteConverter.GetBytes(txtplain.Text);
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
            txtencrypt.Text = ByteConverter.GetString(encryptedtext);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Aktarılan bayt dizisinin şifresini çözen decryptedtex byte dizisi tanimlanir.
            // Yukaridaki Decryption fonksiyonu kullanilarak decryptedtext txtdecrypt'e aktarilir.

            byte[] decryptedtext = Decryption(encryptedtext,
            RSA.ExportParameters(true), false);
            txtdecrypt.Text = ByteConverter.GetString(decryptedtext);
        }
    }
}
