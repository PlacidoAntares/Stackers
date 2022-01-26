using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior2D : MonoBehaviour {

	public Sprite [] Box_Sprites;
	public string[] Box_Tags;
	public int Box_ID;
	public int Grey_2D_R_Rate;
	public float Box_Size;
	public GameObject[] Box_List;
	SpriteRenderer Sprite_R;
	Ray2D Ray_2DLeft;
	Ray2D Ray_2DRight;
	Ray2D Ray_2DUp;
	Ray2D Ray_2DDown;
	public Vector2[] CubeRays;
	//
	public bool UpdateRenderTag;
	public bool IsMoving;
	//
	public CubeBehavior2D C_B;
	public GameObject Active_Cube_Counter;
	public CubeCounter A_C_C;
	//
	private Transform ObjTransform;
	// Use this for initialization
	void Start () {
		ObjTransform = GetComponent< Transform> ();
		IsMoving = true;
		Box_List = new GameObject[4];
		UpdateRenderTag = false;
		Sprite_R = GetComponent<SpriteRenderer> ();
		Box_ID = Random.Range (0, Box_Sprites.Length);
		Sprite_R.sprite = Box_Sprites [Box_ID];
		this.gameObject.tag = Box_Tags [Box_ID];
		Active_Cube_Counter = GameObject.Find ("Active_Cube_Counter");
		A_C_C = Active_Cube_Counter.GetComponent<CubeCounter> ();
		InvokeRepeating ("GreyRandom", 0, Grey_2D_R_Rate);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (UpdateRenderTag == true) {
			Sprite_R = GetComponent<SpriteRenderer> ();
			Box_ID = Random.Range (0, Box_Sprites.Length);
			Sprite_R.sprite = Box_Sprites [Box_ID];
			this.gameObject.tag = Box_Tags [Box_ID];
			UpdateRenderTag = false;
		}

		Draw_2DRays ();
		C_2DRaycasts();
				if ((A_C_C.GameIsOver != true)&&(IsMoving != true)) 
				{
					Cube2D_Reactions ();
				}
		}
	void Draw_2DRays()
	{
		Ray_2DLeft = new Ray2D(this.gameObject.transform.position,(CubeRays[0]));
		Debug.DrawRay(this.gameObject.transform.position,CubeRays[0],Color.green);
		Ray_2DRight = new Ray2D(this.gameObject.transform.position,(CubeRays[1]));
		//Debug.DrawRay(this.gameObject.transform.position,(CubeRays[1]),Color.green);
		Ray_2DUp = new Ray2D(this.gameObject.transform.position,(CubeRays[2]));
		//Debug.DrawRay(this.gameObject.transform.position,(CubeRays[2]),Color.green);
		Ray_2DDown = new Ray2D(this.gameObject.transform.position,(CubeRays[3]));
		Debug.DrawRay(this.gameObject.transform.position,(CubeRays[3]),Color.green);
	}
	void C_2DRaycasts()
	{
		RaycastHit2D Left_2DRay = Physics2D.Raycast(ObjTransform.position,CubeRays[0],1.0f);
		if (Left_2DRay.rigidbody != null) 
			{
				if (IsMoving != true) 
				{
				Box_List [0] = Left_2DRay.rigidbody.gameObject;
				}
			}
		RaycastHit2D Right_2DRay = Physics2D.Raycast(ObjTransform.position,CubeRays[1],1.0f);
		if (Right_2DRay.rigidbody != null) 
		{
			if (IsMoving != true) 
			{
				Box_List [1] = Right_2DRay.rigidbody.gameObject;
			}
		}
		//
		RaycastHit2D Up_2DRay = Physics2D.Raycast(ObjTransform.position,CubeRays[2],1.0f);
		if (Up_2DRay.rigidbody != null) 
		{
			if (IsMoving != true) 
			{
				Box_List [2] = Up_2DRay.rigidbody.gameObject;
			}
		}
		//
		RaycastHit2D Down_2DRay = Physics2D.Raycast(ObjTransform.position,CubeRays[3],1.0f);
		if (Down_2DRay.collider != null) 
		{
			Box_List [3] = Down_2DRay.collider.gameObject;
			IsMoving = false;
		}

	}
	void Cube2D_Reactions()
	{
	//0(Left),1(right),2(Up),3(Down)
	//Reds can only clear when 3 of them at a side.
		if ((Box_List [0] != null) && (Box_List [1] != null)) 
		{
			//if ((Cube_Contacts [0].tag == "Red") && (Cube_Contacts [1].tag == "Red") && (this.gameObject.tag == "Red")) 
			if ((Box_List [0].tag == "Red") && (Box_List [1].tag == "Red") && (this.gameObject.tag == "Red")) 
			{
				C_B = Box_List [0].GetComponent<CubeBehavior2D> ();
				Box_List [0].SetActive (false);
				C_B.UpdateRenderTag = true;
				C_B.IsMoving = true;
				//
				C_B = Box_List [1].GetComponent<CubeBehavior2D> ();
				Box_List [1].SetActive (false);
				C_B.UpdateRenderTag = true;
				C_B.IsMoving = true;
				//
				this.gameObject.SetActive (false);
				UpdateRenderTag = true;
				IsMoving = true;
				A_C_C.CubesActive -= 3;
				A_C_C.Score += 3;
				A_C_C.CubesDestroyed += 3;
			}
		}
	//Blues can only clear when 3 of them stacked on top of each other
		if ((Box_List [2] != null) && (Box_List [3] != null)) {
			if ((Box_List [2].tag == "Blue") && (Box_List [3].tag == "Blue") && (this.gameObject.tag == "Blue")) {
				C_B = Box_List [2].GetComponent<CubeBehavior2D> ();
				Box_List [2].SetActive (false);
				C_B.UpdateRenderTag = true;
				C_B.IsMoving = true;
				//
				C_B = Box_List [3].GetComponent<CubeBehavior2D> ();
				Box_List [3].SetActive (false);
				C_B.UpdateRenderTag = true;
				C_B.IsMoving = true;
				//
				this.gameObject.SetActive (false);
				UpdateRenderTag = true;
				IsMoving = true;
				A_C_C.CubesActive -= 3;
				A_C_C.Score += 3;
				A_C_C.CubesDestroyed += 3;
			}
		} 

	}

	void GreyRandom()
	{
		if((this.gameObject.tag == "Grey") && (A_C_C.GameIsOver != true)&&(IsMoving != true)) 
		{
			UpdateRenderTag = true;
		}
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.relativeVelocity.magnitude > 2) 
		{
			IsMoving = false;

		}
	}
}