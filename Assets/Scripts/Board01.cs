//using UnityEngine;
//using UnityEngine.Tilemaps;

//public class Board01 : MonoBehaviour
//{
//    public Tilemap tilemap { get; private set; }
//    public Piece activePiece { get; private set; }

//    public TetrominoData[] tetrominoes;
//    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);

//    private void Awake()
//    {
//        tilemap = GetComponentInChildren<Tilemap>();
//        for (int i = 0; i < tetrominoes.Length; i++)
//        {
//            tetrominoes[i].Initialize();
//        }
//    }

//    private void Start()
//    {
//        SpawnPiece();
//    }

//    public void SpawnPiece()
//    {
//        int random = Random.Range(0, tetrominoes.Length);
//        TetrominoData data = tetrominoes[random];
//        activePiece = new(this, spawnPosition, data);
//        Set(activePiece);
//    }
//    public void Set(Piece piece)
//    {
//        for (int i = 0; i < piece.Cells.Length; i++)
//        {
//            Vector3Int tilePosition = piece.Cells[i] + piece.Position;
//            tilemap.SetTile(tilePosition, piece.Data.tile);
//        }
//    }
//}
