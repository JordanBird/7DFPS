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
	GameObject[] ammoCrates;
	GameObject[] healthCrates;
	GameObject[] allDoors;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{		
		allZombies = GameObject.FindGameObjectsWithTag ("Zombie");
		ammoCrates = GameObject.FindGameObjectsWithTag ("AmmoBox");
		healthCrates = GameObject.FindGameObjectsWithTag ("HealthBox");
		allDoors = GameObject.FindGameObjectsWithTag ("Door");
		
		checkAmmoBox ();
		checkHealthBox();
		checkDoor ();
		
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
		if (ammoCrates == null)
			return;
		
		Vector3 playerPos = transform.position;
		Vector3 difference = new Vector3 (0, 0, 0);
		float distance = Mathf.Infinity;
		
		for (int i = 0; i < ammoCrates.Length; i++) 
		{
			difference = ammoCrates[i].transform.position - playerPos;
			distance = difference.sqrMagnitude;
				
			if (distance < 20.0f && cubes >= 50) 
			{
				GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptAmmo = true;
				
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					ammo = ammo + 30;
					cubes -= 50;
				}
				
				break;
			}
			else
				GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptAmmo = false;
		}
		
	}
	
	void checkHealthBox ()
	{
		
		if (healthCrates == null)
			return;
		
		Vector3 playerPos = transform.position;
		Vector3 difference = new Vector3 (0, 0, 0);
		float distance = Mathf.Infinity;

		for (int i = 0; i < healthCrates.Length; i++) 
		{
			difference = healthCrates[i].transform.position - playerPos;
			distance = difference.sqrMagnitude;
			
			if (distance < 20.0f && cubes >= 50 && health < 100) 
			{
				GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptHealth = true;
				
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					health = health + 10;
					cubes -= 50;
					
					if (health > 100)
						health = 100;
				}
				
				break;
			}
			else
				GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptHealth = false;
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
			
			if (distance < 10.0f && allDoors [i].GetComponent<cscript_doorchecker> ().isOpen == false) 
			{
				GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptDoor = true;
				
				if (Input.GetKeyDown (KeyCode.E)) 
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
				break;
			}
			else
				GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptDoor = false;
		}
	}
	
	void OnGUI()
	{
		
	}
}
