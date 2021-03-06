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
       
        public bool promptAmmo = false;
        public bool promptHealth = false;
        public bool promptDoor = false;
        public bool promptDestDoor = false;
       
    void OnGUI()
        {
                //Health
                GUI.DrawTexture(new Rect(10, 33, 50, 50), healthSymbol);
                GUI.Label(new Rect(80, 10, 300, 100), GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().health.ToString (), healthText.label);
               
                //Ammo
                GUI.DrawTexture(new Rect(Screen.width - 25,Screen.height - 70, 16, 52), ammoSymbol);
                GUI.Label(new Rect(Screen.width - 200, Screen.height - 80, 300, 100), (GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().GetCurrentGunClip ())+ "/" +(GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().GetCurrentAmmo ()), ammoText.label);
               
                //Cubes - Money
                //GUI.DrawTexture(new Rect(Screen.width - 50, 20, 38, 56), poundSymbol);
                //GUI.Label(new Rect(Screen.width - 300,10, 250, 100), GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().cubes.ToString (), cubeText.label);
                string cubes = GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().cubes.ToString ();
               
                GUI.DrawTexture(new Rect(Screen.width - (75 + cubes.Length * 30), 20, 38, 56), poundSymbol);
                GUI.Label(new Rect(Screen.width - (30 + cubes.Length * 30), 15, 250, 100), cubes, cubeText.label);
               
                //Waves
                GUI.Label(new Rect(0,10, Screen.width, 80), "Wave: " + GameObject.FindGameObjectWithTag("Master").GetComponent<cscript_master>().wave.ToString(), waveText.label);
               
                //asasas
                if (promptAmmo == true)
                        GUI.Label (new Rect(10, 100, Screen.width, 50), "Press 'E' to Buy 30 Ammo for 25 Cubes", waveText.label);
               
                if (promptHealth == true)
                        GUI.Label (new Rect(10, 100, Screen.width, 50), "Press 'E' to Buy 10 Health for 25 Cubes", waveText.label);
               
                if (promptDoor == true)
                        GUI.Label (new Rect(10, 100, Screen.width, 50), "Press 'E' to Open the Door for 100 Cubes", waveText.label);
               
                if (promptDestDoor == true)
                        GUI.Label (new Rect(10, 100, Screen.width, 50), "Press 'E' to Fix the Wall for 50 Cubes", waveText.label);
        }
}