namespace BalloonCrusher.View
{
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeView : MonoBehaviour
{
    [SerializeField] private List<Image> _life = new List<Image>();
    [SerializeField] private Color _activeColor = Color.white;
    [SerializeField] private Color _inactiveColor = Color.black;

    public void Reset() => _life.ForEach(image => image.color = _activeColor);

    public void InactivateLife()
    {
        Image life = _life.Find(life => life.color == _activeColor);

        if (life != null)
        {
            life.color = _inactiveColor;
        }
    }
}
}
