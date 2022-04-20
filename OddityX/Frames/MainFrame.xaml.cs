﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Oddity;
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
    public sealed partial class MainFrame : Page
    {
        OddityCore oddity;
        public MainFrame()
        {
            this.InitializeComponent();
            oddity = new OddityCore();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var allCapsules = await oddity.CapsulesEndpoint.GetAll().ExecuteAsync();

            progressRing.Visibility = Visibility.Collapsed;
            contentFrame.Visibility = Visibility.Visible;

            contentFrame.Navigate(typeof(ListCapsulesFrame), allCapsules);
        }
    }
}
