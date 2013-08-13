using UnityEngine;
using System.Collections;

public class cscript_bullet : MonoBehaviour {
	
	private float timer = 300;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;
		
		if (timer <= 0)
			Destroy (gameObject);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		Destroy (gameObject);
	}
}
