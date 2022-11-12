using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GraphicDieSlot : MonoBehaviour
{
    private Image _image;
    private Die _die = null;
    private int _valueRepetitions;
    [SerializeField] private Sprite[] _diceSides;
    [SerializeField] private List<Color> _colorsPerRepetition;
    
    
    public void Init(IEnumerable<Sprite> diceSides, IEnumerable<Color> colorsPerRepetition)
    {
        _diceSides = diceSides.ToArray();
        _colorsPerRepetition = new List<Color>(colorsPerRepetition);
    }
    
    
    public void SetDie(Die die)
    {
        this._die = die;
        UpdateGraphic();
    }
    
    public void SetDie(Die die, int valueRepetitions)
    {
        this._die = die;
        this._valueRepetitions = valueRepetitions;
        UpdateGraphic();
    }

    public void SetDieRepetitions(int valueRepetitions)
    {
        _valueRepetitions = valueRepetitions;
        UpdateGraphic();
    }
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    
    private Sprite GetSpriteForDie()
    {
        if (_die == null)
            return null;

        return _diceSides[_die.Value - 1];
    }
    
    
    private void UpdateGraphic()
    {
        _image.sprite = GetSpriteForDie();
        _image.enabled = _die != null;
        _image.color = GetColorForDie();
    }

    private Color GetColorForDie()
    {
        if(_valueRepetitions > _colorsPerRepetition.Length)
            return Color.white;
        return _colorsPerRepetition[_valueRepetitions - 1];
        
    }
}
