using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPooler : MonoBehaviour {

	public static GenericPooler current;
	public GameObject pooledObject;
	public int Pool_Amt;
	public bool willGrow;
	List<GameObject> PooledObjs;

	// Use this for initialization
	void Awake()
	{
		current = this;
	}
	void Start () {
		PooledObjs = new List<GameObject> ();
		for (int i = 0; i < Pool_Amt; i++) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObject);
			obj.SetActive (false);
			PooledObjs.Add (obj);
		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < PooledObjs.Count; i++) 
		{
			if (!PooledObjs [i].activeInHierarchy) 
			{
				return PooledObjs [i];
			}
		}
		if (willGrow) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObject);
			PooledObjs.Add (obj);
			return obj;
		}
		return null;
	}
	

}
