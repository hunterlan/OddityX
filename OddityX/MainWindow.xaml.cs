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
using Oddity;
using OddityX.Frames;
using OddityX.Frames.CrewFrames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private OddityCore oddity;
        public MainWindow()
        {
            this.InitializeComponent();
            oddity = new OddityCore();
        }

        private async void nvTopLevelNav_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in nvTopLevelNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "CapsuleFrame")
                {
                    nvTopLevelNav.SelectedItem = item;
                    break;
                }
            }
            var capsules = await oddity.CapsulesEndpoint.GetAll().ExecuteAsync();

            LoadingRing.Visibility = Visibility.Collapsed;
            contentFrame.Visibility = Visibility.Visible;
            contentFrame.Navigate(typeof(ListCapsulesFrame), capsules);
        }

        private async void nvTopLevelNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedCategory = nvTopLevelNav.SelectedItem as NavigationViewItem;
            LoadingRing.Visibility = Visibility.Visible;
            contentFrame.Visibility = Visibility.Collapsed;

            if (selectedCategory.Tag.ToString() == "CapsuleFrame")
            {
                var capsules = await oddity.CapsulesEndpoint.GetAll().ExecuteAsync();
                LoadingRing.Visibility = Visibility.Collapsed;
                contentFrame.Visibility = Visibility.Visible;
                contentFrame.Navigate(typeof(ListCapsulesFrame), capsules);
            }
            else if (selectedCategory.Tag.ToString() == "CrewFrame")
            {
                var crews = await oddity.CrewEndpoint.GetAll().ExecuteAsync();
                LoadingRing.Visibility = Visibility.Collapsed;
                contentFrame.Visibility = Visibility.Visible;
                contentFrame.Navigate(typeof(ListCrewFrame), crews);
            }
            else if (selectedCategory.Tag.ToString() == "WIPFrame")
            {
                contentFrame.Navigate(typeof(Frames.WIPFrame));
            }

            nvTopLevelNav.Header = selectedCategory.Content.ToString();
        }

        private void nvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
        }
    }
}
