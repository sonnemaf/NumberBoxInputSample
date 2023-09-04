# NumberBoxInputSample

WinUI3 and UWP Sample project showing how to limit keyboard input on a NumberBox, only allow nubers, no letters.

Uses `x:Bind` to the [PreviewKeyDownHandler](https://github.com/sonnemaf/NumberBoxInputSample/blob/master/WinUI3App1/PreviewKeyDownHandler.cs) helper class.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<Window x:Class="WinUI3App1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:helpers="using:Helpers"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d">

    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <StackPanel Width="208"
                    HorizontalAlignment="Center"
                    VerticalAlignment="Center"
                    Spacing="8">

            <NumberBox Header="A"
                       PreviewKeyDown="{x:Bind helpers:PreviewKeyDownHandler.RejectLetters}"
                       SpinButtonPlacementMode="Inline" />

            <NumberBox Header="B"
                       PreviewKeyDown="{x:Bind helpers:PreviewKeyDownHandler.AcceptNumbersOnly}" />

            <StackPanel Orientation="Horizontal"
                        PreviewKeyDown="{x:Bind helpers:PreviewKeyDownHandler.AcceptNumbersOnly}"
                        Spacing="8">
                <NumberBox Width="100"
                           Header="C" />
                <NumberBox Width="100"
                           Header="D" />
            </StackPanel>
        </StackPanel>
    </Grid>
</Window>
```




