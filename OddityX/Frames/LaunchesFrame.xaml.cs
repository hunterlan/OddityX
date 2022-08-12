using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Oddity.Models.Launches;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LaunchesFrame : Page
    {
        private List<LaunchInfo> _currentLaunches;
        private LaunchInfo _currentLaunch;
        public LaunchesFrame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _currentLaunches = e.Parameter as List<LaunchInfo>;
            LaunchesListView.ItemsSource = _currentLaunches;
        }

        private void FindLaunchByName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FindLaunchByName.Text) || string.IsNullOrEmpty(FindLaunchByName.Text))
            {
                LaunchesListView.ItemsSource = _currentLaunches;
            }
            else
            {
                var currentText = FindLaunchByName.Text;
                var filtered = _currentLaunches?.Where(l => l.Name.Contains(currentText)).ToList();
                LaunchesListView.ItemsSource = filtered;
            }
        }

        private void LaunchesListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadingLaunchInfo.IsActive = true;
            LaunchData.Visibility = Visibility.Collapsed;
            _currentLaunch = LaunchesListView.SelectedItem as LaunchInfo;

            Gallery.ItemsSource = _currentLaunch?.Links.Flickr.Original;
            FlipViewPipsPager.NumberOfPages = _currentLaunch.Links.Flickr.Original.Count;

            if (!string.IsNullOrEmpty(_currentLaunch?.Details))
            {
                LaunchDetails.Text = _currentLaunch?.Details;
            }

            if (_currentLaunch?.Links.Flickr.Original.Count == 0)
            {
                LaunchPhotos.Visibility = Visibility.Collapsed;
            }
            else
            {
                LaunchDetails.Margin = new Thickness(24, 0, 0, 0);
            }

            if (_currentLaunch?.Links.Reddit.Campaign == null)
            {
                RedditLink.Visibility = Visibility.Collapsed;
            }
            else
            {
                RedditLink.NavigateUri = new Uri(_currentLaunch.Links.Reddit.Campaign);
            }

            if (_currentLaunch?.Links.Webcast == null)
            {
                YouTubeLink.Visibility = Visibility.Collapsed;
            }
            else
            {
                YouTubeLink.NavigateUri = new Uri(_currentLaunch.Links.Webcast);
            }

            IsSuccess.IsChecked = _currentLaunch?.Success;
            IsUpcoming.IsChecked = _currentLaunch?.Upcoming;
            DateLocal.Text = $"Local date and time launch: {_currentLaunch?.DateLocal.ToString()}";
            DateUtc.Text = $"UTC date and time launch: {_currentLaunch?.DateUtc.ToString()}";

            LoadingLaunchInfo.IsActive = false;
            LaunchData.Visibility = Visibility.Visible;
        }
    }
}
