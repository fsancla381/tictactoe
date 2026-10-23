using System;
using System.Collections.Generic;
using UnityEngine;

public enum CellState { Empty, X, O }

public class GameManager : MonoBehaviour
{
    // Tablero de 3x3 celdas
    public CellState[,] Board { get; private set; } = new CellState[3, 3];
    public CellState CurrentTurn { get; private set; } = CellState.X;
    public bool IsGameOver { get; private set; }
    public CellState Winner { get; private set; } = CellState.Empty;
    
    // Contenedor de celdas
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform cellsContainer;
    
    // Singleton
    public static GameManager Instance  { get; private set; }

    private void Awake()
    {
        // verifica si existe alguna instancia del GameManager previa
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // si ya existe una instancia previa, eliminar este objeto (suicidio)
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // genera el tablero vacio
        if (cellPrefab &&  cellsContainer)
        {
            for (int i = 0; i < 9; i++)
            {
                // crea una instancia de la celda
                GameObject cell = Instantiate(cellPrefab, cellsContainer);
                // obtiene el componente BoardCell del GameObject
                BoardCell boardCell = cell.GetComponent<BoardCell>();
                // asigna la fila y columna a la celda
                boardCell.Row = i / 3;
                boardCell.Col = i % 3;
            }
        }
    }

    // Intenta realizar una jugada en la posición (row, col)
    public bool PlayMove(int row, int col)
    {
        // comprobar si el juego terminó o la casilla ya está ocupada
        if (IsGameOver || Board[row, col] != CellState.Empty)
            return false;

        // Registrar la jugada
        Board[row, col] = CurrentTurn;
        
        // Comprobar si hay algún ganador
        if (CheckWin(row, col))
        {
            IsGameOver = true;
            Winner = CurrentTurn;
        }
        // Comprobar si el tablero está completo
        else if (IsBoardFull())
        {
            IsGameOver = true;
            Winner = CellState.Empty;
        }
        else
        {
            // Cambiar el turno
            CurrentTurn = (CurrentTurn == CellState.X) ? CellState.O : CellState.X;
        }

        return true;
    }

    public void StartNewGame()
    {
        Board = new CellState[3, 3];
        CurrentTurn = CellState.X;
        IsGameOver = false;
        Winner = CellState.Empty;
    }

    private bool IsBoardFull()
    {
        // Recorrer el tablero en busca de una casilla vacía (CellState.Empty)
        foreach (CellState cell in Board)
        {
            if (cell == CellState.Empty)
                return false;
        }
        return true;
    }

    public bool CheckWin(int row, int col)
    {
        // Comprobar la fila en la que estoy
        if (Board[row, 0] == CurrentTurn && Board[row, 1] == CurrentTurn && Board[row, 2] == CurrentTurn)
            return true;
        
        // Comprobar la columna en la que estoy
        if (Board[0, col] == CurrentTurn && Board[1, col] == CurrentTurn && Board[2, col] == CurrentTurn)
            return true;
        
        // Comprobar la diagonal principal
        if (Board[0, 0] == CurrentTurn && Board[1, 1] == CurrentTurn && Board[2, 2] == CurrentTurn)
            return true;
        
        // Comprobar la diagonal secundaria
        if (Board[0, 2] == CurrentTurn && Board[1, 1] == CurrentTurn && Board[2, 0] == CurrentTurn)
            return true;
        
        return false;
    }
}
