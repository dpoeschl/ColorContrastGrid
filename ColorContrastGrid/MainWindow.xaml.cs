using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorContrastGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateInfo();
        }

        private void PopulateInfo()
        {
            var backgrounds = new[]
            {
                Color.FromRgb(0xFF, 0xFF, 0xFF),
                Color.FromRgb(0xF6, 0xB9, 0x4D),
                Color.FromRgb(0xAD, 0xD6, 0xFF),
                Color.FromRgb(0xA8, 0xAC, 0x94),
                Color.FromRgb(0xEB, 0xF1, 0xDD),
                Color.FromRgb(0xD7, 0xE3, 0xBC),
                Color.FromRgb(0xFF, 0xCC, 0xCC),
                Color.FromRgb(0xFF, 0x99, 0x99)
            };

            var backgroundNames = new[]
            {
                "White",
                "Find",
                "Highlight Text",
                "Highlight + Find",
                "Diff light green",
                "Diff dark green",
                "Diff light red",
                "Diff dark red"
            };

            var foregrounds = new[]
            {
                Color.FromRgb(0x00, 0x00, 0x00),
                Color.FromRgb(0x2B, 0x91, 0xAF),
                Color.FromRgb(0x00, 0x00, 0xFF),
                Color.FromRgb(0xA3, 0x15, 0x15),
                Color.FromRgb(0x80, 0x00, 0x00),
                Color.FromRgb(0x80, 0x80, 0x80),
                Color.FromRgb(0x64, 0x64, 0xB9),
                Color.FromRgb(0x84, 0x46, 0x46),
                Color.FromRgb(0xB9, 0x64, 0x64),
                Color.FromRgb(0xC0, 0xC0, 0xC0)
            };

            var foregroundNames = new[]
            {
                "Text",
                "Types",
                "Keywords",
                "Strings (1)",
                "Strings (2)",
                "Doc comments",
                "VBXML brackets",
                "VBXML elements",
                "VBXML attributes",
                "VBXML CDATA"
            };

            grid.RowDefinitions.Add(new RowDefinition());
            foreach (var background in backgrounds)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            foreach (var foreground in foregrounds)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < backgrounds.Length; i++)
            {
                var textBlock = new TextBlock
                {
                    Text = backgroundNames[i],
                    FontWeight = FontWeights.Bold
                };

                textBlock.SetValue(Grid.RowProperty, i + 1);
                textBlock.SetValue(Grid.ColumnProperty, 0);

                grid.Children.Add(textBlock);

                for (int j = 0; j < foregrounds.Length; j++)
                {
                    textBlock = new TextBlock
                    {
                        Text = foregroundNames[j],
                        FontWeight = FontWeights.Bold
                    };

                    textBlock.SetValue(Grid.RowProperty, 0);
                    textBlock.SetValue(Grid.ColumnProperty, j + 1);

                    grid.Children.Add(textBlock);

                    var ratio = LuminosityContrast(backgrounds[i], foregrounds[j]);

                    textBlock = new TextBlock
                    {
                        Text = "Test " + (string.Format("({0:0.00})", ratio)),
                        Background = new SolidColorBrush(backgrounds[i]),
                        Foreground = new SolidColorBrush(foregrounds[j]),
                        FontWeight = FontWeights.Bold
                    };

                    var border = new Border
                    {
                        BorderThickness = new Thickness(4),
                        BorderBrush = new SolidColorBrush(ratio < 4.5 ? Colors.Red : Colors.White),
                        Child = textBlock
                    };

                    border.SetValue(Grid.RowProperty, i + 1);
                    border.SetValue(Grid.ColumnProperty, j + 1);

                    grid.Children.Add(border);
                }
            }
        }

        // Algorithm from http://andora.us/blog/2011/03/03/choosing-foreground-using-luminosity-contrast-ratio/
        // that seems to match Colour Contrast Analyzer.
        private double LuminosityContrast(Color foreground, Color background)
        {
            var foregroundLuminosity = RelativeLuminance(foreground.R, foreground.G, foreground.B);
            var backgroundLuminosity = RelativeLuminance(background.R, background.G, background.B);

            if (foregroundLuminosity > backgroundLuminosity)
            {
                return (foregroundLuminosity + 0.05) / (backgroundLuminosity + 0.05);
            }
            else
            {
                return (backgroundLuminosity + 0.05) / (foregroundLuminosity + 0.05);
            }
        }

        private double RelativeLuminance(byte r, byte g, byte b)
        {
            double rs = ((double)r / 255);
            double gs = ((double)g / 255);
            double bs = ((double)b / 255);
            double R = 0;
            double G = 0;
            double B = 0;

            R = (rs <= 0.03928) ? (double)(rs / 12.92) : Math.Pow(((rs + 0.055) / 1.055), 2.4);
            G = (gs <= 0.03928) ? (double)(gs / 12.92) : Math.Pow(((gs + 0.055) / 1.055), 2.4);
            B = (bs <= 0.03928) ? (double)(bs / 12.92) : Math.Pow(((bs + 0.055) / 1.055), 2.4);

            return ((double)(0.2126 * R)) + ((double)(0.7152 * G)) + ((double)(0.0722 * B));
        }
    }
}
