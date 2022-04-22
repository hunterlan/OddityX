using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.Generic;
using Oddity.Models.Rockets;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames.RocketFrames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListRocketsFrame : Page
    {
        private List<RocketInfo> rockets;
        public ListRocketsFrame()
        {
            this.InitializeComponent();
        }

        private void RocketItemChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var currentRocket = RocketsListView.SelectedItem as RocketInfo;
            ContentFrame.Navigate(typeof(RocketInfoFrame), currentRocket);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rockets = e.Parameter as List<RocketInfo>;
            RocketsListView.ItemsSource = rockets;
        }
    }
}
