using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    [SerializeField] private float moveSpeed = 1f;

    private void Update()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
        }
    }

    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCube>();
        }

        CurrentCube = this;

        GetComponent<Renderer>().material.color = GetRandomColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y,
            LastCube.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f),
            UnityEngine.Random.Range(0, 1f));
    }

    internal void Stop()
    {
        moveSpeed = 0f;
        float hangover = GetHangover();


        float max;
        if (MoveDirection == MoveDirection.Z)
        {
            max = LastCube.transform.localScale.z;
        }
        else
        {
            max = LastCube.transform.localScale.x;
        }

        if (Mathf.Abs(hangover) >= max)
        {
            LastCube = null;
            CurrentCube = null;
            Debug.Log("Game over");
            SceneManager.LoadScene(0);
        }

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

        if (MoveDirection == MoveDirection.Z)
        {
            SplitCubeOnZ(hangover, direction);
        }
        else
        {
            SplitCubeOnX(hangover, direction);
        }

        LastCube = this;
    }

    private float GetHangover()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            return transform.position.z - LastCube.transform.position.z;
        }
        else
        {
            return transform.position.x - LastCube.transform.position.x;
        }
    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        //Calculating the size of falling block
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;
        //Calculating the position of remaining cube
        float newXPosition = LastCube.transform.position.x + (hangover / 2f);
        //Setting new scale and position of cube
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEgde = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPos = cubeEgde + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockXPos, fallingBlockSize);
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

        float cubeEgde = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPos = cubeEgde + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockZPos, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockPos, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockPos);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockPos, transform.position.y, transform.position.z);
        }


        cube.AddComponent<Rigidbody>();
        //Making the new cube the same color as the moving cube
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);
    }
}