using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private MovingCube cubePrefab;
    [SerializeField] private MoveDirection _moveDirection;

    public void SpawnCube()
    {
        var cube = Instantiate(cubePrefab);

        if (MovingCube.LastCube != null &&
            MovingCube.LastCube.gameObject != GameObject.Find("StartCube"))
        {
            cube.transform.position = new Vector3(transform.position.x,
                MovingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y,
                transform.position.z);
        }
        else
        {
            cube.transform.position = transform.position;
        }

        cube.MoveDirection = _moveDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}