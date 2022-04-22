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
using Oddity.Models.Launches;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames.LaunchFrames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListLaunchesFrame : Page
    {
        private List<LaunchInfo> _launches;
        public ListLaunchesFrame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _launches = e.Parameter as List<LaunchInfo>;
            LaunchesListView.ItemsSource = _launches;
        }

        private void LaunchesItemChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var currentLaunch = LaunchesListView.SelectedItem as LaunchInfo;
            ContentFrame.Navigate(typeof(LaunchInfoFrame), currentLaunch);
        }

        /*private void FindLaunchByName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var currentLaunches = LaunchesListView.ItemsSource as List<LaunchInfo>;
            if (string.IsNullOrWhiteSpace(FindLaunchByName.Text) || string.IsNullOrEmpty(FindLaunchByName.Text))
            {
                LaunchesListView.ItemsSource = _launches;
            }
            else
            {
                var currentText = FindLaunchByName.Text;
                var filtered = currentLaunches?.Where(l => l.Name.Contains(currentText)).ToList();
                LaunchesListView.ItemsSource = filtered;
            }
        }*/
    }
}
