using System;
using Windows.UI.Xaml.Controls;
using XamlBrewer.Uwp.Controls;

namespace XamlBrewer.Uwp.BackDropControl.Sample
{
    public sealed partial class SquareOfSquaresPage : Page
    {
        public SquareOfSquaresPage()
        {
            this.InitializeComponent();
            Loaded += SquareOfSquaresPage_Loaded;
        }

        private void SquareOfSquaresPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            foreach (var square in SquareOfSquares.Squares)
            {
                square.Content = new BackDrop
                {
                    Height = square.ActualHeight,
                    Width = square.ActualWidth,
                    TintColor = square.RandomColor(),
                    BlurAmount = random.Next(40)
                };
            }
        }
    }
}
