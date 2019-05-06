using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public List<NodeBehaviour> Nodes;
    public List<GameObject> Spaces;
    public Dictionary<GameObject, NodeBehaviour> PlaySpaces;    

    public GameObject NodePrefab;

    public enum EDimensions
    {
        Two_By_Two = 2, Three_by_Three = 3, Four_by_Four = 4, Five_by_Five = 5, Six_by_Six = 6
    }

    public EDimensions GridSize;

    private void Awake() 
    {
        Nodes = new List<NodeBehaviour>();
        PlaySpaces = new Dictionary<GameObject, NodeBehaviour>();
        for(int x = 0; x < (int)GridSize; x++)
        {
            for(int y = 0; y < (int)GridSize; y++)
            {
                var space = new GameObject();
                space.transform.position = new Vector2(x,y) * 2;
                space.name = (PlaySpaces.Count + 1).ToString();
                Spaces.Add(space);
                PlaySpaces.Add(space, null);
            }            
        }
        Restart();
    }

    void Restart()
    {
        foreach (var node in Nodes)
        {
            Destroy(node.gameObject);
        }

        Nodes = new List<NodeBehaviour>();        

        int placeOne, placeTwo;
        placeOne = Random.Range(0, PlaySpaces.Count - 1);                    
        var newNodeOne = GameObject.Instantiate(NodePrefab);
        newNodeOne.transform.position = Spaces[placeOne].transform.position;
        Nodes.Add(newNodeOne.GetComponent<NodeBehaviour>());

        placeTwo = Random.Range(0, PlaySpaces.Count - 1);
        if(placeOne == placeTwo)
            placeTwo = placeOne + 1 > PlaySpaces.Count - 1 ? placeOne - 1 : placeOne + 1;
        var newNodeTwo = GameObject.Instantiate(NodePrefab);
        newNodeTwo.transform.position = Spaces[placeTwo].transform.position;
        Nodes.Add(newNodeTwo.GetComponent<NodeBehaviour>());

        PlaySpaces[Spaces[placeOne]] = newNodeOne.GetComponent<NodeBehaviour>();
        PlaySpaces[Spaces[placeTwo]] = newNodeTwo.GetComponent<NodeBehaviour>();
    }    

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Restart();    
    }

    private void OnDrawGizmos() 
    {
        if(PlaySpaces == null)
            return;
        foreach (var space in Spaces)
        {
            Gizmos.color = new Color(1,1,1,0.2f);
            Gizmos.DrawCube(space.transform.position, new Vector3(1,1,1));
        }        
    }

    public bool CheckVacancy(Vector3 position)
    {        
        foreach (var space in Spaces)
        {
            if(space.transform.position == position)
                return PlaySpaces[space] == null;
        }
        return false;
    }

    public void CheckSpaces()
    {
        foreach (var space in Spaces)
        {
            bool hasNode = false;
            foreach (var node in Nodes)
            {
                if(node.transform.position == space.transform.position)
                {
                    PlaySpaces[space] = node;
                    hasNode = true;
                }
            }
            PlaySpaces[space] = !hasNode ? null : PlaySpaces[space];
        }        
    }
}
