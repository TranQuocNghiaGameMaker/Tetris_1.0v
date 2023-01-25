using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I,O,T,J,L,S,Z
}

[System.Serializable]
public struct TetrominoData
{
    public Tetromino Tetromino;
    public Tile tile;
    public Vector2Int[] Cells;
    public Vector2Int[,] WallKicks;
    
    public void Initialize()
    {
        Cells = Data.Cells[this.Tetromino];
        WallKicks = Data.WallKicks[this.Tetromino];
    }
}