using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class GameManager : MonoBehaviour
{
    public static event Action OnCubeSpawn = delegate { };

    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    private void Update()
    {
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Debug.Log("Touch pressed");
            if (MovingCube.CurrentCube != null)
            {
                MovingCube.CurrentCube.Stop();
            }

            // FindObjectOfType<CubeSpawner>().SpawnCube();
            spawnerIndex = spawnerIndex == 0 ? 1 : 0;

            currentSpawner = spawners[spawnerIndex];

            currentSpawner.SpawnCube();
            OnCubeSpawn();
        }
    }
}