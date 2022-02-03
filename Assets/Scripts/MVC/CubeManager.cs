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
        InvokeRepeating("ActivateCubes", 0.0f, spawnDelay);
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
            //gameOver = true;
            //player.playerGameOver = true;
        }
    }

    void SpawnCubes()
    {
        if (spawnerData.CubeAmt < (spawnerData.MaxCubeAmt + 1))
        {
            for (int i = 0; i < spawnerData.cubesPerRow - 1; i++)
            {
                tempCube = Instantiate(spawnerData.Cube, spawnerData.spawnPoints[i], Quaternion.identity);
                container.CubeList.Add(tempCube);
                cubeData = tempCube.GetComponent<CubeData>();
                cubeData.spawnerID = i;
                cubeData.BoxID = spawnerData.CubeAmt;
                spawnerData.CubeAmt++;
                scoreKeeper.cubesActive++;
                scoreKeeper.activeCubesTxt.text = scoreKeeper.cubesActive.ToString();
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
                    scoreKeeper.cubesActive++;
                    scoreKeeper.scoreTxt.text = scoreKeeper.score.ToString();
                    scoreKeeper.activeCubesTxt.text = scoreKeeper.cubesActive.ToString();
                }
            }
        }
        
    }
    void CubeReactions()
    {
        foreach (GameObject cube in container.CubeList)
        {
            if (cube.activeSelf == true ) //code only runs if the cube is active.
            {
                cubeData = cube.GetComponent<CubeData>();
                //Active red cubes can only connect horizontally
                //use index 0-1 for checking if the cube has red cubes beside it.
                //0 for left 1 for right.
                if (cubeData.BoxList[0] != null && cubeData.BoxList[1] != null && cubeData.isMoving != true)
                {                    
                    
                    tempCubeData = cubeData.BoxList[0].GetComponent<CubeData>();
                    cubeData.BoxList[0].transform.position = spawnerData.spawnPoints[tempCubeData.spawnerID];
                    tempCubeData.CleanBoxList();
                    cubeData.BoxList[0].SetActive(false);
                    //
                    tempCubeData = cubeData.BoxList[1].GetComponent<CubeData>();
                    cubeData.BoxList[1].transform.position = spawnerData.spawnPoints[tempCubeData.spawnerID];
                    tempCubeData.CleanBoxList();
                    cubeData.BoxList[1].SetActive(false);
                    //
                    cube.transform.position = spawnerData.spawnPoints[cubeData.spawnerID];

                    cubeData.CleanBoxList();
                    cube.SetActive(false);
                    //add +3 to score
                    scoreKeeper.cubesActive -= 3;
                    scoreKeeper.score += 3;
                    scoreKeeper.scoreTxt.text = scoreKeeper.score.ToString();
                    scoreKeeper.activeCubesTxt.text = scoreKeeper.cubesActive.ToString();
                }
                //Active blue cubes can only connect vertically
                //use index 2-3 for checking if the cube has blue cubes above and below it
                if (cubeData.BoxList[2] != null && cubeData.BoxList[3] != null && cubeData.isMoving != true)
                {   
                    tempCubeData = cubeData.BoxList[2].GetComponent<CubeData>();
                    cubeData.BoxList[2].transform.position = spawnerData.spawnPoints[tempCubeData.spawnerID];
                    tempCubeData.CleanBoxList();
                    cubeData.BoxList[2].SetActive(false);
                    //
                    tempCubeData = cubeData.BoxList[3].GetComponent<CubeData>();
                    cubeData.BoxList[3].transform.position = spawnerData.spawnPoints[tempCubeData.spawnerID];
                    tempCubeData.CleanBoxList();
                    cubeData.BoxList[3].SetActive(false);
                    //
                    cube.transform.position = spawnerData.spawnPoints[cubeData.spawnerID];
                    cubeData.CleanBoxList();
                    cube.SetActive(false);
                    //add +3 to score
                    scoreKeeper.cubesActive -= 3;
                    scoreKeeper.score += 3;
                    scoreKeeper.scoreTxt.text = scoreKeeper.score.ToString();
                    scoreKeeper.activeCubesTxt.text = scoreKeeper.cubesActive.ToString();

                }


            }
        }
    }
}
