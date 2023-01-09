using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I,O,T,J,L,S,Z
}

[Serializable]
public struct TetrominoData
{
    public Tetromino Tetromino;
    public Tile tile;
    public Vector2Int[] cells;
}