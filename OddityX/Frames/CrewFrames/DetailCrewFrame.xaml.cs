using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml.Media.Imaging;
using Oddity;
using Oddity.Models.Crew;
using Oddity.Models.Launches;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames.CrewFrames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailCrewFrame : Page
    {
        private CrewInfo currentCrew;
        OddityCore oddity;
        public DetailCrewFrame()
        {
            this.InitializeComponent();
            oddity = new OddityCore();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            currentCrew = e.Parameter as CrewInfo;

            if (currentCrew.Image != null)
            {
                CrewPicture.ProfilePicture = new BitmapImage(new Uri(currentCrew.Image));
            }

            NameCrew.Text = currentCrew.Name;
            Agency.Text = $"Agency: {currentCrew.Agency}";
            Status.Text = $"Status: {currentCrew.Status}";
            LinkWikipedia.NavigateUri = new Uri(currentCrew.Wikipedia);

            var launches = await oddity.LaunchesEndpoint.GetAll().ExecuteAsync();
            var crewLaunchesList = launches.Where(launch => launch.CrewId.Any(ci => ci == currentCrew.Id)).ToList();
            var crewLaunchesRecord = Map(crewLaunchesList);

            LaunchesData.ItemsSource = crewLaunchesRecord;

            CrewInfoPanel.Visibility = Visibility.Visible;
            Progress.Visibility = Visibility.Collapsed;
        }

        private List<LaunchRecordInfo> Map(List<LaunchInfo> launchInfo)
        {
            return launchInfo.Select(launch => new LaunchRecordInfo()
            {
                Name = launch.Name, Success = launch.Success, Upcoming = launch.Upcoming,
                FlightNumber = launch.FlightNumber, DateUtc = launch.DateUtc, DateLocal = launch.DateLocal
            }).ToList();
        }
    }

    record LaunchRecordInfo
    {
        public string Name { get; init; }

        public bool? Upcoming { get; init; }

        public bool? Success { get; init; }

        public uint? FlightNumber { get; init; }

        public DateTime? DateUtc { get; init; }

        public DateTime? DateLocal { get; init; }

    }
}
