using UnityEngine;
using System.Collections;
 
public class pauseMenu : MonoBehaviour
{
        public GUISkin myskin;
        private Rect windowRect;
        private bool paused = false, waited = true;
       
        private void Start()
        {
                windowRect = new Rect(Screen.width/2 - 150, Screen.height/2 - 150, 300, 300);
        }
       
        private void waiting()
        {
                waited = true;
        }
 
        private void Update()
        {               
            if(waited)
            {
                    if(Input.GetKey(KeyCode.Escape) || Input.GetKey (KeyCode.P))
                    {

                            if(paused)
                            {
                                    paused = false;
                            }
                            else
                            {
                                    paused = true;
                            }
                           
                            waited = false;
                            Invoke("waiting",0.3f);
                    }
            }
           
            if(paused)
            {
                    Time.timeScale=0;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>().enabled = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MouseLook>().enabled = false;
			
					GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().GetCurrentGun ().GetComponent<cscript_gun>().enabled = false;
			
                    Camera.mainCamera.GetComponent<MouseLook>().enabled = false;
            }
            else
            {
                    Time.timeScale = 1;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>().enabled = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MouseLook>().enabled = true;
			
					GameObject.FindGameObjectWithTag("Player").GetComponent<cscript_player>().GetCurrentGun ().GetComponent<cscript_gun>().enabled = true;
			
                    Camera.mainCamera.GetComponent<MouseLook>().enabled = true;
            }
        }
       
        private void OnGUI()
        {
                if(paused)
                {
                        windowRect = GUI.Window (0, windowRect, windowFunc, "Pause Menu");
                }
                               
        }
       
        private void windowFunc(int id)
        {
                if(GUILayout.Button("Resume"))
                {
                        paused = false;
                }
                if(GUILayout.Button ("Main Menu"))
                {
                        Application.LoadLevel (0);     
                }
                if(GUILayout.Button("Quit"))
                {
                        Application.Quit();
                }
        }
}