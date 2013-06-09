using UnityEngine;
using System.Collections;

public class Nivel4 : MonoBehaviour {
	
	public int CANTIDAD_GOLES = 1;
	int numGoles;
	public int nivelDeInicio = 1;
	private int nivel;
	public bool pause = false;
	public float tiempoDelJuego = 60;
	private float timer;
	private bool terminoElTiempo = false;
	public GUISkin skin;
	public Material canchaMaterial1;
	public Material canchaMaterial2;
	public Material canchaMaterial3;
	public Material canchaMaterial4;
	public GameObject Cancha;
	private bool ahoritaMeteGolEnLaPorteriaDerecha = true;
	
	// Use this for initialization
	void Start () {
		//renderer.material.mainTexture = (Texture)Resources.Load("Textura/Materials/campo24");
		//Debug.Log (GameObject.Find("Cancha").GetComponent("campo1"));
		
		
		iniciarJuego();
		
		
		Cancha.renderer.material = canchaMaterial1;
		 
		Vector3 posCamara = Camera.main.transform.position;
		GameObject marcador = GameObject.Find("Marcador");
		if(!ahoritaMeteGolEnLaPorteriaDerecha){
			marcador.transform.position = new Vector3(posCamara.x,posCamara.y-17,posCamara.z-9);
		}else{
			marcador.transform.position = new Vector3(posCamara.x-8,posCamara.y-17,posCamara.z+9);
		}
		
	}
		
	
	void iniciarJuego(){
		numGoles = 0;
		nivel = nivelDeInicio;
		timer = tiempoDelJuego;
		terminoElTiempo = false;
		empezarNivel();	
	}
	
	
	void OnGUI () {
		if(terminoElTiempo){
			Application.LoadLevel(0);
		}
		else if(pause){
			GUI.Box(new Rect(0,0,Screen.width ,Screen.height),"");
		}
		
	}
	
	
	// Update is called once per frame
	void Update () {
		if(timer <= 0 ) 
			terminoElTiempo = true;
		
		if(terminoElTiempo){
			limpiarObjetosDelCampo();
		}else{
			if (Input.GetKeyUp("p") ){
				pause = pause == false ? true : false;
			}
			
			if(pause){
				Time.timeScale = 0;
				GameObject.Find("musicaJuego").audio.Pause();
				//GameObject.Find("PausaMensaje").GetComponent("MeshFilter").renderer.enabled = true;
				GameObject.Find("PausaMensaje").renderer.enabled = true;
			}
			else{
				Time.timeScale = 1;
				if(!GameObject.Find("musicaJuego").audio.isPlaying){
					GameObject.Find("musicaJuego").audio.Play();
					GameObject.Find("PausaMensaje").renderer.enabled = false;
				}
			}
			
			timer -= Time.deltaTime;
			updateMarcador();
		}
	}
	
	
	
	
	
	void crearObjetos(ArrayList objetos){
		GameObject clonado;
		foreach (ObjetoPlantilla op in objetos)
		{
		    clonado = Instantiate( Resources.LoadAssetAtPath("Assets/Objetos/"+op.nombre+".prefab", typeof(GameObject)) ) as GameObject;
			clonado.transform.position = new Vector3(op.x,op.y,op.z);
			clonado.name = op.nombre;
			clonado.tag = "objetoDeCancha";
			
			
			if(op.nombre.Equals("Cilindro") || op.nombre.Equals("Barra")){
				Defensa script = clonado.GetComponent<Defensa>();
				if(nivel > 20 && op.nombre.Equals("Barra")){
					script.nivelDefensa = 4;
				}else{
					script.nivelDefensa = op.nivel;
				}
			}
		}
	}
	
	void updateMarcador(){
		GameObject marcador = GameObject.Find("Marcador");
		TextMesh t = (TextMesh)marcador.GetComponent(typeof(TextMesh));
		t.text = "Goles: "+numGoles+"\nEscena: "+nivel+"    Tiempo: "+((int)timer);
		//t.text = "Goles: "+numGoles+"\nTiempo: "+((int)timer);
	}
	
	public void GOOOL(){
		Destroy(GameObject.Find("pelota"));
		numGoles++;
		if(numGoles%CANTIDAD_GOLES == 0){
			irAlSiguienteNivel();
		}
		else{
			GameObject pelota = Instantiate( Resources.LoadAssetAtPath("Assets/Objetos/pelota.prefab", typeof(GameObject)) ) as GameObject;
			pelota.transform.position = new Vector3(-7.423032f,3.175456f,-10.79169f);
			pelota.name = "pelota";
			pelota.tag = "objetoDeCancha";
			establecerOrientacionDelBalon(pelota);
			establecerVelocidadDelBalon(pelota);
			
			updateMarcador();
		}
		
			
	}
	
	void irAlSiguienteNivel(){
		nivel++;
		

		if(nivel == 41){
			Application.LoadLevel(0);
		}
		
		empezarNivel();
		
	}
	
	void limpiarObjetosDelCampo(){
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("objetoDeCancha"))
		{
		    Destroy (obj);
		}
	}
	
	
	void voltearCancha(){
		GameObject pelota = GameObject.Find("pelota");
		GameObject marcador = GameObject.Find("Marcador");
		GameObject pausaMensaje = GameObject.Find("PausaMensaje");
		marcador.transform.Rotate(0,0,180);
		pausaMensaje.transform.Rotate(0,0,180);
		MovimientoPelota script = pelota.GetComponent<MovimientoPelota>();
		Camera.main.transform.Rotate(0,0,180);
		Vector3 posCamara = Camera.main.transform.position;
		
		if(ahoritaMeteGolEnLaPorteriaDerecha == true){
			ahoritaMeteGolEnLaPorteriaDerecha = false;
			marcador.transform.position = new Vector3(posCamara.x,posCamara.y-17,posCamara.z-9);
			pausaMensaje.transform.position = new Vector3(8.093399f,9.23919f,-14.13325f);
			
		}else{
			ahoritaMeteGolEnLaPorteriaDerecha = true;
			marcador.transform.position = new Vector3(posCamara.x-8,posCamara.y-17,posCamara.z+9);
			pausaMensaje.transform.position = new Vector3(-3.314562f,9.674511f,-7.034167f);
			
		}
	}
	
	void establecerOrientacionDelBalon(GameObject pelota){
		MovimientoPelota script = pelota.GetComponent<MovimientoPelota>();
		Vector3 posCamara = Camera.main.transform.position;
		if(ahoritaMeteGolEnLaPorteriaDerecha){
			script.setCamaraVolteada(false);
		}else{
			script.setCamaraVolteada(true);
		}
	}
	
	void establecerVelocidadDelBalon(GameObject pelota){
		MovimientoPelota script = pelota.GetComponent<MovimientoPelota>();
		Vector3 posCamara = Camera.main.transform.position;
		if(nivel > 20){
			script.moveSpeed = 120f;
		}else{
			script.moveSpeed = 60f;
		}	
	}
	
	
	
	
	void empezarNivel(){
		limpiarObjetosDelCampo();
		
		GameObject pelota = Instantiate( Resources.LoadAssetAtPath("Assets/Objetos/pelota.prefab", typeof(GameObject)) ) as GameObject;
		pelota.transform.position = new Vector3(-7.423032f,3.175456f,-10.79169f);
		pelota.name = "pelota";
		pelota.tag = "objetoDeCancha";
		establecerOrientacionDelBalon(pelota);
		establecerVelocidadDelBalon(pelota);
		
		ArrayList objetos = new ArrayList();
		if(nivel == 1 || nivel == 21){
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			
		}
		else if(nivel == 2 || nivel == 22){
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			
		}
		else if(nivel == 3 || nivel == 23){
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			
		}
		else if(nivel == 4 || nivel == 24){
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
		}
		else if(nivel == 5 || nivel == 25){
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
		}
		else if(nivel == 6 || nivel == 26){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
		}
		else if(nivel == 7 || nivel == 27){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,2));
		}
		else if(nivel == 8 || nivel == 28){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",16.3f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,2));
		}
		
		else if(nivel == 9 || nivel == 29){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",16.3f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,1));
			objetos.Add(new ObjetoPlantilla("Barra",0f,3.082378f,2f,1));
		}
		
		else if(nivel == 10 || nivel == 30){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",16f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,2));
			objetos.Add(new ObjetoPlantilla("Barra",0f,3.082378f,2f,2));
		}
		
		else if(nivel == 11 || nivel == 31){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,3));
			objetos.Add(new ObjetoPlantilla("Barra",0f,3.082378f,2f,3));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
		}
		
		else if(nivel == 12 || nivel == 32){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",16.3f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,3));
			
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
		}
		
		else if(nivel == 13 || nivel == 33){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",16.3f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,3));
			objetos.Add(new ObjetoPlantilla("Barra",0f,3.082378f,2f,3));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
		}
		
		else if(nivel == 14 || nivel == 34){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f,1));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",16.3f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,3));
			objetos.Add(new ObjetoPlantilla("Barra",0f,3.082378f,2f,3));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
		}
		
		else if(nivel == 15 || nivel == 35){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f,2));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",16f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,3));
			objetos.Add(new ObjetoPlantilla("Barra",0f,3.082378f,2f,3));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
		}
		
		else if(nivel == 16 || nivel == 36){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f,2));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",16f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,3));
			objetos.Add(new ObjetoPlantilla("Barra",0f,3.082378f,2f,3));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
		}
		
		else if(nivel == 17 || nivel == 37){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f,1));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f,1));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f));
			
			objetos.Add(new ObjetoPlantilla("Barra",16f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,-14.48495f,3));
			objetos.Add(new ObjetoPlantilla("Barra",0f,3.082378f,2f,3));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
			
		}
		
		else if(nivel == 18 || nivel == 38){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f,1));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f,1));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f,1));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f,2));
			
			
			objetos.Add(new ObjetoPlantilla("Barra",16f,3.082378f,2f,3));
			/*objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
			objetos.Add(new ObjetoPlantilla("Bloque",15f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",15f,2.817461f,0.5f));*/
		}
		
		else if(nivel == 19 || nivel == 39){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f,1));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f,1));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f,1));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f,1));
			
			objetos.Add(new ObjetoPlantilla("Barra",16f,3.082378f,2f,3));
			/*objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
			objetos.Add(new ObjetoPlantilla("Bloque",15f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",15f,2.817461f,0.5f));*/
		}
		
		else if(nivel == 20 || nivel == 40){
			//Portero
			objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f,1));
			//Delanteros
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-10.93861f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.5f,2.759493f,-17.5f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",2.52f,2.759493f,-4.5f,1));
			//medios
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-10.93861f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.5f,2.759493f,-17.5f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",8.52f,2.759493f,-4.5f,1));
			//defensas
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-1.795187f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-7.619292f,2));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-14.63951f,1));
			objetos.Add(new ObjetoPlantilla("Cilindro",14.07548f,2.759493f,-20.2f,1));
			
			objetos.Add(new ObjetoPlantilla("Barra",16f,3.082378f,2f,3));
			objetos.Add(new ObjetoPlantilla("Barra",11f,3.082378f,2f,3));
			/*objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
			objetos.Add(new ObjetoPlantilla("Bloque",15f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",15f,2.817461f,0.5f));
			*/
		}
		
		
		
		
		crearObjetos(objetos);
		updateMarcador();
	}
	
}









/*


class ObjetoPlantilla{
	public string nombre;
	public float x;
	public float y;
	public float z;
	public int nivel;
	
	
	public ObjetoPlantilla(string nombre, float x, float y, float z, int nivel){
		this.nombre = nombre;
		this.x = x;
		this.y = y;
		this.z = z;
		this.nivel = nivel;
	}
	
	public ObjetoPlantilla(string nombre, float x, float y, float z){
		this.nombre = nombre;
		this.x = x;
		this.y = y;
		this.z = z;
	}
	
	public ObjetoPlantilla()
	{
	}
	
}

*/