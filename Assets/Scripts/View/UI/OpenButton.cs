namespace BalloonCrusher.View
{
using UnityEngine;

public class OpenButton : AbstractButtonView
{
    [SerializeField] private GameObject _window = default;

    protected override void OnButtonClick() => _window.SetActive(true);
}
}
