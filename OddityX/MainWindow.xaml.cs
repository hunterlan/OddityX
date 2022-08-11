using Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Oddity;
using OddityX.Frames;
using OddityX.Frames.CrewFrames;
using OddityX.Frames.LaunchFrames;
using OddityX.Frames.RocketFrames;
using OddityX.Frames.HistroyEventFrames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private const string DefaultPage = "HistoryEvents";

        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;  // enable custom titlebar
            SetTitleBar(AppTitleBar);      // set user ui element as titlebar
        }

        private void nvTopLevelNav_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in nvTopLevelNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == DefaultPage)
                {
                    nvTopLevelNav.SelectedItem = item;
                    break;
                }
            }
            LoadingRing.Visibility = Visibility.Collapsed;
            contentFrame.Visibility = Visibility.Visible;
            contentFrame.Navigate(typeof(HistoryEventsCards));
        }

        private async void nvTopLevelNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedCategory = nvTopLevelNav.SelectedItem as NavigationViewItem;
            LoadingRing.Visibility = Visibility.Visible;
            contentFrame.Visibility = Visibility.Collapsed;

            if (selectedCategory.Tag.ToString() == "CapsuleFrame")
            {
                var capsules = await App.OddityCore.CapsulesEndpoint.GetAll().ExecuteAsync();
                contentFrame.Navigate(typeof(CapsulesFrame), capsules);
            }
            else if (selectedCategory.Tag.ToString() == "Ships")
            {
                var ships = await App.OddityCore.ShipsEndpoint.GetAll().ExecuteAsync();
                contentFrame.Navigate(typeof(ShipsFrame), ships);
            }
            else if (selectedCategory.Tag.ToString() == "CrewFrame")
            {
                var crews = await App.OddityCore.CrewEndpoint.GetAll().ExecuteAsync();
                contentFrame.Navigate(typeof(ListCrewFrame), crews);
            }
            else if (selectedCategory.Tag.ToString() == "RocketFrame")
            {
                var rockets = await App.OddityCore.RocketsEndpoint.GetAll().ExecuteAsync();
                contentFrame.Navigate(typeof(ListRocketsFrame), rockets);
            }
            else if (selectedCategory.Tag.ToString() == "LaunchFrame")
            {
                var launches = await App.OddityCore.LaunchesEndpoint.GetAll().ExecuteAsync();
                contentFrame.Navigate(typeof(ListLaunchesFrame), launches);
            }
            else if (selectedCategory.Tag.ToString() == "WIPFrame")
            {
                contentFrame.Navigate(typeof(WIPFrame));
            }
            else if (selectedCategory.Tag.ToString() == "Settings")
            {
                contentFrame.Navigate(typeof(SettingsFrame));
            }
            else
            {
                contentFrame.Navigate(typeof(HistoryEventsCards));
            }

            LoadingRing.Visibility = Visibility.Collapsed;
            contentFrame.Visibility = Visibility.Visible;
            nvTopLevelNav.Header = selectedCategory.Content.ToString();
        }

        private void nvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
        }

        public bool IsNavViewOpen()
        {
            return nvTopLevelNav.IsPaneOpen;
        }
    }
}
