using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Spawning : MonoBehaviour {

	public bool Is_Spawning;
	public float spawnRepeat;
	public float spawnTime;
	public GameObject Cube_Pooler;
	public GameObject Cube_Counter;
	public CubeCounter Active_Counter;
	GenericPooler pool_script;
	// Use this for initialization
	void Start () 
	{
		Is_Spawning = true;
		Cube_Counter = GameObject.Find ("Active_Cube_Counter");
		Active_Counter = Cube_Counter.GetComponent<CubeCounter> ();
		InvokeRepeating ("Spawn_Cube", spawnTime, spawnRepeat);
	}
	void Spawn_Cube()
	{
		if (Is_Spawning == true) {
			pool_script = Cube_Pooler.GetComponent<GenericPooler> ();
			GameObject obj = pool_script.GetPooledObject ();
			//GameObject obj = GenericPooler.current.GetPooledObject();
			if (obj == null)
				return;
			obj.transform.position = transform.position;
			obj.transform.rotation = transform.rotation;
			obj.SetActive (true);
			Active_Counter.CubesActive += 1;
		}
	}
}
