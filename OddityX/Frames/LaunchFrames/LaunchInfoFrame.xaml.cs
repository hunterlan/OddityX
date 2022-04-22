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
    public sealed partial class LaunchInfoFrame : Page
    {
        private LaunchInfo _launch;
        public LaunchInfoFrame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _launch = e.Parameter as LaunchInfo;
            if (!string.IsNullOrEmpty(_launch?.Details))
            {
                LaunchDetails.Text = _launch?.Details;
            }

            if (_launch?.Links.Flickr.Original.Count == 0)
            {
                LaunchPhotos.Visibility = Visibility.Collapsed;
            }

            if (_launch?.Links.Reddit.Campaign == null)
            {
                RedditLink.Visibility = Visibility.Collapsed;
            }
            else
            {
                RedditLink.NavigateUri = new Uri(_launch.Links.Reddit.Campaign);
            }

            if (_launch?.Links.Webcast == null)
            {
                YouTubeLink.Visibility = Visibility.Collapsed;
            }
            else
            {
                YouTubeLink.NavigateUri = new Uri(_launch.Links.Webcast);
            }
        }
    }
}
