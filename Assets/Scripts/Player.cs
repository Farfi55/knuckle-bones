using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, IAgent
{
    public int RollDice()
    {
        var dice = Random.Range(1, 7);
        Debug.Log(dice);
        return dice;
    }

    public void PlaceDie(int dieValue, FullBoard fullBoard, int side)
    {
        
    }


    private void Update()
    {
        if(!Input.anyKeyDown)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            
        }
        else
        {
            
        }
        
    }
}