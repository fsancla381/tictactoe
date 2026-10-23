using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BoardCell : MonoBehaviour
{
    [Header("Posición lógica en el tablero")]
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

    [Header("Representación gráfica")]
    [SerializeField] private Sprite cellSpriteX;
    [SerializeField] private Sprite cellSpriteO;

    private Image cellImage;
    private Sprite cellDefaultSprite;

    private void Awake()
    {
        // recupera la imagen por defecto de la celda
        cellImage = GetComponent<Image>();
        cellDefaultSprite = cellImage.sprite;
    }

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
        if (state == CellState.X)
        {
            cellImage.sprite = cellSpriteX;
        }
        else if (state == CellState.O)
        {
            cellImage.sprite = cellSpriteO;
        }
        else
        {
            cellImage.sprite = cellDefaultSprite;
        }
    }
}
