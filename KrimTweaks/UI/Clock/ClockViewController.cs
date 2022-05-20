using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace KrimTweaks.UI.Clock;

[ViewDefinition("KrimTweaks.UI.BSML.ClockView.bsml")]
[HotReload(RelativePathToLayout = @"..\BSML\ClockView.bsml")]
internal class ClockViewController : BSMLAutomaticViewController
{
    private string _clockText = "";

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
}