using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private BoardCell[] boardsCell;

    [SerializeField] private TextMeshProUGUI statusText;
    
    
    void Start()
    {
        Reset();
    }
    
    // Gestión del click sobre el botón de RESET
    public void OnClickReset()
    {
        Reset();
    }

    private void Reset()
    {
        // Limpia el Tablero (modelo)
        GameManager.Instance.StartNewGame();
        // Actualiza la interface de usuario
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Actualiza el símbolo de cada celda en pantalla
        foreach (BoardCell boardCell in boardsCell)
        {
            boardCell.Refresh();
        }
        
        // Actualiza el texto de estado
        if (GameManager.Instance.IsGameOver)
        {
            // poner ganador o empate
            if (GameManager.Instance.Winner == CellState.Empty)
            {
                statusText.text = "¡ Empate !";
            }
            else
            {
                statusText.text = $"¡ Ganador: {GameManager.Instance.Winner} !";
            }
        }
        else
        {
            // informar del turno actual
            statusText.text = $"Turno actual: {GameManager.Instance.CurrentTurn}";
        }
    }
}
