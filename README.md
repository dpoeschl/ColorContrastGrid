# ColorContrastGrid
Displays a grid of color contrast ratios for various foregrounds against various backgrounds. If contrast ratios don't meet a 4.5:1 requirement, it highlights the pair of colors in red. Contrast ratios are calculated using the algorithm from http://andora.us/blog/2011/03/03/choosing-foreground-using-luminosity-contrast-ratio/, which seems to match [Colour Contrast Analyser](https://www.paciellogroup.com/resources/contrastanalyser/) in all of my testing.

# Example
![](https://github.com/dpoeschl/ColorContrastGrid/blob/master/ColorGridExample2.png)

# Instructions
1. Download or clone the repository
2. Open `ColorContrastGrid.sln` in Visual Studio
3. Open `MainWindow.xaml.cs` and adjust the `backgrounds` / `backgroundNames` / `foregrounds` / `foregroundNames` to suit your needs
4. Build and run
