using UnityEngine;
using System.Collections;

public class Niveles : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void OnMouseDown() {
		if(transform.name=="Nivel1") {
        	Application.LoadLevel("Nivel1");
		}
		if(transform.name=="Nivel2") {
        	Application.LoadLevel("Nivel2");
		}
		if(transform.name=="Nivel3") {
        	Application.LoadLevel("Nivel3");
		}
		if(transform.name=="Nivel4") {
        	Application.LoadLevel("Nivel4");
		}
    }
}
