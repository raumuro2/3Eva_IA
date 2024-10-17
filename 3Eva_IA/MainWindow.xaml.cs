using _3Eva_IA.Acceso_a_datos;
using Microsoft.Win32;
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

namespace _3Eva_IA
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string imagePath = "";

        public MainWindow()
        {
            InitializeComponent();
            rellenarVoces();    
        }
        public async void rellenarVoces()
        {
            var voces = await AzureAPI.getVoices();
            cbx_voices.ItemsSource = voces;
            cbx_voices.DisplayMemberPath = "ShortName";
            cbx_voices.Text = "en-GB-SoniaNeural";
        }
        private void btn_subirImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp|Todos los archivos|*.*";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                imagePath = openFileDialog.FileName;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();

                imageControl.Source = bitmap;
            }
        }

        private void btn_image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btn_flecha_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(imagePath != "")
            {
                string result = AzureAPI.AnalyzeImage(imagePath);
                if(result == null) { MessageBox.Show("Ha habido un error al analizar la imagen"); }
                string[] textSplit = result.Split(new char[] { '#' }, 2);
                txt_title.Text = textSplit[0];
                txt_text.Text = textSplit[1];
                AzureAPI.readText(textSplit[1], cbx_voices.Text);
            }
            else
            {
                MessageBox.Show("Debes de seleccionar una imagen");
            }
        }
    }
}
