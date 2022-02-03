using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int score;
    public int cubesActive;
    public Text scoreTxt;
    public Text activeCubesTxt;
    public CubeDataContainer cubeDataContainer;
    // Start is called before the first frame update
    void Start()
    {
        cubeDataContainer = GameObject.Find("CubeDataHolder").GetComponent<CubeDataContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
