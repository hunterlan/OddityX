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
using Oddity.Models.Crew;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames.CrewFrames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListCrewFrame : Page
    {
        private List<CrewInfo> crews;
        public ListCrewFrame()
        {
            this.InitializeComponent();
        }

        private void CrewItemChanged(object sender, RoutedEventArgs e)
        {
            var currentCrew = CrewListView.SelectedItem as CrewInfo;

            contentFrame.Navigate(typeof(DetailCrewFrame), currentCrew);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            crews = e.Parameter as List<CrewInfo>;
            CrewListView.ItemsSource = crews;
        }
    }
}
