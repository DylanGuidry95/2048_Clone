using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeBehaviour : MonoBehaviour
{    
    public int FaceValue;
    readonly int[] StartingValue = new int[2] {2,4};
    [SerializeField]
    private Text ValueView;

    private void Awake() 
    {        
        FaceValue = StartingValue[Random.Range(0,2)];
        ValueView = GetComponentInChildren<Text>();
        ValueView.text = FaceValue.ToString();
    }

    bool CompareNode(NodeBehaviour node)
    {        
        return node.FaceValue == this.FaceValue;
    }

    public void UpdateFaceValue(int value)
    {
        FaceValue = value;
        ValueView.text = FaceValue.ToString();
    }
}
