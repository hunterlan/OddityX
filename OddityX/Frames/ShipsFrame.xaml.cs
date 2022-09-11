using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Media.Imaging;
using Oddity.Models.Ships;
using OddityX.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShipsFrame : Page
    {
        private List<ShipInfo> _ships;
        public ShipInfoView CurrentShip;

        public ShipsFrame()
        {
            _ships = new();
            this.InitializeComponent();
        }

        private async void ShipsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadingShipData.IsActive = true;
            var selectedItem = ShipsList.SelectedItem as string;
            CurrentShip = new(_ships.FirstOrDefault(s => s.Name.Equals(selectedItem)));

            if (CurrentShip != null)
            {
                ShipName.Text = CurrentShip.ShipName;
                ShipModel.Text = CurrentShip.ShipModel is not null ? $"Model: {CurrentShip.ShipModel}" : "Model isn't set up";
                ShipType.Text = CurrentShip.ShipType is not null ? $"Type: {CurrentShip.ShipType}" : "Type isn't set up";
                ShipStatus.Text = CurrentShip.ShipStatus;

                if (CurrentShip.ShipLink is null)
                {
                    ShipLink.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ShipLink.NavigateUri = CurrentShip.ShipLink;
                    ShipLink.Visibility = Visibility.Visible;
                }

                if (CurrentShip.ShipImage is null)
                {
                    ShipImage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ShipImage.Source = new BitmapImage(new Uri(CurrentShip.ShipImage));
                    ShipImage.Visibility = Visibility.Visible;
                }

                LaunchesList.ItemsSource = await CurrentShip.GetShipLaunches();
            }

            LoadingShipData.IsActive = false;
            ShipData.Visibility = Visibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _ships = e.Parameter as List<ShipInfo>;
            if (_ships != null) ShipsList.ItemsSource = _ships.Select(s => s.Name);
        }
    }
}
