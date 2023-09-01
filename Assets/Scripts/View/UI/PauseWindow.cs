namespace BalloonCrusher.View
{
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    private void OnEnable() => UpdateTimeScale(false);

    private void OnDisable() => UpdateTimeScale(true);

    private void UpdateTimeScale(bool isRunTime) => Time.timeScale = isRunTime ? 1 : 0;
}
}
