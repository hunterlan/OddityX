using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Oddity.Models.Capsules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class CapsuleInfoFrame : Page
    {
        CapsuleInfo currentCupsule;
        public CapsuleInfoFrame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            currentCupsule = e.Parameter as CapsuleInfo;
            Serial.Text = $"Serial: {currentCupsule.Serial}";
            Status.Text = $"Status: {currentCupsule.Status.ToString()}";
            ReuseCount.Text = $"Reused count: {currentCupsule.ReuseCount}";
            WaterLandings.Text = $"Water landings: {currentCupsule?.WaterLandings?.ToString()}";
            LandLandings.Text = $"Land landings: {currentCupsule?.LandLandings?.ToString()}";
            CountLaunches.Text = $"Count launches: {currentCupsule.Launches.Count}";
        }
    }
}
