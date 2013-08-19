using UnityEngine;
using System.Collections;

public class cscript_destwall : MonoBehaviour 
{
	public bool isDest = false;
 	public float damageState = 100;
	
 	// Update is called once per frame
 	void Update () 
	{
  		if (damageState < 0)
  		{
   			GetComponent<MeshRenderer>().enabled = false;
  		}
  		else
  		{
			GetComponent<MeshRenderer>().enabled = true;
  		}
 	}
}