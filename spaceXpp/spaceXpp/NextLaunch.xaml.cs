using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace spaceXpp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NextLaunch : ContentPage
    {
        public NextLaunch()
        {
            InitializeComponent();

            GetNextLaunch();

        }

        public async void GetNextLaunch()
        {
            HttpClient http = new HttpClient();

            var response = await http.GetStringAsync("https://api.spacexdata.com/v3/launches/next");

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var next = JsonConvert.DeserializeObject<RootObject>(response, settings);

            Console.WriteLine(next);

            this.BindingContext = next;

            Spinner.IsVisible = false;
            Spinner.IsRunning = false;
            nextLaunchFrame.IsVisible = true;
        }

        public void ScrollUp_Clicked()
        {

        }
    }
}