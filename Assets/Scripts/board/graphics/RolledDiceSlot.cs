using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace board.graphics
{
    public class RolledDiceSlot : MonoBehaviour
    {
        [SerializeField] private GraphicDieSlot _slot;

        public Action OnDieRollStart;
        // invoked when die animation is finished
        public Action<Die> OnDieRollEnd;

        [SerializeField, Range(-1f, 1f)] private float _startDieStopChance = -0.07f;
        [SerializeField, Range(0f, 1f)] private float _dieStopChanceIncrease = 0.07f;
        [SerializeField] private float _timeBetweenDieFaces = .2f;

        private Coroutine _rollCoroutine;

        public Die Die { get; private set; }

        private IEnumerator Roll()
        {
            OnDieRollStart?.Invoke();
            
            var currDie = new Die(RandomDieValue());
            _slot.SetDie(currDie);

            float dieStopChance = _startDieStopChance;
            while (dieStopChance > Random.value)
            {
                yield return new WaitForSeconds(_timeBetweenDieFaces);
                dieStopChance += _dieStopChanceIncrease;
                currDie = RandomDieBut(currDie.Value);
                _slot.SetDie(currDie);
            }

            Die = currDie;
            OnDieRollEnd?.Invoke(currDie);
        }
        
        public void StartRolling()
        {
            if (Die == null && _rollCoroutine == null)
            {
                _rollCoroutine = StartCoroutine(Roll());
            }
        }
        
        public void Clear()
        {
            Die = null;
            _slot.SetDie(null);
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