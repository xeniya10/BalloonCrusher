namespace BalloonCrusher.View
{
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreView : MonoBehaviour
{
    private Text _scoreText = default;
    private int DEFAULT_SCORE = 0;
    public void Reset() => SetScore(DEFAULT_SCORE);

    public void SetScore(int score)
    {
        if (_scoreText == null)
        {
            _scoreText = GetComponent<Text>();
        }
        
        _scoreText.text = score.ToString();
    }
}
}
