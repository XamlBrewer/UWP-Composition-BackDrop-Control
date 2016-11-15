using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XamlBrewer.Uwp.BackDropControl.Sample
{
    public sealed partial class DialogPage : Page
    {
        public DialogPage()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            await ContentDialog.ShowAsync();
        }
    }
}
