using UnityEngine;
using System.Collections;

public class GuardarPuntaje : MonoBehaviour {
	
	private string nombre = "";
 	private ArrayList mejoresJugadores;
	private ArrayList mejoresJugadoresPuntajes;
	private bool hayQueRegistrarNuevoPuntaje;
	
	
	void Start(){
		//PlayerPrefs.DeleteAll();
		//PlayerPrefs.SetInt("puntajeAdquirido",110);
		
		
		cargarPuntajes();
		/*for( int i=0; i<mejoresJugadores.Count; i++ ){
			Debug.Log("nombre="+mejoresJugadores[i]+"       puntos="+mejoresJugadoresPuntajes[i]);
		}
		*/
		
		hayQueRegistrarNuevoPuntaje = registrarNuevoPuntaje();
	}
	
	void cargarPuntajes(){
		mejoresJugadores = new ArrayList();
		mejoresJugadoresPuntajes = new ArrayList();
		
		int i=1;
		while(i<6){
			if(PlayerPrefs.GetString("jugador"+i) != ""){
				mejoresJugadores.Add(PlayerPrefs.GetString("jugador"+i));
				mejoresJugadoresPuntajes.Add(PlayerPrefs.GetInt("jugadorPuntaje"+i));
			}
			i++;
		}
	}
	
	
	bool registrarNuevoPuntaje(){
		if(mejoresJugadores.Count<5){
			return true;
		}
		
		int i=1;
		while(i<6){
			if(PlayerPrefs.GetInt("puntajeAdquirido") > PlayerPrefs.GetInt("jugadorPuntaje"+i)){
				return true;
			}
			i++;
		}
		return false;
	}
	
	void OnGUI () {
 		if(hayQueRegistrarNuevoPuntaje){
			
			// Make a background box
			GUI.Box(new Rect(Screen.width/2-200,Screen.height/2-200,400,160), "Lograste un buen puntaje!");
			
			GUI.Label(new Rect(Screen.width/2-180,Screen.height/2-160,200,20), "Ingresa tu nombre:");
			nombre = GUI.TextField (new Rect(Screen.width/2-60,Screen.height/2-160,240,20), nombre, 25);
	
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if(GUI.Button(new Rect(Screen.width/2-50,Screen.height/2-100,100,20), "Guardar")) {
				guardarNuevoPuntaje(nombre,PlayerPrefs.GetInt("puntajeAdquirido"));
				Application.LoadLevel(2);
			}
		}
		else{
			Application.LoadLevel(2);	
		}
	}
	
	
	void guardarNuevoPuntaje(string nombre, int puntaje){
		ArrayList mejoresJugadoresNuevo = new ArrayList();
		ArrayList mejoresJugadoresPuntajesNuevo = new ArrayList();
		
		if(mejoresJugadores.Count == 0){
			mejoresJugadoresNuevo.Add(nombre);
			mejoresJugadoresPuntajesNuevo.Add(puntaje);
		}
		else{
			bool registroPuntaje=false;
			for( int i=0; i<mejoresJugadores.Count; i++ ){
				if(puntaje > (int)mejoresJugadoresPuntajes[i]  && registroPuntaje == false){
					mejoresJugadoresNuevo.Add(nombre);
					mejoresJugadoresPuntajesNuevo.Add(puntaje);
					registroPuntaje=true;
					if(i<4){
						mejoresJugadoresNuevo.Add(mejoresJugadores[i]);
						mejoresJugadoresPuntajesNuevo.Add(mejoresJugadoresPuntajes[i]);	
					}
				}
				else{
					mejoresJugadoresNuevo.Add(mejoresJugadores[i]);
					mejoresJugadoresPuntajesNuevo.Add(mejoresJugadoresPuntajes[i]);	
				}
			}	
			
			
			if(registroPuntaje == false){
				mejoresJugadoresNuevo.Add(nombre);
				mejoresJugadoresPuntajesNuevo.Add(puntaje);
				registroPuntaje=true;
			}
		}
		
		
		
		//Guardamos en PlayePrefs
		
		for( int i=0; i<mejoresJugadoresNuevo.Count; i++ ){
			PlayerPrefs.SetString("jugador"+(i+1), mejoresJugadoresNuevo[i]+"");
			PlayerPrefs.SetInt("jugadorPuntaje"+(i+1), (int)mejoresJugadoresPuntajesNuevo[i]);
		}
		
		
	}
 	
 	
	void Update () {
	 	/*
	    if(stringToEdit == "1234") { 
	    Debug.Log("code correct"); 
	 
	    } 
	    */ 
	}
}
