using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GridBehaviour GridRef;

    private void Start()
    {
        GridRef = FindObjectOfType<GridBehaviour>();
        var gridCenter = GridRef.Cells[GridRef.Cells.Count - 1].transform.position / 2;
        transform.position = gridCenter + new Vector3(0, 0, -(int)GridRef.GridSize);        
    }
}
