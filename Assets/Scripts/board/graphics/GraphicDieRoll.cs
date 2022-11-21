using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace board.graphics
{
    public class GraphicDieRoll : MonoBehaviour, IDieRoll
    {
        [SerializeField] private GraphicDieSlot _graphicDieSlot;
        
        [SerializeField, Range(-1f, 1f)] private float _startDieStopChance = -0.07f;
        [SerializeField, Range(0f, 1f)] private float _dieStopChanceIncrease = 0.07f;
        [SerializeField] private float _timeBetweenDieFaces = .2f;
        
        private Coroutine _rollCoroutine;
        public Die Die { get; set; }

        public Action OnDieRollStart { get; set; }
        public Action<Die> OnDieRollEnd { get; set; }
        public Action<Die> OnDieRollUpdate { get; set; }

        private void Awake()
        {
            _graphicDieSlot.SetDie(null);
        }

        private IEnumerator RollAnimation()
        {
            OnDieRollStart?.Invoke();
            
            var currDie = new Die(RandomDieValue());
            OnDieRollUpdate?.Invoke(currDie);

            float dieStopChance = _startDieStopChance;
            while (dieStopChance > Random.value)
            {
                yield return new WaitForSeconds(_timeBetweenDieFaces);
                dieStopChance += _dieStopChanceIncrease;
                currDie = RandomDieBut(currDie.Value);
                OnDieRollUpdate?.Invoke(currDie);
            }

            Die = currDie;
            OnDieRollEnd?.Invoke(currDie);
        }
        
        public void StartRolling()
        {
            if (Die == null && _rollCoroutine == null)
            {
                _rollCoroutine = StartCoroutine(RollAnimation());
            }
        }
        
        public void Clear()
        {
            Die = null;
            _graphicDieSlot.SetDie(null);
        }

        private int RandomDieValue() => Random.Range(1, 7);

        private Die RandomDieBut(int value)
        {
            int randomDieValue = RandomDieValue();
            while (randomDieValue == value)
                randomDieValue = RandomDieValue();
            return new Die(randomDieValue);
        }

        
     
    }
}