using UnityEngine;
using System.Collections;

public class cscript_zombie_spawner : MonoBehaviour {
	
	public bool useTimer = false;
	public float spawnTime = 10;
	private float timerTime;
	
	public int zombiesToSpawn = 10;
	
	public GameObject zombiePrefab;
	
	// Update is called once per frame
	void Update () 
	{
		if (useTimer == true)
		{
			timerTime -= Time.deltaTime;	
		}
		
		if (timerTime <= 0)
		{
			for (int i = 0; i < zombiesToSpawn; i++)
			{
				Instantiate(zombiePrefab, this.transform.position, Quaternion.identity);
			}
			
			timerTime = spawnTime;
		}
	}
	
	public void SpawnZombies(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			Instantiate(zombiePrefab, this.transform.position, Quaternion.identity);
		}	
	}
}
