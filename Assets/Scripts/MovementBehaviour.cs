using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridBehaviour))]
public class MovementBehaviour : MonoBehaviour
{
    public enum EDirections
    {
        Left = 2, Right = 3, Up = 0, Down = 1
    }

    public GridBehaviour GridRef;

    public bool IsUpdating;

    public float MovementSpeed;

    private void Awake()
    {
        GridRef = GetComponent<GridBehaviour>();
        if (Application.platform == RuntimePlatform.Android)
            this.enabled = false;
        IsUpdating = false;
    }

    private void Update()
    {
        if (IsUpdating)
            return;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(Move(EDirections.Down));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {            
            StartCoroutine(Move(EDirections.Up));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {            
            StartCoroutine(Move(EDirections.Left));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Move(EDirections.Right));
        }
    }

    protected IEnumerator Move(EDirections direction)
    {
        bool nodesMoved = false;
        IsUpdating = true;
        bool WasMoved = false;
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
                            WasMoved = true;                            
                            yield return new WaitForSeconds(MovementSpeed);
                        }
                        break;
                    case EDirections.Down:
                        if (cell.AdjacentNodes.Bottom == null)
                            break;
                        if (cell.AdjacentNodes.Bottom.MoveNodeInto(cell) != 0)
                        {
                            nodesMoved = true;                            
                            WasMoved = true;
                            yield return new WaitForSeconds(MovementSpeed);
                        }
                        break;
                    case EDirections.Left:
                        if (cell.AdjacentNodes.Left == null)
                            break;
                        if (cell.AdjacentNodes.Left.MoveNodeInto(cell) != 0)
                        {
                            nodesMoved = true;                            
                            WasMoved = true;
                            yield return new WaitForSeconds(MovementSpeed);
                        }
                        break;
                    case EDirections.Right:
                        if (cell.AdjacentNodes.Right == null)
                            break;
                        if (cell.AdjacentNodes.Right.MoveNodeInto(cell) != 0)
                        {
                            nodesMoved = true;
                            WasMoved = true;                            
                            yield return new WaitForSeconds(MovementSpeed);
                        }
                        break;
                }                
            }            
        } while (nodesMoved);
        if (WasMoved)
        {
            GridRef.SpawnNewNode(1);
            Events.ScoreUpdate.Invoke(1);
        }
        IsUpdating = false;
    }
}