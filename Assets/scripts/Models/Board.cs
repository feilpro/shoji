
using System.Numerics;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public struct Board
{
    Square[,] grid;


    public Board(int rows, int cols)
    {
        grid = new Square[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = new Square(i,j);
            }
        }
       
    }

    public ref Square GetSquare(int row, int cols) => ref grid[row, cols];
}

public class Square
{
    public int2 coor;
    public Piece piece;
    public Square(int x, int y ) {
        coor = new int2(x, y);
        piece = null;
    }

    ~Square() { }

    public int2 GetCoor => coor;
}
