using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
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
    }

    private void Update()
    {
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
