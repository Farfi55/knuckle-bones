using UnityEngine;
using Random = UnityEngine.Random;

namespace agents
{
    public class HumanPlayer : Player
    {

        
        private void Update()
        {
            if (!IsItMyTurn())
                return;

            if (!Input.anyKeyDown)
                return;
            
            if(Input.GetKeyDown(KeyCode.Space))
                Board.GetDieRoll()?.StartRolling();

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
            if (Board.IsColFull(col))
                return;
            
            Board.PlaceRolledDie(col);
        }
    }
}