using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    public Maze MazePrefab;

    public GameObject PlayerPrefab;

    private Maze MazeInstance;

    void Start()
    {
        this.BeginGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.RestartGame();
        }
    }

    private void BeginGame()
    {
        this.MazeInstance = GameObject.Instantiate(this.MazePrefab) as Maze;
        StartCoroutine(this.MazeInstance.Generate(this.PlayerPrefab, 1));
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        GameObject.Destroy(this.MazeInstance.gameObject);
        this.BeginGame();
    }
}
