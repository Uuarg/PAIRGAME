using UnityEngine;
using UnityEngine.UI;

public class Resizer : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public RectTransform puzzleField; 
    public int numberOfButtons = 128; 


    public float padding = 10f;
    public Vector2 minButtonSize = new Vector2(100f, 100f);

    void Start()
    {
        ResizeGrid();
    }

    void ResizeGrid()
    {
      
        float puzzleFieldWidth = puzzleField.rect.width;
        float puzzleFieldHeight = puzzleField.rect.height;

     
        int numColumns = Mathf.FloorToInt(puzzleFieldWidth / (minButtonSize.x + padding));
        int numRows = Mathf.CeilToInt((float)numberOfButtons / numColumns);

        float cellWidth = (puzzleFieldWidth - (padding * (numColumns - 1))) / numColumns;
        float cellHeight = (puzzleFieldHeight - (padding * (numRows - 1))) / numRows;

        gridLayoutGroup.cellSize = new Vector2(Mathf.Max(cellWidth, minButtonSize.x), Mathf.Max(cellHeight, minButtonSize.y));
        gridLayoutGroup.spacing = new Vector2(padding, padding);

        puzzleField.sizeDelta = new Vector2(puzzleFieldWidth, numRows * (cellHeight + padding));
    }
}

