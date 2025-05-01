using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class View : MonoBehaviour
{
    [Header("prefabs")]
    [SerializeField] GameObject boxPrefab;

    [Header("view Objects")]
    [SerializeField] Transform gridParent;

    Controller controller;

    SquareView[,] gridView;

    // Start is called once00 before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controller = new Controller(this);
    }

    // Update is called once per frame
    public void CreateGrid(ref Board board, int rows, int cols)
    {
        gridView = new SquareView[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                gridView[i,j] = Instantiate(boxPrefab, gridParent).GetComponent<SquareView>();
                int2 coor = board.GetSquare(i, j).GetCoor;
                gridView[i, j].SetSquare(coor.x, coor.y);
            }
        }
    }

    public void AddPiece(ref Piece piece, int2 coor)
    {
        gridView[coor.x, coor.y].AddPiece(ref piece);
    }

    public void RemovePiece(int2 coor)
    {
        gridView[coor.x, coor.y].RemovePiece();
    }
}
