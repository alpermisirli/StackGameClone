using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        //TODO IF TOUCH STOP MOVING CUBE
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Debug.Log("Touch pressed");
            MovingCube.CurrentCube.Stop();
        }
    }
}