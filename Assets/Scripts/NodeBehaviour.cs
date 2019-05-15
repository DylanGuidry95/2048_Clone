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
        ValueView = GetComponentInChildren<Text>();        
        UpdateFaceValue(StartingValue[Random.Range(0, 2)]);
    }

    public void UpdateFaceValue(int value)
    {
        FaceValue = value;
        ValueView.text = FaceValue.ToString();
        Material mat = GetComponent<MeshRenderer>().material;
        switch(FaceValue)
        {
            case 2:
                mat = Resources.Load("Metal") as Material;
                break;
            case 4:
                mat = Resources.Load("Wood") as Material;
                break;
            case 8:
                mat = Resources.Load("Ice") as Material;
                break;
            case 16:
                mat = Resources.Load("Rock") as Material;
                break;
            case 32:
                mat = Resources.Load("Wood") as Material;
                break;
            case 64:
                mat = Resources.Load("Ice") as Material;
                break;
            case 128:
                mat = Resources.Load("Metal") as Material;
                break;
            case 256:
                mat = Resources.Load("Wood") as Material;
                break;
            case 512:
                mat = Resources.Load("Ice") as Material;
                break;
            case 1024:
                mat = Resources.Load("Metal") as Material;
                break;
            case 2048:
                mat = Resources.Load("Wood") as Material;
                break;
        }
        GetComponent<MeshRenderer>().material = mat;
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
