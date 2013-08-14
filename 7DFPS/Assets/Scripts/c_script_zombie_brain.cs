﻿using UnityEngine;
using System.Collections;

public class c_script_zombie_brain : MonoBehaviour
{
	
	private NavMeshAgent agent;
	private GameObject player;
	public GameObject[] torso;
	public GameObject[] legs;
	private int health = 100;
	
	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		GameObject.FindGameObjectWithTag ("Master").GetComponent<cscript_master>().AddZombie ();
		
		Color cTorso = new Color (Random.Range (0, 100) / 100, Random.Range (0, 250) / 100, Random.Range (0, 250) / 100, 1);
		Color cLegs = new Color (Random.Range (0, 250) / 100, Random.Range (0, 250) / 100, Random.Range (0, 250) / 100, 1);
		
		foreach (GameObject g in torso) {
			g.renderer.material.color = cTorso;
		}
		
		foreach (GameObject g in legs) {
			g.renderer.material.color = cLegs;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player != null)
			agent.SetDestination (player.transform.position);
	}
	
	void OnTriggerEnter (Collider collision)
	{
		if (collision.gameObject.tag == "Bullet") {
			Debug.Log ("Shot");
			Destroy (collision.gameObject);
			Destroy (gameObject);
		}
	}
	
	public void Damage (string part, int damage)
	{
		switch (part) {
		case "Head":
			health -= damage;
			break;
		case "Arm":
			health -= damage;
			break;
		case "Leg":
			health -= damage;
			break;
		case "Body":
			health -= damage;
			break;
		}
		
		if (health <= 0)
			Die (part);
	}
	
	public void Die (string part)
	{
		GameObject.FindGameObjectWithTag ("Master").GetComponent<cscript_master>().RemoveZombie();
		GameObject.FindGameObjectWithTag ("Player").GetComponent<cscript_player> ().kills++;
   		
		if (part != "Head")
			GameObject.FindGameObjectWithTag ("Player").GetComponent<cscript_player> ().cubes += 5;
		else
			GameObject.FindGameObjectWithTag ("Player").GetComponent<cscript_player> ().cubes += 6;
		
		Debug.Log ("Die");
		Destroy (this.GetComponent<Animation> ());
		Destroy (this.GetComponent<NavMeshAgent> ());
  
		ProcessDeadLimb (this.transform.FindChild ("Left Arm").transform.FindChild ("arm").transform);
		ProcessDeadLimb (this.transform.FindChild ("Right Arm").transform.FindChild ("arm").transform);
		ProcessDeadLimb (this.transform.FindChild ("Left Leg").transform.FindChild ("leg").transform);
		ProcessDeadLimb (this.transform.FindChild ("Right Leg").transform.FindChild ("leg").transform);
		ProcessDeadLimb (this.transform.FindChild ("body").transform);
		ProcessDeadLimb (this.transform.FindChild ("head").transform);
  
		ProcessDeadContainer (this.transform);
  
		Destroy (this);
	}
 
	public void ProcessDeadLimb (Transform t)
	{
		t.gameObject.GetComponent<BoxCollider> ().isTrigger = false;
		t.gameObject.AddComponent<Rigidbody> ();
  
		t.name = "Gib";
	}
 
	public void ProcessDeadContainer (Transform t)
	{	
		t.gameObject.AddComponent<TimedObjectDestructor> (); 
		t.name = "Gib";
		t.tag = "Dead Zombie";
	}
}
