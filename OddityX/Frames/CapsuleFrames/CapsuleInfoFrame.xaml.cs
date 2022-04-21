using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Oddity;
using Oddity.Models.Capsules;
using Oddity.Models.Crew;
using Oddity.Models.Launches;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Oddity.Models.Rockets;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CapsuleInfoFrame : Page
    {
        CapsuleInfo currentCupsule;
        OddityCore oddity; 
        
        public CapsuleInfoFrame()
        {
            this.InitializeComponent();
            oddity = new OddityCore();
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            GeneralCapsule.Orientation = XamlRoot.Size.Width >= 1200 ? Orientation.Horizontal : Orientation.Vertical;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            currentCupsule = e.Parameter as CapsuleInfo;
            Serial.Text = $"Serial: {currentCupsule?.Serial}";
            Status.Text = $"Status: {currentCupsule?.Status}";
            ReuseCount.Text = $"Reused count: {currentCupsule?.ReuseCount}";
            WaterLandings.Text = $"Water landings: {currentCupsule?.WaterLandings?.ToString()}";
            LandLandings.Text = $"Land landings: {currentCupsule?.LandLandings?.ToString()}";
            LastUpdate.Text = $"Last update: {currentCupsule?.LastUpdate}";
            CountLaunches.Text = $"Count launches: {currentCupsule?.Launches.Count}";
        }

        private async void ExpandLaunchesNameExpanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            var launchListId = currentCupsule.LaunchesId;
            var launches = await oddity.LaunchesEndpoint.GetAll().ExecuteAsync();
            List<LaunchInfo> capsuleLaunches = new();
            launchListId.ForEach(launchId => capsuleLaunches.AddRange(launches.Where(l => l.Id == launchId)));

            if (!capsuleLaunches.Any())
            {
                var newInfo = new LaunchInfo() { Name = "Doesn't have launches" };
                capsuleLaunches.Add(newInfo);
            }

            LaunchesList.ItemsSource = capsuleLaunches;

            LoadListProgress.Visibility = Visibility.Collapsed;
            LaunchesList.Visibility = Visibility.Visible;
        }

        private async void ExpandCrewExpanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            var launchListId = currentCupsule.LaunchesId;
            var launches = await oddity.LaunchesEndpoint.GetAll().ExecuteAsync();
            var crews = await oddity.CrewEndpoint.GetAll().ExecuteAsync();

            List<LaunchInfo> capsuleLaunches = new();
            List<CrewInfo> capsuleCrew = new(); 
            launchListId.ForEach(launchId => capsuleLaunches.AddRange(launches.Where(l => l.Id == launchId)));

            if (!capsuleLaunches.Any())
            {
                capsuleCrew.Add(new CrewInfo() { Name = "Haven't had a crew yet" });
            }
            else
            {
                foreach (var crewId in capsuleLaunches.SelectMany(launch => launch.CrewId))
                {
                    capsuleCrew.AddRange(crews.Where(crew => crew.Id == crewId));
                }
            }

            if (!capsuleCrew.Any())
            {
                capsuleCrew.Add(new CrewInfo() {Name = "Haven't had a crew yet"});
            }

            CrewList.ItemsSource = capsuleCrew;

            LoadCrewProgress.Visibility = Visibility.Collapsed;
            CrewList.Visibility = Visibility.Visible;
        }

        private async void ExpandRocketExpanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            var launchListId = currentCupsule.LaunchesId;
            var launches = await oddity.LaunchesEndpoint.GetAll().ExecuteAsync();
            var rockets = await oddity.RocketsEndpoint.GetAll().ExecuteAsync();

            List<LaunchInfo> capsuleLaunches = new();
            List<RocketInfo> capsuleRockets = new();
            launchListId.ForEach(launchId => capsuleLaunches.AddRange(launches.Where(l => l.Id == launchId)));

            if (!capsuleLaunches.Any())
            {
                capsuleRockets.Add(new RocketInfo() { Name = "Haven't had rockets yet" });
            }
            else
            {
                foreach (var launch in capsuleLaunches)
                {
                    capsuleRockets.AddRange(rockets.FindAll(rocket => rocket.Id == launch.RocketId));
                }
            }

            if (!capsuleRockets.Any())
            {
                capsuleRockets.Add(new RocketInfo() { Name = "Haven't had rockets yet" });
            }

            RocketsList.ItemsSource = capsuleRockets;

            LoadRocketProgress.Visibility = Visibility.Collapsed;
            RocketsList.Visibility = Visibility.Visible;
        }
    }
}
