namespace BalloonCrusher.View
{
using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Balloon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ParticleSystem _burstEffect = default;
    [SerializeField] private Image _balloonImage = default;
    [SerializeField] private List<Color> _colors = new List<Color>();
    
    private float _velocity = default;
    private float _maxHorizontalPosition = Screen.width / 2;
    private float _minVerticalPosition = -Screen.height / 2 - 400;
    private float _maxVerticalPosition = Screen.height / 2;
    
    private TweenerCore<Vector3, Vector3, VectorOptions> _moveAnimation = null;
    
    public event Action onReset = delegate {};
    public event Action onClick = delegate {};
    public event Action onCompletedMove = delegate {};

    public Balloon Instantiate(Transform parent) => Instantiate(this, parent);

    public void Init(float velocity)
    {
        _velocity = velocity;
        transform.localPosition = new Vector3(UnityEngine.Random.Range(-_maxHorizontalPosition,_maxHorizontalPosition), _minVerticalPosition, 0);
        _balloonImage.color = _colors[UnityEngine.Random.Range(0, _colors.Count)];
        _balloonImage.gameObject.SetActive(true);
        gameObject.SetActive(true);
        Move();
    }

    private void Move()
    {
        _moveAnimation = transform.DOLocalMoveY(_maxVerticalPosition,_velocity, true).SetEase(Ease.Linear);
        _moveAnimation.onComplete += Reset;
        _moveAnimation.onComplete += onCompletedMove.Invoke;
    }

    public void Reset()
    {
        if (_moveAnimation != null)
        {
            _moveAnimation.onComplete -= Reset;
            _moveAnimation.Kill();
        }

        onReset();
        gameObject.SetActive(false);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        _balloonImage.gameObject.SetActive(false);
        _burstEffect.Play();
        onClick();
        StartCoroutine(WaitEndOfEffect());
    }

    private IEnumerator WaitEndOfEffect()
    {
        while (_burstEffect.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        
        Reset();
    }
}
}
