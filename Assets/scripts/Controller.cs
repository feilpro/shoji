using NUnit.Framework;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
public class Controller
{
    View view;
    Board board;


    const int ROWS = 9;
    const int COLS = 9;

    Team currentTurn = Team.White;
    Piece selectedPiece = null;

    Player whitePlayer;
    Player blackPlayer;

    List<int2> validMoves = new List<int2>();

    public Controller(View view)
    {
        this.view = view;
        board = new Board(ROWS,COLS);
        view.CreateGrid(ref board, ROWS, COLS);
        whitePlayer = new Player(Team.White);
        blackPlayer = new Player(Team.Black);
        SetBoard();
        view.EnableTeamCementary(currentTurn);
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
            if (selectedSquare.piece == null)
            {//mover
                if (!IsValidMove(selectedSquare.coor) && selectedPiece.coor.x >= 0) return;
                if (selectedPiece.coor.x < 0) UpdateCementaryCount(selectedPiece.type);
                MoveSelectPiece(selectedSquare); //No hay pieza seleccionada 
                SwitchTeam();
            }
            else if (selectedSquare.piece.team == currentTurn)
            {
                if (selectedPiece.coor.x < 0) EatPiece(ref selectedPiece);
                SelectNewPiece(selectedSquare.piece);
            }
            else if (selectedPiece.coor.x >= 0) //comer
            {
                if (!IsValidMove(selectedSquare.coor)) return;
                EatPiece(ref selectedSquare.piece);
                MoveSelectPiece(selectedSquare);
                SwitchTeam();
            }

        }
        else
        {
            if (selectedSquare.piece == null) return;
            if (selectedSquare.piece.team != currentTurn) return;
            SelectNewPiece(selectedSquare.piece);
            
        }
    }

    public void SelectedCementarySquare(PieceType pieceType) 
    {
        Player currentPlayer = currentTurn == Team.White ? whitePlayer : blackPlayer;
        selectedPiece = pieceType switch
        {
            PieceType.Pawn => currentPlayer.sideBoard.pawns.Dequeue(),
            PieceType.Spear => currentPlayer.sideBoard.spears.Dequeue(),
            PieceType.Horse => currentPlayer.sideBoard.horses.Dequeue(),
            PieceType.Silver => currentPlayer.sideBoard.silver.Dequeue(),
            PieceType.Golden => currentPlayer.sideBoard.golden.Dequeue(),
            PieceType.Tower => currentPlayer.sideBoard.tower.Dequeue(),
            PieceType.Bishop => currentPlayer.sideBoard.bishops.Dequeue(),
            _ => null
        };
    }

    void UpdateCementaryCount(PieceType pieceType)
    {
        Player currentPlayer = currentTurn == Team.White ? whitePlayer : blackPlayer;
        switch (pieceType) 
        {
            case PieceType.Pawn:
                view.UpdateCementary(currentTurn,pieceType, currentPlayer.sideBoard.pawns.Count);
                break;
            case PieceType.Spear:
                view.UpdateCementary(currentTurn, pieceType, currentPlayer.sideBoard.spears.Count);
                break;
            case PieceType.Horse:
                view.UpdateCementary(currentTurn, pieceType, currentPlayer.sideBoard.horses.Count);
                break;
            case PieceType.Silver:
                view.UpdateCementary(currentTurn, pieceType, currentPlayer.sideBoard.silver.Count);
                break;
            case PieceType.Golden:
                view.UpdateCementary(currentTurn, pieceType, currentPlayer.sideBoard.golden.Count);
                break;
            case PieceType.Tower:
                view.UpdateCementary(currentTurn, pieceType, currentPlayer.sideBoard.tower.Count);
                break;
            case PieceType.Bishop:
                view.UpdateCementary(currentTurn, pieceType, currentPlayer.sideBoard.bishops.Count);
                break;
        }
    }

    void EatPiece(ref Piece eatenPiece)
    {
        eatenPiece.coor = new int2(-1, -1);
        eatenPiece.team = currentTurn;
        Player currentPlayer = currentTurn == Team.White ? whitePlayer : blackPlayer;
        switch (eatenPiece.type)
        {
            case PieceType.Pawn:
                currentPlayer.sideBoard.pawns.Enqueue((Pawn)eatenPiece);
                Debug.Log($"{currentPlayer.sideBoard.pawns.Count}, { eatenPiece.team}");
                view.UpdateCementary(currentTurn, eatenPiece.type, currentPlayer.sideBoard.pawns.Count);
                break;
            case PieceType.Spear:
                currentPlayer.sideBoard.spears.Enqueue((Spear)eatenPiece);
                view.UpdateCementary(currentTurn, eatenPiece.type, currentPlayer.sideBoard.spears.Count);
                break;
            case PieceType.Horse:
                currentPlayer.sideBoard.horses.Enqueue((Horse)eatenPiece);
                view.UpdateCementary(currentTurn, eatenPiece.type, currentPlayer.sideBoard.horses.Count);
                break;
            case PieceType.Silver:
                currentPlayer.sideBoard.silver.Enqueue((Silver)eatenPiece);
                view.UpdateCementary(currentTurn, eatenPiece.type, currentPlayer.sideBoard.silver.Count);
                break;
            case PieceType.Golden:
                currentPlayer.sideBoard.golden.Enqueue((Golden)eatenPiece);
                view.UpdateCementary(currentTurn, eatenPiece.type, currentPlayer.sideBoard.golden.Count);
                break;
            case PieceType.Tower:
                currentPlayer.sideBoard.tower.Enqueue((Tower)eatenPiece);
                view.UpdateCementary(currentTurn, eatenPiece.type, currentPlayer.sideBoard.tower.Count);
                break;
            case PieceType.Bishop:
                currentPlayer.sideBoard.bishops.Enqueue((Bishop)eatenPiece);
                view.UpdateCementary(currentTurn, eatenPiece.type, currentPlayer.sideBoard.bishops.Count);
                break;

        }
    }

    bool IsValidMove(int2 move)
    {
        foreach (int2 validMove in validMoves)
        {
            if (move.x != validMove.x) continue;
            if (move.y == validMove.y) return true;
        }
        return false;
    }

    void SelectNewPiece(Piece piece)
    {
        selectedPiece = piece;
        validMoves.Clear();
        List<int2> pieceMove = selectedPiece.GetMoves();
        int2 pieceCoor = selectedPiece.coor;

        if (selectedPiece.GetType().IsSubclassOf(typeof(SingleMovePiece))) 
        {
            foreach (int2 move in pieceMove) 
            {
                int2 newCoor;
                newCoor.x = move.x;
                newCoor.y = currentTurn == Team.White ? move.y : -move.y;
                newCoor += pieceCoor;
                if(newCoor.x < 0 || newCoor.x >= ROWS) continue;
                if(newCoor.y < 0 || newCoor.y >= COLS) continue;
                if (board.GetSquare(newCoor.x, newCoor.y).piece != null) 
                {
                    if (board.GetSquare(newCoor.x, newCoor.y).piece.team == currentTurn) continue;
                }
                validMoves.Add(newCoor);
            }   
        }else if (selectedPiece.GetType().IsSubclassOf(typeof(DirectionalMovePiece)))
        {
            foreach (int2 direction in pieceMove)
            {
                for(int i = 1; i <= 8; i++)
                {
                    int2 newCoor = pieceCoor + direction * i;
                    if (newCoor.x < 0 || newCoor.x >= ROWS) break;
                    if (newCoor.y < 0 || newCoor.y >= COLS) break;
                    if (board.GetSquare(newCoor.x, newCoor.y).piece != null)
                    {
                        if (board.GetSquare(newCoor.x, newCoor.y).piece.team == currentTurn) break;
                        validMoves.Add( newCoor);
                        break;
                    }
                    validMoves.Add(newCoor);
                }
            }
        }

    }

    private void MoveSelectPiece(Square selectedSquare)
    {
        if (selectedPiece.coor.x >= 0) RemovePiece(selectedPiece.coor);
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

    void SwitchTeam()
    {
        currentTurn = currentTurn == Team.White ? Team.Black : Team.White;
    }

    ~Controller() 
    {
        
    }
}
