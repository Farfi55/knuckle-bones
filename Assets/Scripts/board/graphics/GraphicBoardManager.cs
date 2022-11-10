using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicBoardManager : MonoBehaviour
{
    private GameManager gm;
    
    [SerializeField] private Transform graphicBoardRoot;
    
    [SerializeField] private GraphicBoard graphicBoardPrefab;
    
    private List<GraphicBoard> graphicBoards;
    
    
    private void Start()
    {
        gm = GameManager.Instance;
        gm.OnSideChanged += OnSideChanged;
        for (int side = 0; side < gm.nPlayers; side++)
        {
            GenerateBoard(side);
        }
    }

    private void GenerateBoard(int side)
    {
        GraphicBoard graphicBoard = Instantiate(graphicBoardPrefab, graphicBoardRoot);
        graphicBoard.board = gm.fullBoard.GetBoard(side);
        graphicBoard.name = "Board " + side;
        graphicBoard.transform.localPosition = new Vector3(0, side * 10f, 0);
        
        graphicBoards.Add(graphicBoard);
    }

    private void OnSideChanged(int side)
    {
        
    }
}