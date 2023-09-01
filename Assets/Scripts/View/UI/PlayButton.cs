namespace BalloonCrusher.View
{
using System;

public class PlayButton : AbstractButtonView
{
    private event Action _onClickCallback = delegate {};
    
    protected override void OnButtonClick() => _onClickCallback();

    public void AddClickCallback(Action callback) => _onClickCallback = callback;
}
}
