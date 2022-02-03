using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject clickedCube;
    public ScoreKeeper scoreKeeper;
    public bool playerGameOver;
    CubeData cubeDataP;
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (!playerGameOver)
        {
            PlayerActions();
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
                        clickedCube.SetActive(false);
                        scoreKeeper.cubesActive--;
                        scoreKeeper.score++;
                        scoreKeeper.scoreTxt.text = scoreKeeper.score.ToString();
                        scoreKeeper.activeCubesTxt.text = scoreKeeper.cubesActive.ToString();
                    }
                }
            }
        }
    }

}
