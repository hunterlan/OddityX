using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Oddity.Models.Crew;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using OddityX.ViewModels;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CrewsFrame : Page
    {
        private List<CrewInfo> _crews;
        public CrewInfoView CurrentCrew;

        public CrewsFrame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _crews = e.Parameter as List<CrewInfo>;
            CrewListView.ItemsSource = _crews;
        }

        private async void CrewItemChanged(object sender, RoutedEventArgs e)
        {
            CrewInfoPanel.Visibility = Visibility.Collapsed;
            LoadingCrewData.IsActive = true;

            var selectedCrew = CrewListView.SelectedItem as CrewInfo;
            CurrentCrew = new CrewInfoView(selectedCrew);

            if (CurrentCrew.Image != null)
            {
                CrewPicture.ProfilePicture = new BitmapImage(new Uri(CurrentCrew.Image));
            }

            NameCrew.Text = CurrentCrew.Name;
            Agency.Text = $"Agency: {CurrentCrew.Agency}";
            Status.Text = $"Status: {CurrentCrew.Status}";
            LinkWikipedia.NavigateUri = new Uri(CurrentCrew.Wikipedia);

            LaunchesData.ItemsSource = await CurrentCrew.GetCrewLaunches();

            CrewInfoPanel.Visibility = Visibility.Visible;
            LoadingCrewData.IsActive = false;
        }

        private void FindCrewByName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FindCrewByName.Text) || string.IsNullOrEmpty(FindCrewByName.Text))
            {
                CrewListView.ItemsSource = _crews;
            }
            else
            {
                var currentText = FindCrewByName.Text;
                var filtered = _crews.Where(c => c.Name.Contains(currentText)).ToList();
                CrewListView.ItemsSource = filtered;
            }
        }
    }
}
