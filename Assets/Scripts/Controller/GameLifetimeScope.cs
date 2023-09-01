namespace BalloonCrusher.Controller
{
using View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [Header("UI")]
    [SerializeField] private RecordBoard _recordBoard;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private LifeView _lifeView;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private PlayButton _playButton;

    [Header("Prefabs")]
    [SerializeField] private Balloon _balloonPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterUIComponents(builder);
        
        builder.RegisterInstance(_balloonPrefab).AsSelf();
        
        builder.Register<RecordRepository>(Lifetime.Scoped);
        builder.Register<BalloonSpawner>(Lifetime.Scoped);
        builder.Register<GameplayController>(Lifetime.Scoped);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<GameplayController>();
        });
    }

    private void RegisterUIComponents(IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameScreen).AsSelf();
        builder.RegisterInstance(_recordBoard).AsSelf();
        builder.RegisterInstance(_lifeView).AsSelf();
        builder.RegisterInstance(_scoreView).AsSelf();
        builder.RegisterInstance(_playButton).AsSelf();
    }
}
}