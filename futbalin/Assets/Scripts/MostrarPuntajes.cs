using UnityEngine;
using System.Collections;

public class MostrarPuntajes : MonoBehaviour {
	private ArrayList mejoresJugadores;
	private ArrayList mejoresJugadoresPuntajes;
	

	// Use this for initialization
	void Start () {
		cargarPuntajes();
	}
	
	// Update is called once per frame
	void Update () {
	
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
	
	
	void OnGUI () {
		GUI.Box(new Rect(Screen.width/2-120,Screen.height/2-180,240,160), "Mejores 5 resultados");
		int restaHeight = 160;
		
		for(int i=0;i<mejoresJugadores.Count; i++){
			GUI.Label(new Rect(Screen.width/2-60,Screen.height/2-restaHeight,200,20), (i+1)+". "+mejoresJugadores[i]+" "+mejoresJugadoresPuntajes[i]+" goles");
			restaHeight = restaHeight-20;
		}
		
		if(GUI.Button(new Rect(Screen.width/2-50,Screen.height/2-restaHeight+10,100,20), "Salir")) {
			Application.LoadLevel(0);
		}
		
		
	}
}
