using UnityEngine;
using System.Collections;

public class JuegoControlador : MonoBehaviour {
	
	public int CANTIDAD_GOLES = 1;
	int numGoles;
	public int nivel = 1;
	public bool pause = false;

	// Use this for initialization
	void Start () {
		empezarNivel(nivel);
	}
	
	// Update is called once per frame
	void Update () {
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
		
	}
	
	
	
	void empezarNivel(int nivel){
		limpiarObjetosDelCampo();
		
		numGoles = CANTIDAD_GOLES;
		GameObject pelota = Instantiate( Resources.LoadAssetAtPath("Assets/Objetos/pelota.prefab", typeof(GameObject)) ) as GameObject;
		pelota.transform.position = new Vector3(-7.423032f,3.175456f,-10.79169f);
		pelota.name = "pelota";
		
		ArrayList objetos = new ArrayList();
		if(nivel == 1){
			//Portero
			//objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
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
		else if(nivel == 2){
			//Portero
			//objetos.Add(new ObjetoPlantilla("Cilindro",18.37619f,2.759493f,-10.74366f));
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
			
			objetos.Add(new ObjetoPlantilla("Barra",13f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",9f,3.082378f,-14.48495f,2));
		}
		
		else if(nivel == 3){
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
			
			objetos.Add(new ObjetoPlantilla("Barra",13f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",9f,3.082378f,-14.48495f,1));
			objetos.Add(new ObjetoPlantilla("Barra",2f,3.082378f,2f,1));
		}
		
		else if(nivel == 4){
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
			
			objetos.Add(new ObjetoPlantilla("Barra",13f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",9f,3.082378f,-14.48495f,2));
			objetos.Add(new ObjetoPlantilla("Barra",2f,3.082378f,2f,2));
		}
		
		else if(nivel == 5){
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
			
			objetos.Add(new ObjetoPlantilla("Barra",19f,3.082378f,2f,2));
			objetos.Add(new ObjetoPlantilla("Barra",9f,3.082378f,-14.48495f,3));
			objetos.Add(new ObjetoPlantilla("Barra",2f,3.082378f,2f,3));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
			
			
			
		}
		
		else if(nivel == 6){
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
			
			objetos.Add(new ObjetoPlantilla("Barra",19f,3.082378f,2f,3));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",6.960938f,2.817461f,0.5f));
			objetos.Add(new ObjetoPlantilla("Bloque",15f,2.817461f,-22f));
			objetos.Add(new ObjetoPlantilla("Bloque",15f,2.817461f,0.5f));
		}
		
		
		
		
		crearObjetos(objetos);
		updateMarcador();
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
				script.nivelDefensa = op.nivel;
			}
		}
	}
	
	void updateMarcador(){
		GameObject marcador = GameObject.Find("Marcador");
		TextMesh t = (TextMesh)marcador.GetComponent(typeof(TextMesh));
		t.text = "Goles restantes: "+numGoles+"\nNivel: "+nivel;
	}
	
	public void GOOOL(){
		Destroy(GameObject.Find("pelota"));
		numGoles--;
		
		if(numGoles==0){
			irAlSiguienteNivel();
		}
		else{
			GameObject pelota = Instantiate( Resources.LoadAssetAtPath("Assets/Objetos/pelota.prefab", typeof(GameObject)) ) as GameObject;
			pelota.transform.position = new Vector3(-7.423032f,3.175456f,-10.79169f);
			pelota.name = "pelota";
			updateMarcador();
		}
			
	}
	
	void irAlSiguienteNivel(){
		nivel++;
		empezarNivel(nivel);
	}
	
	void limpiarObjetosDelCampo(){
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("objetoDeCancha"))
		{
		    Destroy (obj);
		}
	}
}






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