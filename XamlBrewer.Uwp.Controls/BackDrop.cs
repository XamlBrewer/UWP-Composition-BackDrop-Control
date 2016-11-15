using System;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Microsoft.Graphics.Canvas.Effects;

namespace XamlBrewer.Uwp.Controls
{
    /// <summary>
    /// A lightweight control to add blur and tint effect.
    /// </summary>
    public class BackDrop : Control
    {
        private Compositor _compositor;
        private readonly SpriteVisual _blurVisual;
        private readonly CompositionBrush _blurBrush;
        private readonly Visual _rootVisual;
        private readonly CompositionSurfaceBrush _noiseBrush;

        /// <summary>
        /// The blur amount property.
        /// </summary>
        public static readonly DependencyProperty BlurAmountProperty =
            DependencyProperty.Register(
                nameof(BlurAmount),
                typeof(double),
                typeof(BackDrop),
                new PropertyMetadata(10d, OnBlurAmountChanged));

        /// <summary>
        /// The tint color property.
        /// </summary>
        public static readonly DependencyProperty TintColorProperty = 
            DependencyProperty.Register(
                nameof(TintColor), 
                typeof(Color), 
                typeof(BackDrop), 
                new PropertyMetadata(Colors.Transparent, OnTintColorChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="BackDrop"/> class.
        /// </summary>
        public BackDrop()
        {
            _rootVisual = ElementCompositionPreview.GetElementVisual(this);
            Compositor = _rootVisual.Compositor;
            _blurVisual = Compositor.CreateSpriteVisual();
            _noiseBrush = Compositor.CreateSurfaceBrush();

            CompositionEffectBrush brush = BuildBlurBrush();
            brush.SetSourceParameter("source", _compositor.CreateBackdropBrush());
            _blurBrush = brush;
            _blurVisual.Brush = _blurBrush;

            TintColor = Colors.Transparent;

            ElementCompositionPreview.SetElementChildVisual(this, _blurVisual);

            Loading += OnLoading;
            Unloaded += OnUnloaded;
        }

        public double BlurAmount
        {
            get { return (double)GetValue(BlurAmountProperty); }
            set { SetValue(BlurAmountProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tint color.
        /// </summary>
        /// <value>The tint color.</value>
        public Color TintColor
        {
            get { return (Color)GetValue(TintColorProperty); }
            set { SetValue(TintColorProperty, value); }
        }

        private Compositor Compositor
        {
            get
            {
                return _compositor;
            }

            set
            {
                _compositor = value;
            }
        }

        private static void OnBlurAmountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var backDrop = d as BackDrop;

            if (backDrop == null) return;

            backDrop._blurBrush.Properties.InsertScalar("Blur.BlurAmount", (float)(double)e.NewValue);
            backDrop._rootVisual.Properties.InsertScalar(nameof(BlurAmount), (float)(double)e.NewValue);
        }

        private static void OnTintColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var backDrop = d as BackDrop;

            if (backDrop == null) return;

            backDrop._blurBrush.Properties.InsertColor("Color.Color", (Color)e.NewValue);
            backDrop._rootVisual.Properties.InsertColor(nameof(TintColor), (Color)e.NewValue);
        }

        private async void OnLoading(FrameworkElement sender, object args)
        {
            SurfaceLoader.Initialize(Compositor);

            SizeChanged += OnSizeChanged;
            OnSizeChanged(this, null);

            _noiseBrush.Surface = await SurfaceLoader.LoadFromUri(new Uri("ms-appx:///XamlBrewer.Uwp.Controls/Assets/MoreNoise.png"));
            _noiseBrush.Stretch = CompositionStretch.UniformToFill;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            SurfaceLoader.Uninitialize();
            SizeChanged -= OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_blurVisual != null)
            {
                _blurVisual.Size = new System.Numerics.Vector2((float)ActualWidth, (float)ActualHeight);
            }
        }

        private CompositionEffectBrush BuildBlurBrush()
        {
            var blurEffect = new GaussianBlurEffect()
            {
                Name = "Blur",
                BlurAmount = 0.0f,
                BorderMode = EffectBorderMode.Hard,
                Optimization = EffectOptimization.Balanced,
                Source = new CompositionEffectSourceParameter("source"),
            };

            var blendEffect = new BlendEffect
            {
                Background = blurEffect,
                Foreground = new ColorSourceEffect { Name = "Color", Color = Color.FromArgb(64, 255, 255, 255) },
                Mode = BlendEffectMode.SoftLight
            };

            var saturationEffect = new SaturationEffect
            {
                Source = blendEffect,
                Saturation = 1.75f,
            };

            var finalEffect = new BlendEffect
            {
                Foreground = new CompositionEffectSourceParameter("NoiseImage"),
                Background = saturationEffect,
                Mode = BlendEffectMode.Screen,
            };

            var factory = Compositor.CreateEffectFactory(
                finalEffect,
                new[] { "Blur.BlurAmount", "Color.Color" });

            var brush = factory.CreateBrush();
            brush.SetSourceParameter("NoiseImage", _noiseBrush);
            return brush;
        }
    }
}
