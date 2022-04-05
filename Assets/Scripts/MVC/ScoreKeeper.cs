using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int score;
    public int activeCubes;
    public Text scoreTxt;
    public Text activeCubesTxt;
    public CubeDataContainer cubeDataContainer;
    public CubeManager cubeManager;
    public SpawnerData spawnerData;
    // Start is called before the first frame update
    void Start()
    {
        cubeDataContainer = GameObject.Find("CubeDataHolder").GetComponent<CubeDataContainer>();
        cubeManager = GameObject.Find("CubeManager").GetComponent <CubeManager>();
        spawnerData = GameObject.Find("CubeSpawnerData").GetComponent<SpawnerData>();
    }

    // Update is called once per frame
    void Update()
    {
        SetScore();
    }


    void SetScore()
    {
        activeCubes = spawnerData.activeCubeAmt;
        activeCubesTxt.text = activeCubes.ToString();
        scoreTxt.text = score.ToString();
    }
    
}
