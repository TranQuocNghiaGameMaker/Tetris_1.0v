using System.Collections;
using UnityEngine;


public class PieceManager
{
    public Board board { get; private set; }
    public TetrominoData Data { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Position { get; private set; }
    public PieceManager(Board board, Vector3Int position, TetrominoData data)
    {
        this.Data = data;
        this.board = board;
        this.Position = position;

        if (Cells == null)
        {
            Cells = new Vector3Int[data.Cells.Length];
        }

        for (int i = 0; i < Cells.Length; i++)
        {
            Cells[i] = (Vector3Int)data.Cells[i];
        }
    }

    public void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
    }

    public bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = Position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = board.IsValidPosition(this, newPosition);

        // Only save the movement if the new position is valid
        if (valid)
        {
            Position = newPosition;
        }

        return valid;
    }
}
