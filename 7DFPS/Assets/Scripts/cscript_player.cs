using UnityEngine;
using System.Collections;

public class cscript_player : MonoBehaviour
{
	
	public int health = 100;
	public int ammo = 30;
	public int clip = 7;
	public int cubes = 150;
	public int kills = 0;
	public int lastHitCheck = 25;
	GameObject[] allZombies;
	GameObject ammoCrate;
	GameObject healthCrate;
	GameObject[] allDoors;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{		
		allZombies = GameObject.FindGameObjectsWithTag ("Zombie");
		ammoCrate = GameObject.FindGameObjectWithTag ("AmmoBox");
		healthCrate = GameObject.FindGameObjectWithTag ("HealthBox");
		allDoors = GameObject.FindGameObjectsWithTag ("Door");
		
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			checkAmmoBox ();
			checkHealthBox();
			checkDoor ();
		}
		
		if (lastHitCheck == 1) 
		{			
			checkZombies ();
			lastHitCheck = 25;
		}
		else
			--lastHitCheck;
	}
	
	void checkZombies ()
	{
		if (allZombies == null)
			return;
		
		Vector3 playerPos = transform.position;
		Vector3 difference = new Vector3 (0, 0, 0);
		float distance = Mathf.Infinity;
		
		
		for (int i = 0; i< allZombies.Length; i++) {
			GameObject currentCheck = allZombies [i];
			difference = currentCheck.transform.position - playerPos;
			distance = difference.sqrMagnitude;
			
			if (distance < 5.0f) {
				health--;
			}
		}
	}
	
	void checkAmmoBox ()
	{
		if (ammoCrate == null)
			return;
		
		Vector3 playerPos = transform.position;
		Vector3 difference = new Vector3 (0, 0, 0);
		float distance = Mathf.Infinity;
		
		difference = ammoCrate.transform.position - playerPos;
		distance = difference.sqrMagnitude;
			
		if (distance < 20.0f && cubes >= 50) 
		{
			ammo = ammo + 30;
			cubes -= 50;
		}
	}
	
	void checkHealthBox ()
	{
		if (healthCrate == null)
			return;
		
		Vector3 playerPos = transform.position;
		Vector3 difference = new Vector3 (0, 0, 0);
		float distance = Mathf.Infinity;
		
		difference = healthCrate.transform.position - playerPos;
		distance = difference.sqrMagnitude;
			
		if (distance < 20.0f && cubes >= 50 && health < 100) 
		{
			health = health + 10;
			cubes -= 50;
			
			if (health > 100)
				health = 100;
		}
	}
	
	void checkDoor ()
	{
		if (allDoors == null)
			return;
		
		Vector3 playerPos = transform.position;
		Vector3 difference = new Vector3 (0, 0, 0);
		float distance = Mathf.Infinity;
		
		for (int i = 0; i < allDoors.Length; i++) 
		{
			difference = allDoors [i].transform.position - playerPos;
			distance = difference.sqrMagnitude;
			
			if (distance < 10.0f) 
			{
				bool openState = allDoors [i].GetComponent<cscript_doorchecker> ().isOpen;
			
				if (openState == false && cubes >= 150) 
				{
					allDoors [i].animation.Play ("Door Opening");
					allDoors [i].GetComponent<cscript_doorchecker> ().isOpen = true;
					cubes -= 150;
				} 
//				else 
//				{
//					allDoors [i].animation.Play ("Door Closing");
//					allDoors [i].GetComponent<cscript_doorchecker> ().isOpen = false;
//				}
			}
		}
	}
}
