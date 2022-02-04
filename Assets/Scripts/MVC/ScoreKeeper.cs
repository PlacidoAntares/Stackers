using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int score;
    public int cubesActive;
    public int activeCubes;
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
        SetScore();
        TallyActiveCubes();
    }

    void TallyActiveCubes()
    {
        activeCubes = CountActiveCubes();
    }

    private int CountActiveCubes()
    {
        cubesActive = 0;
        for (int i = 0; i < cubeDataContainer.CubeList.Count; i++)
        {
            if (cubeDataContainer.CubeList[i].activeSelf == true)
            {
                cubesActive++;
            }
 
        }
        return cubesActive;
    }
    void SetScore()
    {
        activeCubesTxt.text = activeCubes.ToString();
        scoreTxt.text = score.ToString();
    }
    
}
