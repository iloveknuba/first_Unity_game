using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance;
    public Square grassPrefab;
    public Square mountainPrefab;
    public int numRows = 9;
    public int numCols = 16;

    public new Transform camera;

    private Square squarePrefab;
    private Square[,] squares;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        squares = new Square[numRows, numCols];

        CreateGrid();


        camera.transform.position = new Vector3((float)numRows / 1.125f, (float)numCols / 2.7f - 2.3f, -10);
    }
    void CreateGrid()
    {
        // Create the grid of squares
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                var isOffset = (row % 2 == 0 && col % 2 == 0 && col != 0 && col != 16);
                Vector3 squarePosition = new(col, row, 0f);
                squarePrefab = isOffset ? mountainPrefab : grassPrefab;
                squares[row, col] = Instantiate(squarePrefab, squarePosition, Quaternion.identity);
                squares[row, col].name = $"Square: {col} {row}";
            }
        }
    }
}
