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
        GenerateCells();
        Events.RestartGame.AddListener(Restart);
        Restart();
    }

    void GenerateCells()
    {
        Cells = new List<CellBehaviour>();
        for (int x = 0; x < (int)GridSize; x++)
        {
            for (int y = 0; y < (int)GridSize; y++)
            {
                var space = Instantiate(Resources.Load("EmptyCell", typeof(GameObject)), new Vector2(x, y) * 1.1f, Quaternion.identity) as GameObject;
                space.name = (Cells.Count + 1).ToString();
                space.transform.parent = this.transform;
                Cells.Add(space.GetComponent<CellBehaviour>());
            }
        }

        foreach (var cell in Cells)
        {
            foreach (var otherCell in Cells)
            {
                if (Vector3.Distance(cell.transform.position, otherCell.transform.position) <= 2)
                {
                    var cellPosition = cell.transform.position;
                    var otherCellPosition = otherCell.transform.position;
                    if (cellPosition.x == otherCellPosition.x && cellPosition.y < otherCellPosition.y)
                    {
                        cell.AdjacentNodes.Top = otherCell;
                    }
                    else if (cellPosition.x == otherCellPosition.x && cellPosition.y > otherCellPosition.y)
                    {
                        cell.AdjacentNodes.Bottom = otherCell;
                    }
                    else if (cellPosition.x > otherCellPosition.x && cellPosition.y == otherCellPosition.y)
                    {
                        cell.AdjacentNodes.Left = otherCell;
                    }
                    else if (cellPosition.x < otherCellPosition.x && cellPosition.y == otherCellPosition.y)
                    {
                        cell.AdjacentNodes.Right = otherCell;
                    }
                }
            }
        }
    }    

    void Restart()
    {
        transform.rotation = Quaternion.identity;
        foreach (var cell in Cells)
        {
            cell.ClearCell();
        }
        SpawnNewNode(2);        
    }

    public void SpawnNewNode(int numberSpawns = 1)
    {
        if (!HasEmptySpace())
            return;
        for (int i = 0; i < numberSpawns; i++)
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
        if (Input.GetKeyDown(KeyCode.Space))
            Restart();
    }

    private bool HasEmptySpace()
    {
        bool hasValidMove = false;
        foreach (var cell in Cells)
        {            
            if (cell.IsVacant)
                return true;
            if (cell.HasValidMove())
                hasValidMove = true;
        }
        if (!hasValidMove)
            Events.NoValidMovesRemain.Invoke();
        return false;
    }
}