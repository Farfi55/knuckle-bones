using UnityEngine;
using Random = UnityEngine.Random;

namespace agents
{
    public class Player : Agent
    {
        public int rolledDie = 0;

        public override int RollDice()
        {
            if (rolledDie != 0) return rolledDie;
            var dice = Random.Range(1, 7);
            Debug.Log($"player {side} rolled {dice}");
            return dice;
        }



        private void Update()
        {
            if (!IsMyTurn())
                return;

            if (!Input.anyKeyDown)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rolledDie = RollDice();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlaceDie(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlaceDie(1);            
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlaceDie(2);
            }
        }

        private void PlaceDie(int col)
        {
            if (board.IsColFull(col))
            {
                Debug.LogWarning($"player {side}, Column {col} is full");
                return;
            }
            if (rolledDie == 0)
            {
                Debug.LogWarning($"player {side}, You have not rolled the die");
                return;
            }

            board.PlaceDie(rolledDie, col);
            Debug.Log($"player {side} placed {rolledDie} in column {col}");
            rolledDie = 0;
        }
    }
}