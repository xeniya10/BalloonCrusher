namespace BalloonCrusher.Model
{
public class Score
{
    private int _scoreNumber = default;

    public int GetScore() => _scoreNumber;

    public void AddPoints(int points) => _scoreNumber += points;

    public void ResetScore() => _scoreNumber = 0;
}
}