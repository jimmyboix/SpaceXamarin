using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace spaceXpp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FutureLaunches : ContentPage
    {
        List<RootObject> launches = null;

        public ObservableCollection<string> Items { get; set; }

        public FutureLaunches()
        {
            InitializeComponent();

            GetLaunches();
        }

        public async void GetLaunches()
        {
            HttpClient http = new HttpClient();

            var response = await http.GetStringAsync("https://api.spacexdata.com/v3/launches/upcoming");

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            this.launches = JsonConvert.DeserializeObject<List<RootObject>>(response, settings);

            Console.WriteLine(response);

            LaunchesListView.ItemsSource = launches;

            Spinner.IsVisible = false;
            Spinner.IsRunning = false;
            LaunchesListView.IsVisible = true;
        }

        public void ScrollUp_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("scrolling:" + sender);
            LaunchesListView.ScrollTo(((IList)LaunchesListView.ItemsSource)[0], ScrollToPosition.Start, false);
        }
    }
}
