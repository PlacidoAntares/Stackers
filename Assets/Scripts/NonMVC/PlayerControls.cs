using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
	private GameObject Clicked_Cube;
	public bool GameOver;
	public GameObject Active_Cube_Counter;
	CubeCounter A_C_C;
	// Use this for initialization
	void Start () {
		GameOver = false;
		Active_Cube_Counter = GameObject.Find ("Active_Cube_Counter");
		A_C_C = Active_Cube_Counter.GetComponent<CubeCounter> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameOver != true) 
		{
			Player_Actions ();
			//Player2D_Actions();
		}
	}

	void Player2D_Actions()
	{
		
	}
	void Player_Actions()
	{
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 5.0f;
			Vector2 v = Camera.main.ScreenToWorldPoint (mousePos);
			Collider2D[] col = Physics2D.OverlapPointAll (v);
			if (col.Length > 0) {
				foreach (Collider2D c in col) {
					if (c.gameObject.tag == "White") {
						Clicked_Cube = c.gameObject;
						Clicked_Cube.SetActive (false);
						CubeBehavior2D C_Behavior = Clicked_Cube.GetComponent<CubeBehavior2D> ();
						C_Behavior.Box_ID = Random.Range (0, C_Behavior.Box_Sprites.Length);
						C_Behavior.UpdateRenderTag = true;
						C_Behavior.IsMoving = true;
						C_Behavior.Box_List [3] = null;
						A_C_C.CubesActive -= 1;
					}
				}
			}
		}
	}
}

//if ((hit.collider != null)&&(hit.collider.tag == "White")) 
//{
//	Clicked_Cube = hit.collider.gameObject;
//	Clicked_Cube.SetActive (false);
//	CubeBehavior2D C_Behavior = Clicked_Cube.GetComponent<CubeBehavior2D> ();
//	C_Behavior.Box_ID = Random.Range (0, C_Behavior.Box_Sprites.Length);
//	C_Behavior.UpdateRenderTag = true;
//	C_Behavior.IsMoving = true;
//	C_Behavior.Box_List [3] = null;
//	A_C_C.CubesActive -= 1;
//}