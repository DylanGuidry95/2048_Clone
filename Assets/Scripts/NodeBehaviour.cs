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

    public float MovementSpeed;

    private void Awake() 
    {        
        FaceValue = StartingValue[Random.Range(0,2)];
        ValueView = GetComponentInChildren<Text>();
        ValueView.text = FaceValue.ToString();
    }

    public void UpdateFaceValue(int value)
    {
        FaceValue = value;
        ValueView.text = FaceValue.ToString();
    }

    public void Move(Vector3 target)
    {
        var startPosition = transform.position;
        while(Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.Lerp(startPosition, target, 
            MovementSpeed);        
        }        
    }
}
