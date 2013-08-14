using UnityEngine;
using System.Collections;

public class cscript_master : MonoBehaviour 
{
	int wave = 1;

	public int maxZombies = 100;
	public int currentZombies = 0;
	private int spawnedZombies = 0;
	
	// Use this for initialization
	void Start () 
	{
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (spawnedZombies < wave * 10 && currentZombies < maxZombies - 10)
		{
			GameObject[] spawns = GameObject.FindGameObjectsWithTag ("Zombie Spawner");
			
			spawns[Random.Range (0, spawns.Length)].GetComponent<cscript_zombie_spawner>().SpawnZombies (10);
		}
		
		if (currentZombies == 0)
		{
			switch (wave)
			{
				case 1:
					ChangeWorldColour (new Color32(0, 255, 255, 250)); //Cyan
					break;
				case 2:
					ChangeWorldColour (new Color32(0, 0, 255, 250)); //Blue
					break;
				case 3:
					ChangeWorldColour (new Color32(255, 255, 0, 250)); // Yellow
					break;
				case 4:
					ChangeWorldColour (new Color32(255, 0, 0, 250)); // Red
					break;
				case 5:
					ChangeWorldColour (new Color32(128, 0, 0, 128)); // Purple
					break;
				case 6:
					ChangeWorldColour (new Color32(0, 0, 0, 0)); // Black
					break;
			}

			spawnedZombies = 0;
			wave++;
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
            Screen.lockCursor = false;
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 100), string.Format ("Health: {0}\nKills: {1}\nAmmo: {2}", GameObject.FindGameObjectWithTag ("Player").GetComponent<cscript_player>().health, GameObject.FindGameObjectWithTag ("Player").GetComponent<cscript_player>().kills,GameObject.FindGameObjectWithTag ("Player").GetComponent<cscript_player>().ammo));
	}
	
	public void AddZombie()
	{
		currentZombies++;
		spawnedZombies++;
	}
	
	public void RemoveZombie()
	{
		currentZombies--;
	}
	
	public void ChangeWorldColour(Color c)
	{
		foreach (GameObject g in GameObject.FindGameObjectsWithTag ("World"))
		{
			g.renderer.material.color = c;
		}
	}
}
