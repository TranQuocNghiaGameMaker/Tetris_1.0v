using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    [SerializeField] Tile tile;
    [SerializeField] Board mainBoard;
    [SerializeField] Piece trackingPiece;

    private Tilemap tilemap;
    public Vector3Int[] Cells { get; private set; }
    public  Vector3Int Position { get; private set; }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        Cells = new Vector3Int[4];
    }
    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }
    private void Clear()
    {
        for (int i = 0; i < Cells.Length; i++)
        {
            Vector3Int tilePosition = Cells[i] + Position;
            tilemap.SetTile(tilePosition, null);
        }
    }
    private void Copy()
    {
        for (int i = 0; i < Cells.Length; i++)
        {
            Cells[i] = trackingPiece.Cells[i];
        }
    }

    private void Drop()
    {
        Vector3Int position = trackingPiece.Position;
        int current = position.y;
        int bottom = -mainBoard.boardSize.y / 2 - 1;

        mainBoard.Clear(trackingPiece);
        for(int row = current; row >= bottom; row--) {
            position.y = row;

            if(mainBoard.IsValidPosition(trackingPiece, position)) { 
                this.Position = position;
            }
            else
            {
                break;
            }
        }
        mainBoard.DrawPiece(trackingPiece);
    }

    private void Set()
    {
        for (int i = 0; i < Cells.Length; i++)
        {
            var tilePosition = Cells[i] + Position;
            tilemap.SetTile(tilePosition, tile);
        }
    }


}
