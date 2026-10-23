using System;
using TMPro;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    [SerializeField] private int row;
    [SerializeField] private int col;
    // getter y setter para los campos serializados
    public int Row
    {
        get => row;
        set => row = value;
    }
    public int Col
    {
        get => col;
        set => col = value;
    }

    [SerializeField] private TextMeshProUGUI cellText;
    
    
    // respuesta al onClick
    public void OnClick()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        } 
        
        // Intentar hacer la jugada
        GameManager.Instance.PlayMove(row, col);
    }

    private void Update()
    {
        CellState state = GameManager.Instance.Board[row, col];
        cellText.text = (state == CellState.Empty) ? "" : state.ToString();
    }
}
