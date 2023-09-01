namespace BalloonCrusher.Controller
{
using View;
using VContainer;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;
using System;
using System.Threading;
using System.Threading.Tasks;

public class BalloonSpawner : IInitializable, IDisposable
{
    [Inject] private readonly Balloon _balloonPrefab = default;
    [Inject] private readonly GameObject _gameScreen = default;
    
    private CancellationTokenSource _cancellationToken = new CancellationTokenSource();
    private List<Balloon> _balloonPool = new List<Balloon>();
    
    private int _poolCapacity = 50;
    private int _startDelayInMilliseconds = 4000;
    private float _startVelocity = 30f;
    private float _velocityMultiplayer = 1f;
    private float _velocityMultiplayerStep = 0.05f;
    private bool _isInit = false;
    
    public event Action onBurstedBalloon = delegate {};
    public event Action onMissedBalloon = delegate {};

    public void Initialize()
    {
        _isInit = true;
        
        CreateBalloonPool();
        ActivateBalloon();
        StartTimer();
    }

    public bool GetInitStatus() => _isInit;

    private async void StartTimer()
    {
        while (_isInit)
        {
            await Task.Delay(_startDelayInMilliseconds * (int)_velocityMultiplayer);
            ActivateBalloon();
        }
    }

    private void CreateBalloonPool()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            Balloon balloon = _balloonPrefab.Instantiate(_gameScreen.transform);
            balloon.onReset += ActivateBalloon;
            balloon.onClick += OnBurstBalloon;
            balloon.onCompletedMove += OnMissedBalloon;
            balloon.gameObject.SetActive(false);
            _balloonPool.Add(balloon);
        }
    }

    private void OnBurstBalloon() => onBurstedBalloon();
    
    private void OnMissedBalloon() => onMissedBalloon();

    private void ActivateBalloon()
    {
        if (_isInit && Time.timeScale != 0)
        {
            Balloon balloon = FindBalloon();
            balloon.Init(_startVelocity / _velocityMultiplayer);
            _velocityMultiplayer += _velocityMultiplayerStep;
        }
    }

    private Balloon FindBalloon()
    {
        Balloon balloon = _balloonPool.Find(balloon => !balloon.gameObject.activeSelf);

        if (balloon == null)
        {
            CreateBalloonPool();
            balloon = _balloonPool.Find(balloon => !balloon.gameObject.activeSelf);
        }

        return balloon;
    }

    public void Reset()
    {
        _isInit = false;
        _cancellationToken.Dispose();
        _balloonPool.ForEach(balloon => balloon.Reset());
        _velocityMultiplayer = 1f;
    }

    void IDisposable.Dispose()
    {
        Reset();
        
        _balloonPool.ForEach(balloon =>
        {
            balloon.onReset -= ActivateBalloon;
            balloon.onClick -= OnBurstBalloon;
            balloon.onCompletedMove -= OnMissedBalloon;
        });
    }
}
}