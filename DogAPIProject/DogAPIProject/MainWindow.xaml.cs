using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace DogAPIProject
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

        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            string breed = txtBreed.Text;

            if (string.IsNullOrWhiteSpace(txtBreed.Text) == true)
            {
                MessageBox.Show("Please enter an actual breed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }            

            string url = $"https://dog.ceo/api/breed/{breed}/images/random";

            DogAPI dog = null;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;

                    dog = JsonConvert.DeserializeObject<DogAPI>(json);
                }
                else
                {
                    MessageBox.Show("Sorry, that breed does not exist!");
                    txtBreed.Clear();
                }
            }

            if (dog != null)
            {
                imgDog.Source = new BitmapImage(new Uri(dog.message));
            }
        }
    }
}
