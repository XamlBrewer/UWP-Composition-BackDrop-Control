﻿using Windows.UI.Xaml.Controls;
using XamlBrewer.Uwp.BackDropControl.Sample;

namespace Mvvm
{
    class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menu
            // Symbol enumeration is here: https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.symbol.aspx
            Menu.Add(new MenuItem() { Glyph = Symbol.Home, Text = "Home", NavigationDestination = typeof(MainPage) });
            Menu.Add(new MenuItem() { Glyph = Symbol.Message, Text = "FlyOut", NavigationDestination = typeof(FlyOutPage) });
            Menu.Add(new MenuItem() { Glyph = Symbol.PreviewLink, Text = "Dialog", NavigationDestination = typeof(DialogPage) });
            Menu.Add(new MenuItem() { Glyph = Symbol.ViewAll, Text = "Squares", NavigationDestination = typeof(SquareOfSquaresPage) });
        }
    }
}
