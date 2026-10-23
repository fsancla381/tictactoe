using TMPro;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    [SerializeField] private int row;
    [SerializeField] private int col;

    [SerializeField] private TextMeshProUGUI cellText;
    
    [SerializeField] private UIManager uiManager;
    
    // respuesta al onClick
    public void OnClick()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        } 
        
        // Intentar hacer la jugada
        if (GameManager.Instance.PlayMove(row, col))
        {
            uiManager.UpdateUI();
        }
    }

    public void Refresh()
    {
        CellState state = GameManager.Instance.Board[row, col];
        cellText.text = (state == CellState.Empty) ? "" : state.ToString();
    }
}
