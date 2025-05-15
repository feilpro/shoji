using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class View : MonoBehaviour
{
    [Header("prefabs")]
    [SerializeField] GameObject boxPrefab;

    [Header("view Objects")]
    [SerializeField] Transform gridParent;
    [SerializeField] CementaryView whiteCementery;
    [SerializeField] CementaryView blackCementery;

    Controller controller;

    SquareView[,] gridView;

    // Start is called once00 before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controller = new Controller(this);
    }

    private void Start()
    {
        whiteCementery.SetCementaryView(this);
        blackCementery.SetCementaryView(this);
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
                gridView[i, j].SetSquare(coor.x, coor.y, this);
            }
        }
    }

    public void EnableTeamCementary(Team team)
    {
        if (team == Team.White)
        {
            whiteCementery.EnableCementaryView();
            blackCementery.EnableCementaryView(false);
        }
        else
        {
            whiteCementery.EnableCementaryView(false);
            blackCementery.EnableCementaryView();
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

    public void SelectSquare(int2 gridPos)
    {
        controller.SelectSquare(gridPos);
    }

    public void SelectCementaryPiece(PieceType pieceType)
    {
        controller.SelectedCementarySquare(pieceType);
    }

    public void UpdateCementary(Team team, PieceType pieceType, int count)
    {
        if(team == Team.White) whiteCementery.UpdateCellView(pieceType, count);
        else blackCementery.UpdateCellView(pieceType, count);
    }
}
