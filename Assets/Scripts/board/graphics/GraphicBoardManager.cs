using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicBoardManager : MonoBehaviour
{
    private GameManager gm;
    
    [SerializeField] private Transform graphicBoardRoot;
    
    [SerializeField] public GraphicBoard graphicBoardPrefab;

    [SerializeField] private float distanceBetweenBoards = 5f;
    private GraphicBoard[] graphicBoards;
    
    
    private void Start()
    {
        gm = GameManager.Instance;
        gm.OnSideChanged += OnSideChanged;
        
        graphicBoards = new GraphicBoard[gm.nPlayers];
        for (int side = 0; side < gm.nPlayers; side++)
        {
            GenerateBoard(side);
        }
    }

    private void GenerateBoard(int side)
    {
        GraphicBoard graphicBoard = Instantiate(graphicBoardPrefab, graphicBoardRoot);
        graphicBoard.name = "Board " + side;
        graphicBoard.transform.localPosition = new Vector3(0, side * 10f, 0);
        graphicBoard.Init(gm.fullBoard.GetBoard(side)); 
        graphicBoards[side] = graphicBoard;
    }

    private void OnSideChanged(int side)
    {
        
    }
}