using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        GameManager.OnCubeSpawn += GameManagerOnOnCubeSpawn;
    }

    private void OnDestroy()
    {
        GameManager.OnCubeSpawn -= GameManagerOnOnCubeSpawn;
    }

    private void GameManagerOnOnCubeSpawn()
    {
        score++;
        text.text = score.ToString();
    }
}