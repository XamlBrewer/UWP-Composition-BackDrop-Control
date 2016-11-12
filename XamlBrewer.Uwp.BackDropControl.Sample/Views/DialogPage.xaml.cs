using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XamlBrewer.Uwp.BackDropControl.Sample
{
    public sealed partial class DialogPage : Page
    {
        public DialogPage()
        {
            this.InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await ContentDialog.ShowAsync();
        }
    }
}
