using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {
    metalOre,
    stoneOre
}

public class ProdecuralGeneration : MonoBehaviour
{
    public GameObject resourcePrefab;

    public Transform[] nodes;

    public ResourceType currentResource;

    void Start()
    {
        SpawnNodes();
    }

    private void SpawnNodes() {
        NodeManager nodeManager = new NodeManager(nodes);

        currentResource = nodeManager.ReturnRandomResourceNode;

        for (int i = 0; i < nodes.Length; i++){
            Vector3 nodePos = nodes[nodeManager.RandomNodeSelection].transform.position;

            if (currentResource == ResourceType.metalOre || currentResource == ResourceType.stoneOre){
                if (!nodeManager.DoesResourceExist(nodePos)){
                    GameObject nodeSpawned = Instantiate(resourcePrefab, nodePos, Quaternion.identity);

                    nodeManager.nodeDuplicateCheck.Add(nodeSpawned.transform.position, nodePos);

                    nodeSpawned.transform.SetParent(this.transform);
                }
            }
        }
    }
}

public class NodeManager
{
    private Transform[] nodes;
    public Hashtable nodeDuplicateCheck = new Hashtable();
    public NodeManager (Transform[] node)
    {
        this.nodes = node;
    }

    public int RandomNodeSelection {
        get { 
            return Random.Range(0, nodes.Length); 
        }
    }

    public int RandomResourceSelection {
        get {
            return Random.Range(0, 100) % 50;
        }
    }

    public bool DoesResourceExist(Vector3 pos){
        return nodeDuplicateCheck.ContainsKey(pos);
    }

    public ResourceType ReturnRandomResourceNode {
        get {
            if (RandomResourceSelection >= 20) {
                return ResourceType.metalOre;
            } else {
                return ResourceType.stoneOre;
            }
        }
    }
}
