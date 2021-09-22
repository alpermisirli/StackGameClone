using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class GameManager : MonoBehaviour
{
    public static event Action OnCubeSpawn = delegate { };
    private Camera m_MainCamera;
    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;
    [SerializeField] private float camSpeed;

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    private void Start()
    {
        //This gets the Main Camera from the Scene
        m_MainCamera = Camera.main;
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

            m_MainCamera.transform.position = Vector3.MoveTowards(m_MainCamera.transform.position, new Vector3(
                m_MainCamera.transform.position.x,
                m_MainCamera.transform.position.y + 0.18f, m_MainCamera.transform.position.z), camSpeed * Time.deltaTime);
            // FindObjectOfType<CubeSpawner>().SpawnCube();
            spawnerIndex = spawnerIndex == 0 ? 1 : 0;

            currentSpawner = spawners[spawnerIndex];

            currentSpawner.SpawnCube();
            OnCubeSpawn();
        }
    }
}