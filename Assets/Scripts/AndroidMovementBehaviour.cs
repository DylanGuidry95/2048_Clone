﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidMovementBehaviour : MovementBehaviour
{
    private void Awake()
    {
        GridRef = GetComponent<GridBehaviour>();
        Input.simulateMouseWithTouches = true;
    }

    bool IsSwipping;
    public Vector2 LastTouchPosition;
    public Vector2 SwipeDirection;
    private void Update()
    {                    
        if(Input.GetMouseButton(0))
        {
            var positionDif = (Vector2)Input.mousePosition - LastTouchPosition;
            LastTouchPosition = Input.mousePosition;
            SwipeDirection = positionDif.normalized;
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(Mathf.Abs(SwipeDirection.x) > Mathf.Abs(SwipeDirection.y))
            {
                if (SwipeDirection.x > 0)
                    StartCoroutine(Move(EDirections.Right));
                else
                    StartCoroutine(Move(EDirections.Left));
            }
            else
            {
                if (SwipeDirection.y > 0)
                    StartCoroutine(Move(EDirections.Up));
                else
                    StartCoroutine(Move(EDirections.Down));
            }
        }
    }
}
