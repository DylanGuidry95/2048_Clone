using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeBehaviour : MonoBehaviour
{    
    public int FaceValue;
    readonly int[] StartingValue = new int[4] {2,4,8,16};
    [SerializeField]
    private Text ValueView;

    private void Awake() 
    {        
        FaceValue = StartingValue[Random.Range(0,4)];        
        ValueView.text = FaceValue.ToString();
    }

    bool CompareNode(NodeBehaviour node)
    {        
        return node.FaceValue == this.FaceValue;     
    }
}
