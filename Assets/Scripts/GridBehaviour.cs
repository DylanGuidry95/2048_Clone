using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{    
    public List<CellBehaviour> Cells;    

    public enum EDimensions
    {
        Two_By_Two = 2, Three_by_Three = 3, Four_by_Four = 4, Five_by_Five = 5, Six_by_Six = 6
    }

    public EDimensions GridSize;

    private void Awake() 
    {
        Cells = new List<CellBehaviour>();
        for(int x = 0; x < (int)GridSize; x++)
        {
            for(int y = 0; y < (int)GridSize; y++)
            {
                var space = new GameObject();
                space.transform.position = new Vector2(x,y) * 2;
                space.name = (Cells.Count + 1).ToString();
                space.AddComponent(typeof(CellBehaviour));
                Cells.Add(space.GetComponent<CellBehaviour>());                
            }            
        }

        foreach (var cell in Cells)
        {            
            foreach (var otherCell in Cells)
            {
                if(Vector3.Distance(cell.transform.position, otherCell.transform.position) <= 2)
                {
                    var cellPosition = cell.transform.position;
                    var otherCellPosition = otherCell.transform.position;
                    if(cellPosition.x == otherCellPosition.x && cellPosition.y < otherCellPosition.y)
                    {
                        cell.AdjacentNodes.Top = otherCell;
                    }
                    else if(cellPosition.x == otherCellPosition.x && cellPosition.y > otherCellPosition.y)
                    {
                        cell.AdjacentNodes.Bottom = otherCell;
                    }
                    else if(cellPosition.x > otherCellPosition.x && cellPosition.y == otherCellPosition.y)
                    {
                        cell.AdjacentNodes.Left = otherCell;
                    }
                    else if(cellPosition.x < otherCellPosition.x && cellPosition.y == otherCellPosition.y)
                    {
                        cell.AdjacentNodes.Right = otherCell;
                    }
                }
            }
        }

        Restart(0);
    }

    void Restart(int placed)
    {   
        if(placed == 0)
        {
            foreach (var cell in Cells)
            {                
                cell.ClearCell();
            }
        }    
        if(placed >= 2)
            return;        
        SpawnNewNode();
        Restart(placed + 1);
    }    

    public void SpawnNewNode(int numberSpawns = 1)
    {
        if(!HasEmptySpace())
            return;
        for(int i = 0; i < numberSpawns; i++)
        {
            int placeOne = 0;
            do
            {
                placeOne = Random.Range(0, Cells.Count);
            } while (Cells[placeOne].SpawnNode() == 0);
        }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Restart(0);    
    }

    private void OnDrawGizmos() 
    {
        if(Cells.Count == 0)
            return;
        Gizmos.color = new Color(1,1,1,0.25f);
        foreach (var cells in Cells)
        {
            Gizmos.DrawCube(cells.gameObject.transform.position, new Vector3(1,1,1));
        }
    }    

    private bool HasEmptySpace()
    {
        foreach (var cell in Cells)
        {
            if(cell.IsVacant)
                return true;
        }
        return false;
    }
}