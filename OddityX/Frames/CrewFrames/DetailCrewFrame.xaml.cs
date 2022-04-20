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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            currentCrew = e.Parameter as CrewInfo;

            if (currentCrew.Image != null)
            {
                CrewPicture.ProfilePicture = new BitmapImage(new Uri(currentCrew.Image));
            }

            NameCrew.Text = currentCrew.Name;

            CrewInfoPanel.Visibility = Visibility.Visible;
            Progress.Visibility = Visibility.Collapsed;
        }
    }
}
