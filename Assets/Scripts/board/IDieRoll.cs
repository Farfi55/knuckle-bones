using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace board
{
    public interface IDieRoll
    {
        public Action OnDieRollStart { get; set; }
        public Action<Die> OnDieRollEnd { get; set; }
        public Action<Die> OnDieRollUpdate { get; set; }

        public Die Die { get; set; }

        public void StartRolling();
        public void Clear();
    }
}