using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridBehaviour))]
public class MovementBehaviour : MonoBehaviour
{
    enum EDirections
    {
        Left, Right, Up, Down
    }

    public GridBehaviour GridRef;

    private void Awake()
    {
        GridRef = GetComponent<GridBehaviour>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(EDirections.Down);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(EDirections.Up);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(EDirections.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(EDirections.Right);
        }
    }

    void Move(EDirections direction)
    {
        bool nodesMoved = false;
        do
        {
            nodesMoved = false;
            foreach (var cell in GridRef.Cells)
            {
                if (cell.IsVacant)
                    continue;
                switch (direction)
                {
                    case EDirections.Up:
                        if (cell.AdjacentNodes.Top == null)
                            break;
                        if (cell.AdjacentNodes.Top.MoveNodeInto(cell) != 0)
                        {
                            nodesMoved = true;
                            transform.rotation = Quaternion.identity;
                            transform.Rotate(new Vector3(10, 0, 0));
                        }
                        break;
                    case EDirections.Down:
                        if (cell.AdjacentNodes.Bottom == null)
                            break;
                        if (cell.AdjacentNodes.Bottom.MoveNodeInto(cell) != 0)
                        {
                            nodesMoved = true;
                            transform.rotation = Quaternion.identity;
                            transform.Rotate(new Vector3(-10, 0, 0));
                        }
                        break;
                    case EDirections.Left:
                        if (cell.AdjacentNodes.Left == null)
                            break;
                        if (cell.AdjacentNodes.Left.MoveNodeInto(cell) != 0)
                        {
                            nodesMoved = true;
                            transform.rotation = Quaternion.identity;
                            GridRef.transform.Rotate(new Vector3(0, 10f, 0));
                        }
                        break;
                    case EDirections.Right:
                        if (cell.AdjacentNodes.Right == null)
                            break;
                        if (cell.AdjacentNodes.Right.MoveNodeInto(cell) != 0)
                        {
                            nodesMoved = true;
                            transform.rotation = Quaternion.identity;
                            transform.Rotate(new Vector3(0, -10f, 0));
                        }
                        break;
                }
            }
        } while (nodesMoved);
        GridRef.SpawnNewNode();
    }
}
