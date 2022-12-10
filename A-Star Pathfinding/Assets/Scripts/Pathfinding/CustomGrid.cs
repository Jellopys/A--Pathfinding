using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomGrid : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile tileBlue;
    [SerializeField] private Tile tileBlack;
    [SerializeField] private Tile tileYellow;
    [SerializeField] private Tile tileRed;

    private Queue<Tile> frontier = new Queue<Tile>();
    private List<Tile> tiles = new List<Tile>();

    private HashSet<Tile> checkedTiles = new HashSet<Tile>();
    // private Dictionary<Tile, Vector2Int> tileDict = new Dictionary<Tile, Vector2Int>();

    private Vector3Int location;

    void Start()
    {
        
    }

    void Update()
    {
        // if (Input.GetKey(KeyCode.Mouse0)) { PlaceWalkableTile(); } // BLUE TILE
        if (Input.GetKey(KeyCode.Mouse0)) { SetNeighborTiles(); } // test
        if (Input.GetKey(KeyCode.Mouse1)) { PlaceBlockingTile(); } // BLACK TILE
        if (Input.GetKey(KeyCode.E))      { PlacePlayerTile(); } // YELLOW TILE
        // if (Input.GetKey(KeyCode.G))      { StartPathFind(); } // Start Pathfinding algorithm
    }

    void SetNeighborTiles()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0f;
        location = tilemap.WorldToCell(mp);

        tilemap.SetTile(location, tileBlue);

        Tile currentTile = tilemap.GetTile<Tile>(location);

        int x = location.x;
        int y = location.y;
        Vector3Int up = new Vector3Int( x , y + 1 , 0 );
        Vector3Int down = new Vector3Int( x , y - 1, 0 );
        Vector3Int left = new Vector3Int( x - 1 , y , 0 );
        Vector3Int right = new Vector3Int( x + 1 , y , 0 );

        // Tile upNeighbor = tilemap.GetTile<Tile>(up);
        // Tile downNeighbor = tilemap.GetTile<Tile>(down);
        // Tile leftNeighbor = tilemap.GetTile<Tile>(left);
        // Tile rightNeighbor = tilemap.GetTile<Tile>(right);

        tilemap.SetTile(up, tileBlack);
        tilemap.SetTile(down, tileBlack);
        tilemap.SetTile(left, tileBlack);
        tilemap.SetTile(right, tileBlack);
    }

    void GetTileNeighbors()
    {

    }

    void PlaceWalkableTile() // BLUE TILE, WALKABLE
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0f;
        location = tilemap.WorldToCell(mp);
        tilemap.SetTile(location, tileBlue);

        if (!tiles.Contains(tilemap.GetTile<Tile>(location))) {
            tiles.Add(tilemap.GetTile<Tile>(location));
        }
    }

    void PlacePlayerTile() // YELLOW TILE, PLAYER
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0f;
        location = tilemap.WorldToCell(mp);
        tilemap.SetTile(location, tileYellow);

        if (!frontier.Contains(tilemap.GetTile<Tile>(location))) {
            frontier.Enqueue(tilemap.GetTile<Tile>(location));
            StartPathFind(location);
        }
    }

    void PlaceBlockingTile() // BLACK TILE, BLOCKING
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0f;
        location = tilemap.WorldToCell(mp);
        tilemap.SetTile(location, tileBlack);
    }

    void StartPathFind(Vector3Int startPos)
    {
        Vector3Int loc = startPos;
        while (frontier.Count > 0) {

            Vector3Int up = new Vector3Int( startPos.x , startPos.y + 1 , 0 );
            Vector3Int down = new Vector3Int( startPos.x , startPos.y - 1, 0 );
            Vector3Int left = new Vector3Int( startPos.x - 1 , startPos.y , 0 );
            Vector3Int right = new Vector3Int( startPos.x + 1 , startPos.y , 0 );

            List<Tile> neighbors = new List<Tile>();
            Dictionary<Tile, Vector3Int> n = new Dictionary<Tile, Vector3Int>();

            neighbors.Add(tilemap.GetTile<Tile>(up));
            neighbors.Add(tilemap.GetTile<Tile>(down));
            neighbors.Add(tilemap.GetTile<Tile>(left));
            neighbors.Add(tilemap.GetTile<Tile>(right));

            foreach (Tile t in neighbors)
            {
                CheckNeighbor(t);
            }
            neighbors.Clear();
            frontier.Dequeue();

            Tile upNeighbor = tilemap.GetTile<Tile>(up);
            Tile downNeighbor = tilemap.GetTile<Tile>(down);
            Tile leftNeighbor = tilemap.GetTile<Tile>(left);
            Tile rightNeighbor = tilemap.GetTile<Tile>(right);
            
        }
    }

    void CheckNeighbor(Tile neighbor)
    {
        if (neighbor != null) {
            checkedTiles.Add(neighbor);
            return;
        }
        else {
            frontier.Enqueue(neighbor);
            neighbor.color = Color.red;
            Debug.Log("Checking tile");
        }
    }
}