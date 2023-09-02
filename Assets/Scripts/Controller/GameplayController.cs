namespace BalloonCrusher.Controller
{
using View;
using Model;
using VContainer;
using VContainer.Unity;
using System;

public class GameplayController : IInitializable
{
    [Inject] private readonly ScoreView _scoreView = default;
    [Inject] private readonly LifeView _lifeView = default;
    [Inject] private readonly BalloonSpawner _balloonSpawner = default;
    [Inject] private readonly RecordBoard _recordBoard = default;
    [Inject] private readonly RecordRepository _recordRepository = default;
    [Inject] private readonly PlayButton _playButton = default;
    
    private readonly Score _score = new Score();
    
    private int _balloonPoints = 1;
    private int _currentLifeNumber = 0;
    private int _totalLifeNumber = 3;

    void IInitializable.Initialize() => _playButton.AddClickCallback(Start);

    private void Start()
    {
        if (!_balloonSpawner.GetInitStatus())
        {
            Reset();
            _balloonSpawner.Initialize();
            _balloonSpawner.onBurstedBalloon += AddPoints;
            _balloonSpawner.onMissedBalloon += OnMissedBalloon;
        }
    }

    private void OnMissedBalloon()
    {
        _currentLifeNumber++;
        _lifeView.InactivateLife();

        if (_currentLifeNumber == _totalLifeNumber)
        {
            _recordRepository.Write(new Record(DateTime.Now.ToString(), _score.GetScore()));
            _recordBoard.gameObject.SetActive(true);
            _balloonSpawner.Reset();
        }
    }

    private void AddPoints()
    {
        _score.AddPoints(_balloonPoints);
        _scoreView.SetScore(_score.GetScore());
    }

    private void Reset()
    {
        _currentLifeNumber = 0;
        
        _score.ResetScore();
        _scoreView.Reset();
        _lifeView.Reset();

        _balloonSpawner.onBurstedBalloon -= AddPoints;
        _balloonSpawner.onMissedBalloon -= OnMissedBalloon;
    }
}
}