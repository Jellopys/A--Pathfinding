using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class NodeGenerator : MonoBehaviour
{
    private Pathfinding pathfinding;
    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] private NPC_Movement npcMovement;

    private void Start() {
        pathfinding = new Pathfinding(20, 10);
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetGrid().GetXY(npcMovement.GetCurrentPosition(), out int x1, out int y1);
            
            List<PathNode> path = pathfinding.FindPath(x1, y1, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                    npcMovement.SetTargetPosition(mouseWorldPosition);
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x,y).SetIsWalkable(!pathfinding.GetNode(x,y).isWalkable);
        }
    }
}
