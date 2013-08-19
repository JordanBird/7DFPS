using UnityEngine;
using System.Collections;
 
public class cscript_player : MonoBehaviour
{
    public int health = 100;
    //public int currentGunAmmo = 28;
    //public int currentGunClip = 7;
	
	public GameObject pistol;
	public GameObject assault;
	public GameObject smg;
	
	public int pistolAmmo = 35;
	public int assaultAmmo = 150;
	public int smgAmmo = 200;
		
	public int pistolClip = 7;
	public int assaultClip = 30;
	public int smgClip = 50;
	
	int activeGun = 0;
	
    public int cubes = 150;
    public int kills = 0;

    public int lastHitCheck = 25;

    GameObject[] allZombies;
    GameObject[] ammoCrates;
    GameObject[] healthCrates;
    GameObject[] allDoors;
    GameObject[] allDestDoors;
   
    // Use this for initialization
    void Start ()
    {
		
    }
   
    // Update is called once per frame
    void Update ()
    {
		if (health <=0)
		{
			DontDestroyOnLoad (this.gameObject);
			Application.LoadLevel ("Game Over");
		}
		
        allZombies = GameObject.FindGameObjectsWithTag ("Zombie");
        ammoCrates = GameObject.FindGameObjectsWithTag ("AmmoBox");
        healthCrates = GameObject.FindGameObjectsWithTag ("HealthBox");
        allDoors = GameObject.FindGameObjectsWithTag ("Door");
        allDestDoors = GameObject.FindGameObjectsWithTag ("Dest Wall");
       
        checkAmmoBox ();
        checkHealthBox();
        checkDoor ();
        checkDestDoor();
       
        if (lastHitCheck == 1)
        {                      
            checkZombies ();
            lastHitCheck = 25;
        }
        else
        	--lastHitCheck;
		
		if (Input.GetKeyDown (KeyCode.Alpha1))
			ChangeGun(0);
		
		if (Input.GetKeyDown (KeyCode.Alpha2))
			ChangeGun(1);
		
		if (Input.GetKeyDown (KeyCode.Alpha3))
			ChangeGun(2);
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
                       
                if (distance < 20.0f && cubes >= 25)
                {
                        GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptAmmo = true;
                       
                        if (Input.GetKeyDown (KeyCode.E))
                        {
                                SpendAmmo (-30);
                                cubes -= 25;
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
                   
                    if (distance < 20.0f && cubes >= 25 && health < 100)
                    {
                            GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptHealth = true;
                           
                            if (Input.GetKeyDown (KeyCode.E))
                            {
                                    health = health + 10;
                                    cubes -= 25;
                                   
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
                           
                                    if (openState == false && cubes >= 100)
                                    {
                                            allDoors [i].animation.Play ("Door Opening");
                                            allDoors [i].GetComponent<cscript_doorchecker> ().isOpen = true;
                                            cubes -= 100;
                                    }
    //                              else
    //                              {
    //                                      allDoors [i].animation.Play ("Door Closing");
    //                                      allDoors [i].GetComponent<cscript_doorchecker> ().isOpen = false;
    //                              }
                            }
                            break;
                    }
                    else
                            GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptDoor = false;
            }
    }
   
    void checkDestDoor ()
    {
            if (allDestDoors == null)
                    return;
           
            Vector3 playerPos = transform.position;
            Vector3 difference = new Vector3 (0, 0, 0);
            float distance = Mathf.Infinity;
           
            for (int i = 0; i < allDestDoors.Length; i++)
            {
                    difference = allDestDoors [i].transform.position - playerPos;
                    distance = difference.sqrMagnitude;
                   
                    if (distance < 10.0f && allDestDoors [i].GetComponent<cscript_destwall> ().damageState < 0f)
                    {
                            GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptDestDoor = true;
                           
                            if (Input.GetKeyDown (KeyCode.E))
                            {
                                    float openState = allDestDoors [i].GetComponent<cscript_destwall> ().damageState;
                           
                                    if (openState < 0f && cubes >= 50)
                                    {
                                            allDestDoors [i].GetComponent<cscript_destwall> ().damageState = 100;
											allDestDoors [i].GetComponent<NavMeshObstacle>().enabled = true;
                                            cubes -= 50;
                                    }
                            }
                            break;
                    }
                    else
                    	GameObject.FindGameObjectWithTag ("Master").GetComponent<GUIDrawer>().promptDestDoor = false;
            }
    }
	
	public int GetCurrentGunClip()
	{
		switch (activeGun)
		{
			case 0:
				return pistolClip;
				break;
			case 1:
				return assaultClip;
				break;
			case 2:
				return smgClip;
				break;
		}
		
		return 0;
	}
	
	public int GetCurrentAmmo()
	{
		switch (activeGun)
		{
			case 0:
				return pistolAmmo;
				break;
			case 1:
				return assaultAmmo;
				break;
			case 2:
				return smgAmmo;
				break;
		}
		
		return 0;
	}
	
	public void SpendAmmo(int s)
	{
		switch (activeGun)
		{
			case 0:
				pistolAmmo -= s;
				break;
			case 1:
				assaultAmmo -= s;
				break;
			case 2:
				smgAmmo -= s;
				break;
		}
	}
	
	public void SpendClip(int s)
	{
		switch (activeGun)
		{
			case 0:
				pistolClip -= s;
				break;
			case 1:
				assaultClip -= s;
				break;
			case 2:
				smgClip -= s;
				break;
		}
	}
	
	public void SetClip(int s)
	{
		switch (activeGun)
		{
			case 0:
				pistolClip = s;
				break;
			case 1:
				assaultClip = s;
				break;
			case 2:
				smgClip = s;
				break;
		}
	}
	
	public void SetAmmo(int s)
	{
		switch (activeGun)
		{
			case 0:
				pistolAmmo = s;
				break;
			case 1:
				assaultAmmo = s;
				break;
			case 2:
				smgAmmo = s;
				break;
		}
	}
	
	public void ChangeGun(int  c)
	{
		pistol.SetActive (false);
		assault.SetActive (false);
		smg.SetActive (false);
		
		switch (c)
		{
			case 0:
				activeGun = 0;
				pistol.SetActive (true);
				break;
			case 1:
				activeGun = 1;
				assault.SetActive (true);
				break;
			case 2:
				activeGun = 2;
				smg.SetActive (true);
				break;
		}
	}
	
	public GameObject GetCurrentGun()
	{
		switch (activeGun)
		{
			case 0:
				return pistol;
				break;
			case 1:
				return assault;
				break;
			case 2:
				return smg;
				break;
		}
		
		return null;
	}
}