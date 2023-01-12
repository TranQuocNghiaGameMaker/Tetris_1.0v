using UnityEngine;

public class Piece
{
    public Board Board { get; private set; }
    public TetrominoData Data { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Position { get; private set; }

    public Piece (Board board,Vector3Int position,TetrominoData data)
    {
        this.Board = board;
        this.Position = position;
        this.Data = data;

        this.Cells ??= new Vector3Int[data.Cells.Length];

        for(int i = 0;i< data.Cells.Length; i++)
        {
            this.Cells[i] = (Vector3Int) data.Cells[i];
        }
    }
}