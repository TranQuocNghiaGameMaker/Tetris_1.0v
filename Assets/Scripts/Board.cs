using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }

    [SerializeField] TetrominoData[] tetrominoes;
    [SerializeField] TetrominoesSO tetrominoesSO;
    [SerializeField] Vector3Int spawnPosition;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        for (int i = 0; i < tetrominoes.Length; i++)
        {
            tetrominoes[i].Initialize();
        }
    }
    private void Start()
    {
        //SpawnPiece();
        SpawnPieceWithSO();
    }

    //private void SpawnPiece()
    //{
    //    int random = Random.Range(0, tetrominoes.Length);
    //    var tetromino = tetrominoes[random];
    //    activePiece = new(this, spawnPosition, tetromino);
    //    Set(activePiece);
    //}
    private void SpawnPieceWithSO()
    {
        int random = Random.Range(0, tetrominoes.Length);
        var tetromino = tetrominoesSO.list[random];
        activePiece = new(this, spawnPosition, tetromino);
        Set(activePiece);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position;
            this.tilemap.SetTile(tilePosition, piece.Data.tile);
        }
    }
}
