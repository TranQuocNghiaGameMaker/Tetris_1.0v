using System;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board Board { get; private set; }
    public TetrominoData Datas { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Position { get; private set; }
    public int RotationIndex { get; private set; }

    [SerializeField] float _stepDelays = 1f;
    [SerializeField] float _lockDelays = 0.5f;
    private float _stepTime;
    private float _lockTime;
    
    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        RotationIndex = 0;
        Datas = data;
        this.Board = board;
        Position = position;

        if (Cells == null)
        {
            Cells = new Vector3Int[data.Cells.Length];
        }

        for (int i = 0; i < Cells.Length; i++)
        {
            Cells[i] = (Vector3Int)data.Cells[i];
        }
    }

    private void Update()
    {
        Board.Clear(this);
        _lockTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }
        // Handle hard drop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }
        // Left/right movement
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        if(Time.time > _stepTime)
        {
            Step();
        }
        Board.DrawPiece(this);
    }

    private void Step()
    {
        _stepTime = Time.time  + _stepDelays;
        Move(Vector2Int.down);

        if(_lockTime > _lockDelays)
        {
            Lock() ;
        }
    }

    private void Lock()
    {
        Board.DrawPiece(this);
        Board.ClearLine();
        Board.SpawnPiece();
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
        Lock();
    }

    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = Position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = Board.IsValidPosition(this, newPosition);

        // Only save the movement if the new position is valid
        if (valid)
        {
            Position = newPosition;
            _lockTime = 0;
        }

        return valid;
    }

    private void Rotate(int direction)
    {
        int originalRotation = RotationIndex;
        this.RotationIndex = Helper.Wrap(this.RotationIndex + direction, 0, 4);
        ApplyRotationMatrix(direction);
        if (!TestWallKick(RotationIndex, direction))
        {
            RotationIndex = originalRotation;
            ApplyRotationMatrix(-direction);
        }
    }

    private void ApplyRotationMatrix(int direction)
    {
        var matrix = Data.RotationMatrix;
        for (int i = 0; i < this.Cells.Length; i++)
        {
            Vector3 cell = this.Cells[i];
            int x, y;
            switch (this.Datas.Tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                    break;
            }
            this.Cells[i] = new(x, y, 0);
        }
    }

    private bool TestWallKick(int rotationIndex,int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);
        for (int i = 0; i < Datas.WallKicks.GetLength(1); i++)
        {
            var translation = Datas.WallKicks[wallKickIndex, i];    
            if (Move(translation))
            {
                return true;
            }
        }
        return false;
    }

    private int GetWallKickIndex(int rotationIndex,int rotationDirection)
    {
        int wallkickIndex = rotationIndex * 2;
        if(rotationDirection < 0)
        {
            wallkickIndex--;
        }
        return Helper.Wrap(wallkickIndex, 0, this.Datas.WallKicks.GetLength(0));
    }
}

