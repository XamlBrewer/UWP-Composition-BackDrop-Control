using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace XamlBrewer.Uwp.BackDropControl.Sample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TintColorSlider_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (e.NewValue < 1)
            {
                BackDrop.TintColor = Colors.Transparent;
            }
            else
            {
                var color = (byte)(e.NewValue * 2.55);
                BackDrop.TintColor = Color.FromArgb(128, color, (byte)(255 - color), (byte)(255 - color));
            }
        }
    }
}
