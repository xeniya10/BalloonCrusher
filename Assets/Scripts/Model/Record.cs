namespace BalloonCrusher.Model
{
using System;

[Serializable]
public class Record
{
    public string ID = default;
    public string Name = default;
    public int Score = default;

    private const string DEFAULT_NAME = "AAA";

    public Record(string id, int score, string name = DEFAULT_NAME)
    {
        ID = id;
        Name = name;
        Score = score;
    }
}
}
