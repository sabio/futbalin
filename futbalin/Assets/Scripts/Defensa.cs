using UnityEngine;
using System.Collections;

//Debug.Log ("vaHaciaArriba="+vaHaciaArriba+"  transform.position.z="+transform.position.z);
public class Defensa : MonoBehaviour {
	
	public int nivelDefensa = 0;
	public float limiteSuperior = 0;
	public float limiteInferior = -22;
	
	//Exclusivo para nivel de defensa 1 y 2
	public bool vaHaciaArriba = true;
	

	
	
	//Exclusivo para nivel de defensa 1
	int velocidadNivel1 = 3; 
	
	//Exclusivo para nivel de defensa 2
	int velocidadNivel2 = 15; 
	
	//Exclusivo para nivel de defensa 3
	int velocidadNivel3 = 7; 
	
	// Update is called once per frame
	void Update () {
		switch(nivelDefensa){
			case 1:
				nivelDefensa1();
				break;
			case 2:
				nivelDefensa2();
				break;
			case 3:
				nivelDefensa3();
				break;
			
		}
	}
	
	void nivelDefensa1(){
		if(vaHaciaArriba){
			if(transform.position.z >= limiteSuperior){
				vaHaciaArriba = false;	
			}else{
				transform.Translate(Vector3.forward * Time.deltaTime*velocidadNivel1);
			}
		}else{
			if(transform.position.z <= limiteInferior){
				vaHaciaArriba = true;	
			}else{
				transform.Translate(Vector3.back * Time.deltaTime*velocidadNivel1);
			}	
		}
	}
	
	
	void nivelDefensa2(){
		if(vaHaciaArriba){
			if(transform.position.z >= limiteSuperior){
				vaHaciaArriba = false;	
			}else{
				transform.Translate(Vector3.forward * Time.deltaTime*velocidadNivel2);
			}
		}else{
			if(transform.position.z <= limiteInferior){
				vaHaciaArriba = true;	
			}else{
				transform.Translate(Vector3.back * Time.deltaTime*velocidadNivel2);
			}	
		}
	}
	
	void nivelDefensa3(){
		GameObject pelota = GameObject.Find("pelota") as GameObject;
		if(pelota != null)
		{
			float posicionPelota = pelota.transform.position.z;
			
			if((transform.position.z-1) >= posicionPelota){
				transform.Translate(Vector3.back * Time.deltaTime*velocidadNivel3);
			}else if((transform.position.z+1) < posicionPelota){
				transform.Translate(Vector3.forward * Time.deltaTime*velocidadNivel3);
			}
		}
	}
	
}
