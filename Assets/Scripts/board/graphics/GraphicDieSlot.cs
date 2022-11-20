using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace board.graphics
{
    public class GraphicDieSlot : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private Die _die;
        private int _valueRepetitions = 1;
        [SerializeField] private Sprite[] _diceSides;
        [SerializeField] private List<Color> _colorsPerRepetition;


        private void Awake()
        {
            if (_image == null)
                GetImageComponent();
        }

        public void Init(IEnumerable<Sprite> diceSides, IEnumerable<Color> colorsPerRepetition)
        {
            _diceSides = diceSides.ToArray();
            _colorsPerRepetition = new List<Color>(colorsPerRepetition);
            // UpdateGraphic();
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

        private Sprite GetSpriteForDie()
        {
            if (_die is null || _die.Value == Die.DefaultDieValue)
                return null;

            return _diceSides[_die.Value - 1];
        }


        private void UpdateGraphic()
        {
            var dieSprite = GetSpriteForDie();
            _image.sprite = dieSprite;
            _image.enabled = dieSprite is not null;
            _image.color = GetColorForDie();
        }

        private Color GetColorForDie()
        {
            if (1 < _valueRepetitions && _valueRepetitions <= _colorsPerRepetition.Count)
                return _colorsPerRepetition[_valueRepetitions - 1];
            return Color.white;
        }
        
        private void GetImageComponent()
        {
            _image = GetComponent<Image>();
            if (_image == null)
                Debug.LogError("No image found on " + gameObject.name);
        }
    }
}