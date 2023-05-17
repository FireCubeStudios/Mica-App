using Microsoft.Toolkit.Uwp.Helpers;
using System;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Mica
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(AppTitleBar);
            Tip.IsOpen = SystemInformation.Instance.IsFirstRun ? true : false;
            InfiniteMicaSetting.Visibility = IsWin11() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ThemeSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = ThemeSwitch.IsOn ? ElementTheme.Dark : ElementTheme.Light;
                if(Mica10 is not null)
                    Mica10.ForcedTheme = ThemeSwitch.IsOn ? ApplicationTheme.Dark : ApplicationTheme.Light;
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Window.Current.Bounds.Width < 800)
            {
                AboutPanel.Visibility = Visibility.Collapsed;
                CloseButton.Margin = new Thickness(50, 54, 50, 100);
                SettingsGrid.Margin = new Thickness(50, 50, 50, 100);
            }
            else
            {
                AboutPanel.Visibility = Visibility.Visible;
                CloseButton.Margin = new Thickness(100, 54, 300, 100);
                SettingsGrid.Margin = new Thickness(100, 50, 300, 100);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Tip.IsOpen = true;

        private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e) => SettingsPanel.Visibility = Visibility.Visible;

        private void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e) => SettingsPanel.Visibility = Visibility.Visible;

        private void CloseButton_Click(object sender, RoutedEventArgs e) => SettingsPanel.Visibility = Visibility.Collapsed;

        private void Tip_ActionButtonClick(Microsoft.UI.Xaml.Controls.TeachingTip sender, object args) => SettingsPanel.Visibility = ((sender.IsOpen = false) is false) ? Visibility.Visible : Visibility.Visible;

        private void ColorPicker_ColorChanged(Microsoft.UI.Xaml.Controls.ColorPicker sender, Microsoft.UI.Xaml.Controls.ColorChangedEventArgs args)
        {
            Gs2.Color = Helpers.ColorHelper.ChangeColorBrightness(sender.Color, 0.51f);
            Gs1.Color = Helpers.ColorHelper.ChangeColorBrightness(sender.Color, 0.25f);
            Gs0.Color = Helpers.ColorHelper.ChangeColorBrightness(sender.Color, 0.02f);
            Glow.Color = sender.Color;
        }

        private async void NewMica_Click(object sender, RoutedEventArgs e) => await Launcher.LaunchUriAsync(new Uri("mica:"));

        public bool IsWin11() => (Environment.OSVersion.Version.Build >= 22000);

        private void InfiniteSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (Mica is not null)
                Mica.Visibility = InfiniteSwitch.IsOn ? Visibility.Visible : Visibility.Collapsed;
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = ThemeSwitch.IsOn ? ElementTheme.Dark : ElementTheme.Light;
                if (Mica10 is not null)
                    Mica10.ForcedTheme = ThemeSwitch.IsOn ? ApplicationTheme.Dark : ApplicationTheme.Light;
            }
        }
    }
}
