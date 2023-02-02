using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap Tilemap { get; private set; }
    public Piece ActivePiece { get; private set; }
    //private PieceManager _piece;

    [SerializeField] TetrominoData[] tetrominoes;
    public Vector2Int boardSize = new(10, 20);
    [SerializeField] Vector3Int _spawnPosition = new(-1, 8, 0);

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    private void Awake()
    {
        Tilemap = GetComponentInChildren<Tilemap>();
        ActivePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < tetrominoes.Length; i++)
        {
            tetrominoes[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = tetrominoes[random];

        ActivePiece.Initialize(this, _spawnPosition, data);

        if (IsValidPosition(ActivePiece, _spawnPosition))
        {
            DrawPiece(ActivePiece);
        }
        else
        {
            GameOver();
        }
    }

    private void Update()
    {
        //Clear(_piece);
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    _piece.Move(Vector2Int.down);
        //}
        //// Handle hard drop
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    _piece.HardDrop();
        //}
        //// Left/right movement
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    _piece.Move(Vector2Int.left);
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    _piece.Move(Vector2Int.right);
        //}
        //DrawPiece(_piece);
    }

    //public void SpawnPiece()
    //{
    //    _piece = new(this, _spawnPosition, tetrominoes[1]);
    //    if (IsValidPosition(_piece, _spawnPosition))
    //    {
    //        DrawPiece(_piece);
    //    }
    //    else
    //    {
    //        GameOver();
    //    }
    //}

    public void GameOver()
    {
        Tilemap.ClearAllTiles();

        // Do anything else you want on game over here..
    }

    //Draw piece cell on the tile map
    public void DrawPiece(Piece piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position;
            Tilemap.SetTile(tilePosition, piece.Datas.tile);
        }
    }

    //Clear piece celltile on the tile map
    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position;
            Tilemap.SetTile(tilePosition, null);
        }
    }

   

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + position;

            // An out of bounds tile is invalid
            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            // A tile already occupies the position, thus invalid
            if (Tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }



    public bool IsValidPosition(PieceManager piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + position;

            // An out of bounds tile is invalid
            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            // A tile already occupies the position, thus invalid
            if (Tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public void DrawPiece(PieceManager piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position;
            Tilemap.SetTile(tilePosition, piece.Data.tile);
        }
    }
    private void Clear(PieceManager piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position;
            Tilemap.SetTile(tilePosition, null);
        }
    }

    public void ClearLine()
    {
        var bound = Bounds;
        int row = bound.yMin;
        while(row < bound.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
            }
            else
            {
                row++;
            }
        }

    }

    private void LineClear(int row)
    {
        var bound = Bounds;

        //Clear all tile in the row
        for(int i = bound.xMin;i < bound.xMax; i++)
        {
            var position = new Vector3Int(i, row);
            Tilemap.SetTile(position, null);
        }

        //Shift every row above down one
        while (row < bound.yMax) 
        {
            for (int i = bound.xMin; i < bound.xMax; i++)
            {
                Vector3Int position = new(i, row + 1);
                var above = Tilemap.GetTile(position);

                position = new(i, row);
                Tilemap.SetTile(position, above);
            }
            row++;
        }
    }

    private bool IsLineFull(int row)
    {
        var bound = Bounds;
        for (int col = bound.xMin; col < bound.xMax; col++)
        {
            var position = new Vector3Int(col, row);
            if (!Tilemap.HasTile(position))
            {
                return false;
            }
        }
        return true;
    }
}