using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject clickedCube;
    public bool playerGameOver;
    CubeData cubeDataP;
    ScoreKeeper scoreKeeper;
    CubeManager cubeManager;
    SpawnerData spawnerData;
    private PlayerTouch playerTouch;
    private Camera mainCam;
    // Start is called before the first frame update

    private void Awake()
    {
        playerTouch = PlayerTouch.Instance;
    }
    void Start()
    {
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        cubeManager = GameObject.Find("CubeManager").GetComponent<CubeManager>();
        spawnerData = GameObject.Find("CubeSpawnerData").GetComponent<SpawnerData>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (!playerGameOver)
        {
            //PlayerActions();
        }
        
    }

    private void OnEnable()
    {
        playerTouch.OnStartTouch += Tap;
    }

    private void OnDisable()
    {
        playerTouch.OnEndTouch -= Tap;
    }

    public void Tap(Vector2 screenPosition)
    {
        Vector3 tapPos = screenPosition;
        tapPos.z = 5.0f;
        Vector2 v = Camera.main.ScreenToWorldPoint(tapPos);
        Collider2D[] col = Physics2D.OverlapPointAll(v);
        if (col.Length > 0)
        {
            foreach (Collider2D c in col)
            {
                if (c.gameObject.tag == "White")
                {
                    clickedCube = c.gameObject;
                    cubeDataP = clickedCube.GetComponent<CubeData>();
                    for (int i = 0; i < cubeDataP.BoxList.Length; i++)
                    {
                        if (cubeDataP.BoxList[i] != null && cubeDataP.BoxList[i].gameObject.tag != "Ground")
                        {
                            cubeDataP.BoxList[i].SetActive(false);
                            scoreKeeper.score += 1;
                            scoreKeeper.activeCubes -= 1;
                            spawnerData.activeCubeAmt -= 1;
                        }

                    }
                    clickedCube.SetActive(false);
                    scoreKeeper.score += 1;
                    scoreKeeper.activeCubes -= 1;
                    spawnerData.activeCubeAmt -= 1;
                }
            }
        }
    }
    void PlayerActions()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.0f;
            Vector2 v = Camera.main.ScreenToWorldPoint(mousePos);
            Collider2D[] col = Physics2D.OverlapPointAll(v);
            if (col.Length > 0)
            {
                foreach (Collider2D c in col)
                {
                    if (c.gameObject.tag == "White")
                    {
                        clickedCube = c.gameObject;
                        cubeDataP = clickedCube.GetComponent<CubeData>();
                        for (int i = 0; i < cubeDataP.BoxList.Length; i++)
                        {
                            if (cubeDataP.BoxList[i] != null && cubeDataP.BoxList[i].gameObject.tag != "Ground")
                            {
                                cubeDataP.BoxList[i].SetActive(false);                               
                                scoreKeeper.score += 1;
                                scoreKeeper.activeCubes -= 1;
                                spawnerData.activeCubeAmt -= 1;
                            }

                        }
                        clickedCube.SetActive(false);
                        scoreKeeper.score += 1;
                        scoreKeeper.activeCubes -= 1;
                        spawnerData.activeCubeAmt -= 1;
                    }
                }
            }
        }
    }

}
