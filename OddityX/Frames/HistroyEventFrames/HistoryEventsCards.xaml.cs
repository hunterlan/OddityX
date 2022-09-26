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
using OddityX.Helpers.HistorySpaceX;
using Syncfusion.UI.Xaml.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX.Frames.HistroyEventFrames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HistoryEventsCards : Page
    {
        private List<HistoryModel> _historyEvents;

        public HistoryEventsCards() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _historyEvents = e.Parameter as List<HistoryModel>;
            if (_historyEvents == null || !_historyEvents.Any())
            {
                EventsHistorical.Visibility = Visibility.Collapsed;
                EmptyError.Visibility = Visibility.Visible;
            }
        }
    }
}
