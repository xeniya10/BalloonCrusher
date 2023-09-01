namespace BalloonCrusher.View
{
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class AbstractButtonView : MonoBehaviour
{
    private Button _button = default;
    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    protected virtual void OnDestroy() => _button.onClick.RemoveListener(OnButtonClick);

    protected abstract void OnButtonClick();
}
}