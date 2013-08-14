using UnityEngine;
using System.Collections;

public class GUIDrawer : MonoBehaviour 
{
	//health
	//clip
	//wave
	//cubes
	//ammo is reserve ammo
	public Texture healthSymbol;
	public Texture ammoSymbol;
	public Texture poundSymbol;
	public GUISkin healthText;
	public GUISkin ammoText;
	public GUISkin cubeText;
	public GUISkin waveText;
		
	public bool drawGUI;
		
    void OnGUI() 
	{
		//Health
		GUI.DrawTexture(new Rect(10, 33, 50, 50), healthSymbol);
		GUI.Label(new Rect(80, 10, 300, 100), GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().health.ToString (), healthText.label);
		
		//Ammo
		GUI.DrawTexture(new Rect(Screen.width - 25,Screen.height - 70, 16, 52), ammoSymbol);
		GUI.Label(new Rect(Screen.width - 200, Screen.height - 80, 300, 100), (GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().clip)+ "/" +(GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().ammo), ammoText.label);
		
		//Cubes - Money
		//GUI.DrawTexture(new Rect(Screen.width - 50, 20, 38, 56), poundSymbol);
		//GUI.Label(new Rect(Screen.width - 300,10, 250, 100), GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().cubes.ToString (), cubeText.label);
		string cubes = GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().cubes.ToString ();
		
		GUI.DrawTexture(new Rect(Screen.width - (75 + cubes.Length * 30), 20, 38, 56), poundSymbol);
		GUI.Label(new Rect(Screen.width - (30 + cubes.Length * 30), 15, 250, 100), cubes, cubeText.label);
		
		//Waves
		GUI.Label(new Rect(0,10, Screen.width, 80), "Wave: " + GameObject.FindGameObjectWithTag("Master").GetComponent<cscript_master>().wave.ToString(), waveText.label);
	}
}
