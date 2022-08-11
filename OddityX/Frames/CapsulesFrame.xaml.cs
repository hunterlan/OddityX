using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Oddity.Models.Capsules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using OddityX.ViewModels;
using Oddity.Models.Launches;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CapsulesFrame : Page
    {
        private List<CapsuleInfo> _capsules;
        public CapsuleInfoView CurrentCapsule;

        public CapsulesFrame()
        {
            this.InitializeComponent();
        }

        private async void CapsulesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CapsuleData.Visibility = Visibility.Collapsed;
            LoadingCapsuleData.IsActive = true;

            var selectedItem = CapsulesList.SelectedItem as CapsuleInfo;
            CurrentCapsule = new CapsuleInfoView(_capsules.FirstOrDefault(s => s.Id.Equals(selectedItem.Id)));
            Serial.Text = $"Serial: {CurrentCapsule?.Serial}";
            Status.Text = $"Status: {CurrentCapsule?.Status}";
            ReuseCount.Text = $"Reused count: {CurrentCapsule?.ReuseCount}";
            WaterLandings.Text = $"Water landings: {CurrentCapsule?.CountWaterLanding.ToString()}";
            LandLandings.Text = $"Land landings: {CurrentCapsule?.CountLandLanding?.ToString()}";
            LastUpdate.Text = $"Last update: {CurrentCapsule?.LastUpdate}";
            int countLaunches = await CurrentCapsule.GetCountLaunches();
            CountLaunches.Text = $"Count launches: {countLaunches}";

            LoadingCapsuleData.IsActive = false;
            CapsuleData.Visibility = Visibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _capsules = e.Parameter as List<CapsuleInfo>;
            CapsulesList.ItemsSource = _capsules;
        }

        private async void ExpandLaunchesNameExpanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            LaunchesList.ItemsSource = await CurrentCapsule.GetCapsuleLaunches();
            LoadListProgress.Visibility = Visibility.Collapsed;
            LaunchesList.Visibility = Visibility.Visible;
        }

        private async void ExpandCrewExpanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            CrewList.ItemsSource = await CurrentCapsule.GetCapsuleCrew();
            LoadCrewProgress.Visibility = Visibility.Collapsed;
            CrewList.Visibility = Visibility.Visible;
        }

        private async void ExpandRocketExpanding(Expander sender, ExpanderExpandingEventArgs args)
        {
            RocketsList.ItemsSource = await CurrentCapsule.GetCapsuleRockets();
            LoadRocketProgress.Visibility = Visibility.Collapsed;
            RocketsList.Visibility = Visibility.Visible;
        }

        private void FindCapsuleByName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FindCapsuleByName.Text) || string.IsNullOrEmpty(FindCapsuleByName.Text))
            {
                CapsulesList.ItemsSource = _capsules;
            }
            else
            {
                var currentText = FindCapsuleByName.Text;
                var filtered = _capsules.Where(c => c.Serial.Contains(currentText)).ToList();
                CapsulesList.ItemsSource = filtered;
            }
        }
    }
}
