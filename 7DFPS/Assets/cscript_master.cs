using UnityEngine;
using System.Collections;

public class cscript_master : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 100), string.Format ("Health: {0}\nKills: {1}", GameObject.FindGameObjectWithTag ("Player").GetComponent<cscript_player>().health, GameObject.FindGameObjectWithTag ("Player").GetComponent<cscript_player>().kills));
	}
}
