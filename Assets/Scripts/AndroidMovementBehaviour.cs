using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidMovementBehaviour : MovementBehaviour
{
    private void Awake()
    {
        GridRef = GetComponent<GridBehaviour>();
        if (Application.platform != RuntimePlatform.Android)
        {
            this.enabled = false;
        }
    }

    bool IsSwipping;
    Vector2 LastTouchPosition;
    Vector2 SwipeDirection;
    private void Update()
    {
        if (Input.touchCount == 0)
            return;        
        if(Input.GetTouch(0).deltaPosition.sqrMagnitude != 0)
        {
            if (!IsSwipping)
            {
                IsSwipping = true;
                LastTouchPosition = Input.GetTouch(0).position;
                return;
            }
            if(IsSwipping)
            {
                var direction = Input.GetTouch(0).position - LastTouchPosition;   
            }
        }
        else
        {
            if (Mathf.Abs(SwipeDirection.x) > Mathf.Abs(SwipeDirection.y))
            {
                if (SwipeDirection.x > 0)
                    Move(EDirections.Right);
                else
                    Move(EDirections.Left);
            }
            else
            {
                if (SwipeDirection.y > 0)
                    Move(EDirections.Up);
                else
                    Move(EDirections.Down);
            }
            SwipeDirection = Vector3.zero;
            IsSwipping = false;
        }
    }
}
