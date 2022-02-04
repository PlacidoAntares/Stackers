using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    
    public SpawnerData spawnerData;
    public CubeDataContainer container;
    public CubeData cubeData;
    public CubeData tempCubeData;
    public ScoreKeeper scoreKeeper;
    public Player player;
    public float spawnDelay;
    private GameObject tempCube;
    private bool gameOver;
    // Start is called before the first frame update
    void Start()
    {        
        spawnerData = GameObject.Find("CubeSpawnerData").GetComponent<SpawnerData>();
        container = GameObject.Find("CubeDataHolder").GetComponent<CubeDataContainer>();
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        player = GameObject.Find("PlayerControl").GetComponent<Player>();
        InvokeRepeating("SpawnCubes", 0.0f, spawnDelay);
        InvokeRepeating("ActivateCubes", 0.0f, spawnDelay + 1.0f);
        gameOver = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
          CubeReactions();
          GameOver();
        }
       
    }

    void GameOver()
    {
        if (scoreKeeper.cubesActive >= spawnerData.MaxCubeAmt)
        {
            gameOver = true;
            player.playerGameOver = true;
        }
    }

    void SpawnCubes()
    {
        if (spawnerData.CubeAmt < (spawnerData.MaxCubeAmt + 1))
        {
            for (int i = 0; i < spawnerData.cubesPerRow - 1; i++)
            {
                tempCube = Instantiate(spawnerData.Cube, spawnerData.spawnPoints[i] + new Vector2(0,1.0f), Quaternion.identity);
                container.CubeList.Add(tempCube);
                cubeData = tempCube.GetComponent<CubeData>();
                cubeData.spawnerID = i;
                spawnerData.CubeAmt++;
            }
        }         
    }

    void ActivateCubes()
    {
        if (!gameOver)
        {
            //Debug.Log("Activating inactive cubes");
            foreach (GameObject cube in container.CubeList)
            {
                if (cube.activeSelf == false)
                {                    
                    cube.SetActive(true);
                    tempCubeData = cube.GetComponent<CubeData>();
                    cube.transform.position = spawnerData.spawnPoints[tempCubeData.spawnerID];                    
                    cubeData.SetSprite();
                    cubeData.CleanBoxList();
                    cubeData.isMoving = true;
                }
            }
        }
        
    }
    void CubeReactions()
    {
        foreach (GameObject cube in container.CubeList)
        {
            if (cube.activeSelf == true) //code only runs if the cube is active.
            {
                //Use BoxList Index 0-1 for Red cubes
                //0 for left 1 for right
                //Red cubes only connect sideways
                if (cube.gameObject.tag == "Red")
                {
                    cubeData = cube.GetComponent<CubeData> ();
                    if (cubeData.BoxList[0] != null && cubeData.BoxList[1] != null)
                    {
                        
                        tempCubeData = cubeData.BoxList[0].GetComponent<CubeData>();
                        tempCubeData.CleanBoxList ();
                        cubeData.BoxList[0].SetActive(false);
                        
                        tempCubeData = cubeData.BoxList[1].GetComponent<CubeData>();
                        tempCubeData.CleanBoxList();
                        cubeData.BoxList[1].SetActive(false);
                        cube.SetActive(false);
                        scoreKeeper.score++;
                    }
                }
                //Use BoxList Index 2-3 for Blue cubes
                //2 for up 3 for down
                //Blue cubes only connect vertically
                else if (cube.gameObject.tag == "Blue")
                {
                    cubeData = cube.GetComponent<CubeData>();
                    if (cubeData.BoxList[2] != null && cubeData.BoxList[3] != null)
                    {
                        
                        tempCubeData = cubeData.BoxList[2].GetComponent<CubeData>();
                        tempCubeData.CleanBoxList();
                        cubeData.BoxList[2].SetActive(false);
                        //
                        
                        tempCubeData = cubeData.BoxList[3].GetComponent<CubeData>();
                        tempCubeData.CleanBoxList();
                        cubeData.BoxList[3].SetActive(false);
                        cube.SetActive(false);
                        scoreKeeper.score++;
                    }
                }
            }
        }
    }
}
