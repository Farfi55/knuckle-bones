using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace board.graphics
{
    public class GraphicBoardColumn : MonoBehaviour
    {
        [SerializeField] private GraphicDieSlot _slotPrefab;
        [SerializeField] private VerticalLayoutGroup _slotContainer;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
        [SerializeField] private TMP_Text _columnScoreLabel;

        public List<GraphicDieSlot> Slots { get; private set; }
        private int _columnScore;

        private Board _board;
        private int _columnIndex;
        private bool _reverseDieOrder;


        public void Init(Board board, int columnIndex)
        {
            _board = board;
            _columnIndex = columnIndex;

            Slots = new List<GraphicDieSlot>();
            for (int i = 0; i < _board.rows; i++)
            {
                var slot = Instantiate(_slotPrefab, _slotContainer.transform);
                Slots.Add(slot);
            }

            _board.OnBoardChanged += UpdateSlots;
            
            UpdateSlots();
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void InitSlots(IEnumerable<Sprite> diceSides, IEnumerable<Color> colorsPerRepetition)
        {
            foreach (var graphicDieSlot in Slots)
            {
                graphicDieSlot.Init(diceSides, colorsPerRepetition);
            }
        }

        private void UpdateSlots() => UpdateSlots(_board);
        
        private void UpdateSlots(Board board)
        {
            var valueRepetitions = board.GetValueRepetitions(_columnIndex);
            var dice = board.GetColumn(_columnIndex);
            for (int i = 0; i < Slots.Count; i++)
            {
                Slots[i].SetDie(dice[i], valueRepetitions[dice[i]]);
            }

            _columnScore = board.GetColumnScore(_columnIndex);
            _columnScoreLabel.text = _columnScore.ToString();
        }

        public void SetReverseDieOrder(bool reverseDieOrder)
        {
            _reverseDieOrder = reverseDieOrder;
            _verticalLayoutGroup.reverseArrangement = _reverseDieOrder;
            _slotContainer.reverseArrangement = _reverseDieOrder;
        }
    }
}