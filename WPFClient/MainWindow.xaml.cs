using DockerE2EApi.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using WPFClient.Model;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DateTime Date { get; set; } = DateTime.Today;

        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

        static HttpClient HttpClient = new HttpClient();

        public MainWindow()
        {
            HttpClient.BaseAddress = new Uri("http://localhost:32772/");
            DataContext = this;
            InitializeComponent();
            receiveCars();
        }

        public async void receiveCars()
        {
            var resp = await HttpClient.GetAsync("api/Cars/Day/" + Date.Year + "-" + Date.Month + "-" + Date.Day);
            JsonSerializer.Deserialize<List<Car>>(await resp.Content.ReadAsStringAsync()).ForEach(c => Cars.Add(c));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Cars.Clear();
            var resp = await HttpClient.GetAsync("api/Cars/Day/" + Date.Year + "-" + Date.Month + "-" + Date.Day);
            JsonSerializer.Deserialize<List<Car>>(await resp.Content.ReadAsStringAsync()).ForEach(c => Cars.Add(c));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cars.Clear();
            Date date = new Date();
            date.DateTime = Date;
            var content = new StringContent(JsonSerializer.Serialize(date), Encoding.UTF8, "application/json");
            Debug.WriteLine(await content.ReadAsStringAsync());
            var resp = await HttpClient.PutAsync("api/Cars/Book/" + ((Button)sender).Tag, content);
            Cars.Clear();
            resp = await HttpClient.GetAsync("api/Cars/Day/" + Date.Year + "-" + Date.Month + "-" + Date.Day);
            JsonSerializer.Deserialize<List<Car>>(await resp.Content.ReadAsStringAsync()).ForEach(c => Cars.Add(c));
        }
    }
}
