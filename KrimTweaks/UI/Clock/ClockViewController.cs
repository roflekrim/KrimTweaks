using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace KrimTweaks.UI.Clock;

[ViewDefinition("KrimTweaks.UI.BSML.Clock.ClockView.bsml")]
[HotReload(RelativePathToLayout = @"..\BSML\Clock\ClockView.bsml")]
internal class ClockViewController : BSMLAutomaticViewController
{
    private string _clockText = "";
    private string _clockColor = "#FFFFFF";
    
    [UIValue("clock-text")]
    public string ClockText
    {
        get => _clockText;
        set
        {
            _clockText = value;
            NotifyPropertyChanged(nameof(ClockText));
        }
    }

    [UIValue("clock-color")]
    public string ClockColor
    {
        get => _clockColor;
        set
        {
            _clockColor = value;
            NotifyPropertyChanged(nameof(ClockColor));
        }
    }
}