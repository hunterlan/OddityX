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
        private readonly HistoryActions _actions;
        private List<HistoryModel> _historyEvents;

        public HistoryEventsCards()
        {
            _actions = new();
            InitializeComponent();
            InitializeCards();
        }

        private async void InitializeCards()
        {
            var isError = false;

            try
            {
                _historyEvents = await _actions.GetAll();
            }
            catch
            {
                isError = true;
            }
            finally
            {
                LoadingHistory.IsActive = false;
            }

            if (isError)
            {
                TextBlock errorText = new()
                {
                    Text = "Sorry, but we can't show history events for now."
                };
                PanelForCards.Children.Add(errorText);
            }
            else
            {
                var i = 0;

                while (i < _historyEvents.Count)
                {

                    StackPanel card = new()
                    {
                        Orientation = Orientation.Vertical,
                        Padding = new Thickness(0, 20, 0, 0)
                    };

                    TextBlock title = new()
                    {
                        Text = _historyEvents[i].Title,
                        FontSize = 24
                    };

                    TextBlock date = new()
                    {
                        Text = _historyEvents[i].DateUtc.ToString("MM/dd/yyyy HH:mm")
                    };

                    TextBlock description = new()
                    {
                        Text = _historyEvents[i].Details,
                        TextWrapping = TextWrapping.WrapWholeWords
                    };

                    card.Children.Add(title);
                    card.Children.Add(date);
                    card.Children.Add(description);


                    PanelForCards.Children.Add(card);
                    i++;
                }
            }
        }
    }
}
