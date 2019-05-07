using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    struct Neighbors
    {
        public CellBehaviour Left, Right, Top, Bottom;
        public CellBehaviour CheckNextVacancy(int direction)
        {
            switch(direction)
            {
                case 1:
                    if(Top == null && Top.IsVacant)
                        return null;
                    return Top;                
                case 2:
                    if(Bottom == null && Bottom.IsVacant)
                            return null;
                        return Bottom;                
                case 3:
                    if(Left == null && Left.IsVacant)
                        return null;
                    return Left;
                case 4:
                    if(Right == null && Right.IsVacant)
                        return null;
                    return Right;
            }
            return null;
        }
    }
    public Vector3 Position;
    private NodeBehaviour Node;   

    public bool IsVacant
    {
        get
        {
            return Node == null;
        }
    }

   public int TryMergeNodes(NodeBehaviour node)
   {
        if(IsVacant)
            return -1;
        if(Node.FaceValue == node.FaceValue)
        {
            var newNode = Instantiate(Resources.Load("Node", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
            newNode.GetComponent<NodeBehaviour>().FaceValue = Node.FaceValue + node.FaceValue;            
            Destroy(Node.gameObject);
            Destroy(node.gameObject);            
            return 1;
        }
        return 0;
   }

   public int MoveNodeInto(NodeBehaviour node)
   {
        if(IsVacant)
        {
            Node = node;
            Node.transform.position = this.transform.position;
            return 1;
        }
        return TryMergeNodes(node);
   }

   public int SpawnNode()
   {
        if(!IsVacant)
            return 0;
        var newNode = Instantiate(Resources.Load("Node", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        Node = newNode.GetComponent<NodeBehaviour>();
        return 1;
   }

   public void ClearCell()
   {
        if(IsVacant)
            return;
        Destroy(Node.gameObject);        
   }
}
