using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeCounter : MonoBehaviour {

	public int CubesActive;
	public int CubesDestroyed;
	public int CubeLimit;
	public int Score;
	public bool GameIsOver;
	public GameObject Player;
	PlayerControls P_C;
	public Text ScoreText;
	public Text CubeDestroyed;
	public Text Time;
	public Text Cubes_Present;
	// Use this for initialization
	void Start () {
		P_C = Player.GetComponent<PlayerControls> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (CubesActive >= CubeLimit) 
		{
			P_C.GameOver = true;
			GameIsOver = true;
		}
		Score_System ();
	}

	void Score_System()
	{
		if (GameIsOver != true) 
		{
			ScoreIncrementing ();
		}
	}
	void ScoreIncrementing()
	{
		ScoreText.text = "Current Score:" + Score.ToString ();
		CubeDestroyed.text = "Cubes Destroyed:" + CubesDestroyed.ToString (); 
		Cubes_Present.text = "Cubes Present:" + CubesActive.ToString (); 

	}
}
