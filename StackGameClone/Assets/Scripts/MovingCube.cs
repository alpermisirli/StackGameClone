using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }

    [SerializeField] private float moveSpeed = 1f;

    private void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }

    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCube>();
        }

        CurrentCube = this;
    }

    internal void Stop()
    {
        moveSpeed = 0f;
        float hangover = transform.position.z - LastCube.transform.position.z;
        float direction;
        // Debug.Log(hangover);
        if (hangover > 0)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }

        SplitCubeOnZ(hangover, direction);
    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        //Calculating the size of falling block
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;
        //Calculating the position of remaining cube
        float newZPosition = LastCube.transform.position.z + (hangover / 2f);
        //Setting new scale and position of cube
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEgde = transform.position.z + (newZSize / 2f) * direction;
        float fallingBlockZPos = cubeEgde + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockZPos, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPos, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPos);
    }
}