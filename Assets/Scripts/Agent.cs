using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public interface IAgent
{
    int RollDice();
    void PlaceDie(int dieValue, FullBoard fullBoard, int side);
}