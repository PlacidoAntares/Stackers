using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour {

	public Material [] Obj_Color;
	public int Mat_ID;
	public int Grey_R_Rate;
	public MeshRenderer Mesh_R;
	public string[] Colors;
	//
	public GameObject [] Cube_Contacts; //0(Left),1(right),2(Up),3(Down)
	Ray Ray_Left;
	Ray Ray_Right;
	Ray Ray_Up;
	Ray Ray_Down;
	public Vector3[] D_CubeRays;
	//
	public bool MeshTagUpdate;
	public bool IsMoving;
	//
	public CubeBehavior C_B;
	public GameObject Active_Cube_Counter;
	public GameObject[] Greys;
	public CubeCounter A_C_C;
	//
	private Transform ObjTransform;
	// Use this for initialization
	void Start () {
		ObjTransform = GetComponent<Transform> ();
		IsMoving = true;
		Cube_Contacts = new GameObject[4];
		MeshTagUpdate = false;
		Mesh_R = GetComponent<MeshRenderer> ();
		Mat_ID = Random.Range (0, Obj_Color.Length);
		Mesh_R.material = Obj_Color [Mat_ID];
		this.gameObject.tag = Colors [Mat_ID];
		Active_Cube_Counter = GameObject.Find ("Active_Cube_Counter");
		A_C_C = Active_Cube_Counter.GetComponent<CubeCounter> ();
		InvokeRepeating ("GreyRandoms", 0, Grey_R_Rate);
	}
	
	// Update is called once per frame
	void Update () {
		if (MeshTagUpdate == true) {
			Mesh_R = GetComponent<MeshRenderer> ();
			Mat_ID = Random.Range (0, Obj_Color.Length);
			Mesh_R.material = Obj_Color [Mat_ID];
			this.gameObject.tag = Colors [Mat_ID];
			MeshTagUpdate = false;
		}
		Draw_Rays ();
		C_Raycasts ();
		if ((A_C_C.GameIsOver != true)&&(IsMoving != true)) 
		{
			Cube_Reactions ();
		}
			}

	void Draw_Rays()
	{
		Ray_Left = new Ray (this.gameObject.transform.position,(D_CubeRays[0]));
		Debug.DrawRay(this.gameObject.transform.position,(D_CubeRays[0]),Color.green);
		Ray_Right = new Ray (this.gameObject.transform.position,D_CubeRays[1]);
		Debug.DrawRay(this.gameObject.transform.position, D_CubeRays[1],Color.green);
		Ray_Up = new Ray (this.gameObject.transform.position,D_CubeRays[2]);
		Debug.DrawRay(this.gameObject.transform.position,D_CubeRays[2],Color.green);
		Ray_Down = new Ray (this.gameObject.transform.position, D_CubeRays[3]);
		Debug.DrawRay(this.gameObject.transform.position,D_CubeRays[3],Color.green);

	}
	void C_Raycasts()
	{
		RaycastHit Left_Ray;
		if (Physics.Raycast (Ray_Left, out Left_Ray,1.0f)) 
		{
			if (Left_Ray.collider != null) 
			{
				if (IsMoving != true) 
				{
					Cube_Contacts [0] = Left_Ray.collider.gameObject;
				}
			}
		}
		RaycastHit Right_Ray;
		if (Physics.Raycast (Ray_Right, out Right_Ray,1.0f)) 
		{
			if (Right_Ray.collider != null) 
			{
				if (IsMoving != true) 
				{
					Cube_Contacts [1] = Right_Ray.collider.gameObject;
				}
					
			}
		}
		RaycastHit Up_Ray;
		if (Physics.Raycast (Ray_Up, out Up_Ray,0.5f)) 
		{
			if (Up_Ray.collider != null) 
			{
				if (IsMoving != true) 
				{
					Cube_Contacts [2] = Up_Ray.collider.gameObject;
				}
			}
		}
		RaycastHit Down_Ray;
		if (Physics.Raycast (Ray_Down, out Down_Ray,0.5f)) 
		{
			if (Down_Ray.collider != null) {
				if (IsMoving != true) {
					if ((Down_Ray.collider.gameObject.tag != "Grey") || (Down_Ray.collider.gameObject.tag != "Ground")) {
						Cube_Contacts [3] = Down_Ray.collider.gameObject;
					}
				}
			

			} else if (Down_Ray.collider == null) 
			{
				IsMoving = true;
			}
		}
	}

	void Cube_Reactions()
	{
		//0(Left),1(right),2(Up),3(Down)
		//Reds can only clear when 3 of them at a side.
		if ((Cube_Contacts [0] != null) && (Cube_Contacts [1] != null)) {
			
			if ((Cube_Contacts [0].tag == "Red") && (Cube_Contacts [1].tag == "Red") && (this.gameObject.tag == "Red")) {
				C_B = Cube_Contacts [0].GetComponent<CubeBehavior> ();
				Cube_Contacts [0].SetActive (false);
				C_B.MeshTagUpdate = true;
				C_B.IsMoving = true;
				//
				C_B = Cube_Contacts [1].GetComponent<CubeBehavior> ();
				Cube_Contacts [1].SetActive (false);
				C_B.MeshTagUpdate = true;
				C_B.IsMoving = true;
				//
				this.gameObject.SetActive (false);
				MeshTagUpdate = true;
				IsMoving = true;
				A_C_C.CubesActive -= 3;
				A_C_C.Score += 100;
				A_C_C.CubesDestroyed += 3;

			}
		}
		//Blues can only clear when 3 of them stacked on top of each other
		if ((Cube_Contacts [2] != null) && (Cube_Contacts [3] != null)) {

			if ((Cube_Contacts [2].tag == "Blue") && (Cube_Contacts [3].tag == "Blue") && (this.gameObject.tag == "Blue")) {
				C_B = Cube_Contacts [2].GetComponent<CubeBehavior> ();
				Cube_Contacts [2].SetActive (false);
				C_B.MeshTagUpdate = true;
				C_B.IsMoving = true;
				//
				C_B = Cube_Contacts [3].GetComponent<CubeBehavior> ();
				Cube_Contacts [3].SetActive (false);
				C_B.MeshTagUpdate = true;
				C_B.IsMoving = true;
				//
				this.gameObject.SetActive (false);
				MeshTagUpdate = true;
				IsMoving = true;
				A_C_C.CubesActive -= 3;
				A_C_C.Score += 200;
				A_C_C.CubesDestroyed += 3;
			} else if ((Cube_Contacts [3].tag == "Ground") && (this.gameObject.tag == "Grey")) 
			{
				Greys = GameObject.FindGameObjectsWithTag ("Grey");
				for (int Grey_Count = 0; Grey_Count < Greys.Length; Grey_Count++) 
				{
					C_B = Greys [Grey_Count].GetComponent<CubeBehavior> ();
					C_B.MeshTagUpdate = true;
				}
				this.gameObject.SetActive (false);
				MeshTagUpdate = true;
				IsMoving = true;
			}
		}



	}
	void GreyRandoms()
	{
		//Greys Change randomly every x seconds
		if ((this.gameObject.tag == "Grey") && (A_C_C.GameIsOver != true))
		{
			MeshTagUpdate = true;
		}
	}
	void OnCollisionEnter(Collision collision) {
		if (collision.relativeVelocity.magnitude > 2) 
		{
			IsMoving = false;

		}
	}
}

