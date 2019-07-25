using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections;

namespace spaceXpp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PastLaunches : ContentPage
    {
        public PastLaunches()
        {
            InitializeComponent();

            GetLaunches();
        }

        public async void GetLaunches()
        {
            HttpClient http = new HttpClient();

            var response = await http.GetStringAsync("https://api.spacexdata.com/v3/launches/past?order=desc");

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var launches = JsonConvert.DeserializeObject<List<RootObject>>(response, settings);

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
