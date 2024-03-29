﻿using Microsoft.UI.Xaml;
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
using System.Reflection;
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
    public sealed partial class SettingsFrame : Page
    {
        public SettingsFrame()
        {
            this.InitializeComponent();
            if (App.m_window.Content is FrameworkElement rootElement)
            {
                switch (rootElement.RequestedTheme)
                {
                    case ElementTheme.Dark:
                        Dark.IsChecked = true;
                        break;
                    case ElementTheme.Light:
                        Light.IsChecked = true;
                        break;
                    default:
                        Default.IsChecked = true;
                        break;
                }

            }
        }

        private void ThemeRadioButtonChecked(object sender, RoutedEventArgs routedEventArgs)
        {
            var selectedTheme = ((RadioButton)sender)?.Tag?.ToString();
            if (selectedTheme == null) return;
            if (App.m_window.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = selectedTheme switch
                {
                    "Dark" => ElementTheme.Dark,
                    "Light" => ElementTheme.Light,
                    _ => ElementTheme.Default
                };
            }
        }
    }
}
