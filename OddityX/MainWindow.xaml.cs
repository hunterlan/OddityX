using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using OddityX.Frames;
using OddityX.Frames.LaunchFrames;
using OddityX.Frames.HistroyEventFrames;
using OddityX.Helpers.HistorySpaceX;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX;

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
        App.ErrorOccured += App_ErrorOccured;
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

        ChangePage(nvTopLevelNav.MenuItems.First() as NavigationViewItem);
        LoadingRing.Visibility = Visibility.Collapsed;
        ContentFrame.Visibility = Visibility.Visible;
    }

    private void NavigateBack(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        // Section header remaining as was
        App.TryGoBack(ContentFrame);
        var menuItems = nvTopLevelNav.MenuItems.ToList();
        foreach (var navItem in menuItems
                     .Select(item => item as NavigationViewItem)
                     .Where(navItem => string.Equals(navItem.Tag.ToString(), ContentFrame.Content.GetType().Name)))
        {
            ChangePage(navItem, true);
            break;
        }
    }

    private void nvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        NavigationViewItem selectedCategory = (NavigationViewItem)args.InvokedItemContainer;
        LoadingRing.Visibility = Visibility.Visible;
        ContentFrame.Visibility = Visibility.Collapsed;

        ChangePage(selectedCategory);

        LoadingRing.Visibility = Visibility.Collapsed;
        ContentFrame.Visibility = Visibility.Visible;
    }

    private async void ChangePage(NavigationViewItem selectedCategory, bool isNavigateBack = false)
    {
        if (!isNavigateBack)
        {
            switch (selectedCategory.Tag.ToString())
            {
                case "CapsulesFrame":
                {
                    var capsules = await App.OddityCore.CapsulesEndpoint.GetAll().ExecuteAsync();
                    ContentFrame.Navigate(typeof(CapsulesFrame), capsules);
                    break;
                }
                case "ShipsFrame":
                {
                    var ships = await App.OddityCore.ShipsEndpoint.GetAll().ExecuteAsync();
                    ContentFrame.Navigate(typeof(ShipsFrame), ships);
                    break;
                }
                case "CrewsFrame":
                {
                    var crews = await App.OddityCore.CrewEndpoint.GetAll().ExecuteAsync();
                    ContentFrame.Navigate(typeof(CrewsFrame), crews);
                    break;
                }
                case "RocketsFrame":
                {
                    var rockets = await App.OddityCore.RocketsEndpoint.GetAll().ExecuteAsync();
                    ContentFrame.Navigate(typeof(RocketsFrame), rockets);
                    break;
                }
                case "LaunchesFrame":
                {
                    var launches = await App.OddityCore.LaunchesEndpoint.GetAll().ExecuteAsync();
                    ContentFrame.Navigate(typeof(LaunchesFrame), launches);
                    break;
                }
                case "WIPFrame":
                    ContentFrame.Navigate(typeof(WIPFrame));
                    break;
                case "Settings":
                    ContentFrame.Navigate(typeof(SettingsFrame));
                    break;
                default:
                    ContentFrame.Navigate(typeof(HistoryEventsCards), await GetHistoryModels());
                    break;
            }
        }
        else
        {
            nvTopLevelNav.SelectedItem = selectedCategory;
        }

        nvTopLevelNav.Header = selectedCategory.Content.ToString();
    }

    private async Task<List<HistoryModel>> GetHistoryModels()
    {
        HistoryActions actions = new();

        try
        {
            return await actions.GetAll();
        }
        catch
        {
            return null;
        }
    }

    private void App_ErrorOccured()
    {
        ErrorBar.IsOpen = true;
        ErrorBar.Content = App.LastException.Message;
        ContentFrame.Margin = new Thickness(0, 75, 0, 0);
    }

    private void ErrorBar_CloseButtonClick(InfoBar sender, object args)
    {
        ContentFrame.Margin = new Thickness(0);
    }
}