using UnityEngine;
using System.Collections;

public class cscript_game_over : MonoBehaviour {
	
	public GUISkin skin;
	
	public string kills = "";
	public string cubes = "";
	
	// Use this for initialization
	void Start () 
	{
		Screen.lockCursor = false;
		
		kills = GameObject.FindGameObjectWithTag ("Player").GetComponent<cscript_player>().kills.ToString ();
		cubes = GameObject.FindGameObjectWithTag ("Player").GetComponent<cscript_player>().cubes.ToString ();
		
		GameObject.Destroy (GameObject.FindGameObjectWithTag ("Player"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		GUI.Label (new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Kills: " + kills + "\n\nCubes: " + cubes, skin.label);
		
		if (GUI.Button (new Rect(100, 250, 100, 30), "Try Again"))
		{
			Application.LoadLevel ("Test Scene");
		}
		
		if (GUI.Button (new Rect(Screen.width - 200, 250, 100, 30), "Exit"))
		{
			Application.Quit ();
		}
	}
}
