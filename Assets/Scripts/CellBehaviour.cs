﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    public struct Neighbors
    {
        public CellBehaviour Left, Right, Top, Bottom;
        public CellBehaviour CheckNextVacancy(int direction)
        {
            switch (direction)
            {
                case 1:
                    if (Top == null && Top.IsVacant)
                        return null;
                    return Top;
                case 2:
                    if (Bottom == null && Bottom.IsVacant)
                        return null;
                    return Bottom;
                case 3:
                    if (Left == null && Left.IsVacant)
                        return null;
                    return Left;
                case 4:
                    if (Right == null && Right.IsVacant)
                        return null;
                    return Right;
            }
            return null;
        }

        public CellBehaviour Neighbor(int index)
        {
            switch (index)
            {
                case 0:
                    return Top;
                case 1:
                    return Bottom;
                case 2:
                    return Left;
                case 3:
                    return Right;
            }
            return null;
        }
    }
    public Vector3 Position;
    private NodeBehaviour Node;

    public Neighbors AdjacentNodes;

    public bool IsVacant
    {
        get
        {
            return Node == null;
        }
    }

    private void Update()
    {
        if (Node != null)
        {
            Node.transform.position = transform.position + new Vector3(0, 0f, -0.1f);
            Node.transform.rotation = this.transform.rotation;
        }
    }

    public int TryMergeNodes(NodeBehaviour node)
    {
        if (Node.FaceValue == node.FaceValue)
        {
            int NewValue = Node.FaceValue + node.FaceValue;
            Destroy(Node.gameObject);
            Destroy(node.gameObject);
            var newNode = Instantiate(Resources.Load("Node", typeof(GameObject)), transform.position + new Vector3(0, 0f, -0.1f), this.transform.rotation) as GameObject;
            newNode.GetComponent<NodeBehaviour>().UpdateFaceValue(NewValue);
            Node = newNode.GetComponent<NodeBehaviour>();
            Events.MergedCells.Invoke(transform.position, Node.FaceValue);
            if (Node.FaceValue == 2048)
                Events.Victory.Invoke();
            return 1;
        }
        return 0;
    }

    public int MoveNodeInto(CellBehaviour cell)
    {
        if (IsVacant)
        {
            Node = cell.Node;
            cell.Node = null;
            var dir = Node.transform.position - this.transform.position;
            Node.transform.Translate(dir * Time.deltaTime * 0.01f);
            Node.transform.position = this.transform.position + new Vector3(0, 0, -0.1f);
            Node.transform.rotation = this.transform.rotation;
            return 1;
        }
        else if (TryMergeNodes(cell.Node) == 1)
        {
            cell.Node = null;
            return -1;
        }
        return 0;
    }

    public int SpawnNode()
    {
        if (!IsVacant)
            return 0;
        var newNode = Instantiate(Resources.Load("Node", typeof(GameObject)), transform.position + new Vector3(0, 0, -0.1f), this.transform.rotation) as GameObject;
        Node = newNode.GetComponent<NodeBehaviour>();
        return 1;
    }

    public void ClearCell()
    {
        if (IsVacant)
            return;
        Destroy(Node.gameObject);
        Node = null;
    }

    public bool HasValidMove()
    {
        for (int i = 0; i < 4; i++)
        {
            if (AdjacentNodes.Neighbor(i)?.Node == null || IsVacant)
                continue;
            else if (AdjacentNodes.Neighbor(i).Node.FaceValue == Node.FaceValue)
                return true;
        }
        return false;
    }
}
