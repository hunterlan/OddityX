using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Oddity.Models.Rockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RocketsFrame : Page
    {
        private List<RocketInfo> _rockets;
        private RocketInfo _currentRocket;

        public RocketsFrame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _rockets = e.Parameter as List<RocketInfo>;
            RocketsListView.ItemsSource = _rockets;
        }

        private void RocketsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RocketDataLoading.IsActive = true;
            RocketData.Visibility = Visibility.Collapsed;

            _currentRocket = RocketsListView.SelectedItem as RocketInfo;
            Description.Text = _currentRocket?.Description;
            Gallery.ItemsSource = _currentRocket?.FlickrImages;

            RocketDataLoading.IsActive = false;
            RocketData.Visibility = Visibility.Visible;
        }

        private void FindRocketByName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FindRocketByName.Text) || string.IsNullOrEmpty(FindRocketByName.Text))
            {
                RocketsListView.ItemsSource = _rockets;
            }
            else
            {
                var currentText = FindRocketByName.Text;
                var filtered = _rockets.Where(c => c.Name.Contains(currentText)).ToList();
                RocketsListView.ItemsSource = filtered;
            }
        }
    }
}
