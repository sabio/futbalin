using UnityEngine;
using System.Collections;

public class PantallaPrincipal : MonoBehaviour {
	public GUISkin miPiel;
	public AudioClip botonazo;
	public Texture top5;
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		GUI.skin = miPiel;
		GUI.Box(new Rect(Screen.width/2-200,Screen.height/2-150,400,300), "");
		
		GUI.Label(new Rect(Screen.width/2-120,Screen.height/2-140,400,50), "MENU PRINCIPAL");
			
		
		if(GUI.Button(new Rect(Screen.width/2-70,Screen.height/2-80,140,30), "Modo Arcade")) {
			//AudioSource.PlayClipAtPoint(botonazo, Camera.main.transform.position);
			audio.PlayOneShot(botonazo);
			Application.LoadLevel(1);
		}
		
		if(GUI.Button(new Rect(Screen.width/2-70,Screen.height/2-35,140,30), "Modo Niveles")) {
			//AudioSource.PlayClipAtPoint(botonazo, Camera.main.transform.position);
			audio.PlayOneShot(botonazo);
			Application.LoadLevel(4);
		}
		
		if(GUI.Button(new Rect(Screen.width/2-40,Screen.height/2+10,80,80), top5)) {
			audio.PlayOneShot(botonazo);
			Application.LoadLevel(3);
		}
		
		
	}
}
