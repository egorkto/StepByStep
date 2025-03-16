public class SignedFloatSliderValuePresenter : SliderValuePresenter
{
    protected override void OnChanged(float value) {
        Text.text = (value > 0 ? "+" : "") + value.ToString("0.0");
    }
}
