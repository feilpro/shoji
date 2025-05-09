using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class Controller
{
    View view;
    Board board;


    const int ROWS = 9;
    const int COLS = 9;

    Team currentTurn = Team.White;
    Piece selectedPiece = null;

    public Controller(View view)
    {
        this.view = view;
        board = new Board(ROWS,COLS);
        view.CreateGrid(ref board, ROWS, COLS);
        SetBoard();
    }

    void SetBoard()
    {
        CreateWhitePieces();
        CreateBlackPieces();

        
    }

    void CreatePiece(int2 coor, PieceType type, Team team)
    {
        Piece piece = type switch
        {
            PieceType.Pawn => new Pawn(coor, team),
            PieceType.Spear => new Spear(coor, team),
            PieceType.Horse => new Horse(coor, team),
            PieceType.Silver => new Silver(coor, team),
            PieceType.Golden => new Golden(coor, team),
            PieceType.Tower => new Tower(coor, team),
            PieceType.Bishop => new Bishop(coor, team),
            PieceType.King => new King(coor, team),
            _ => null
        };
        board.GetSquare(coor.x, coor.y).piece = piece;
        view.AddPiece(ref piece, coor);
        
    }

    #region Create Pieces
    void CreateWhitePieces()
    {
        //blancas
        //fila1
        CreatePiece(new int2(0, 6), PieceType.Pawn, Team.White);
        CreatePiece(new int2(1, 6), PieceType.Pawn, Team.White);
        CreatePiece(new int2(2, 6), PieceType.Pawn, Team.White);
        CreatePiece(new int2(3, 6), PieceType.Pawn, Team.White);
        CreatePiece(new int2(4, 6), PieceType.Pawn, Team.White);
        CreatePiece(new int2(5, 6), PieceType.Pawn, Team.White);
        CreatePiece(new int2(6, 6), PieceType.Pawn, Team.White);
        CreatePiece(new int2(7, 6), PieceType.Pawn, Team.White);
        CreatePiece(new int2(8, 6), PieceType.Pawn, Team.White);
        //fila2
        CreatePiece(new int2(1, 7), PieceType.Bishop, Team.White);
        CreatePiece(new int2(7, 7), PieceType.Tower, Team.White);
        //fila3
        CreatePiece(new int2(0, 8), PieceType.Spear, Team.White);
        CreatePiece(new int2(1, 8), PieceType.Horse, Team.White);
        CreatePiece(new int2(2, 8), PieceType.Silver, Team.White);
        CreatePiece(new int2(3, 8), PieceType.Golden, Team.White);
        CreatePiece(new int2(4, 8), PieceType.King, Team.White);
        CreatePiece(new int2(5, 8), PieceType.Golden, Team.White);
        CreatePiece(new int2(6, 8), PieceType.Silver, Team.White);
        CreatePiece(new int2(7, 8), PieceType.Horse, Team.White);
        CreatePiece(new int2(8, 8), PieceType.Spear, Team.White);
    }
    void CreateBlackPieces()
    {
        //fila1
        CreatePiece(new int2(0, 2), PieceType.Pawn, Team.Black);
        CreatePiece(new int2(1, 2), PieceType.Pawn, Team.Black);
        CreatePiece(new int2(2, 2), PieceType.Pawn, Team.Black);
        CreatePiece(new int2(3, 2), PieceType.Pawn, Team.Black);
        CreatePiece(new int2(4, 2), PieceType.Pawn, Team.Black);
        CreatePiece(new int2(5, 2), PieceType.Pawn, Team.Black);
        CreatePiece(new int2(6, 2), PieceType.Pawn, Team.Black);
        CreatePiece(new int2(7, 2), PieceType.Pawn, Team.Black);
        CreatePiece(new int2(8, 2), PieceType.Pawn, Team.Black);
        //fila2
        CreatePiece(new int2(7, 1), PieceType.Bishop, Team.Black);
        CreatePiece(new int2(1, 1), PieceType.Tower, Team.Black);
        //fila3
        CreatePiece(new int2(0, 0), PieceType.Spear, Team.Black);
        CreatePiece(new int2(1, 0), PieceType.Horse, Team.Black);
        CreatePiece(new int2(2, 0), PieceType.Silver, Team.Black);
        CreatePiece(new int2(3, 0), PieceType.Golden, Team.Black);
        CreatePiece(new int2(4, 0), PieceType.King, Team.Black);
        CreatePiece(new int2(5, 0), PieceType.Golden, Team.Black);
        CreatePiece(new int2(6, 0), PieceType.Silver, Team.Black);
        CreatePiece(new int2(7, 0), PieceType.Horse, Team.Black);
        CreatePiece(new int2(8, 0), PieceType.Spear, Team.Black);
    }
    #endregion

    public void SelectSquare(int2 gridPos)
    {
        ref Square selectedSquare = ref board.GetSquare(gridPos.x, gridPos.y);
        if (selectedPiece != null)
        {
            if (selectedSquare.piece == null) MoveSelectPiece(selectedSquare);
            else if (selectedSquare.piece.team == currentTurn) selectedPiece = selectedSquare.piece;
            else
            {
                EatPiece(selectedSquare.coor);
                MoveSelectPiece(selectedSquare);
            }

        }
        else
        {
            if (selectedSquare.piece == null) return;
            if (selectedSquare.piece.team != currentTurn) return;
            selectedPiece = selectedSquare.piece;
        }
    }

    void EatPiece(int2 coor)
    {

    }

    private void MoveSelectPiece(Square selectedSquare)
    {
        RemovePiece(selectedPiece.coor);
        AddPiece(ref selectedPiece, selectedSquare.coor);
        selectedPiece = null;
    }
    void RemovePiece(int2 coor)
    {
        board.GetSquare(coor.x,coor.y).piece = null;
        view.RemovePiece(coor);
    }

    void AddPiece(ref Piece piece,int2 coor)
    {
        board.GetSquare(coor.x,coor.y).piece = piece;
        piece.coor = coor;
        view.AddPiece(ref piece, coor);
    }

    ~Controller() 
    {
        
    }
}
