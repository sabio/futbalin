using UnityEngine;
using System.Collections;

public class MovimientoPelota : MonoBehaviour {
	
	public float moveSpeed = 60.0f;
	private bool camaraVolteada = false;
	
	// Use this for initialization
	void Start () {
		//rigidbody.AddForce(1000,0,1000);
	}
	
	
	
	void Update () 
	{	
		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward.x = 0;
		forward = forward.normalized;
		
		Vector3 forwardForce = new Vector3();
		
		forwardForce = forward * Input.GetAxis("Vertical") * moveSpeed;
		
		if(camaraVolteada){
			rigidbody.AddForce(forwardForce);
			
		}else{
			rigidbody.AddForce(-forwardForce);
			
		}
		Vector3 right= Camera.main.transform.TransformDirection(Vector3.right);
		right.y = 0;
		right = right.normalized;
		
		Vector3 rightForce = new Vector3();
		
		rightForce= right * Input.GetAxis("Horizontal") * moveSpeed;
		rigidbody.AddForce(rightForce);
				
		/*if (canJump && Input.GetKeyDown(KeyCode.Space))
		{
			rigidbody.AddForce(Vector3.up * jumpSpeed * 100);
			canJump = false;
			_GameManager.GetComponent<GameManager>().BallJump();
		}*/
	}
	
	/*
	void OnCollisionEnter(Collision collision) {
	 	Debug.Log (collision.transform.name);
		if (collision.transform.name == "Cilindro") {
			rigidbody.AddForce(200,0,200);
		} else if(collision.transform.name == "Vara") {
				rigidbody.AddForce(200,0,200);
		} 
   }
	*/
    
	void OnTriggerEnter(Collider other) {
       if (other.transform.name=="Porteria"){
			GameObject obj = GameObject.Find("JuegoControlador");
		 	JuegoControlador script = obj.GetComponent<JuegoControlador>();
			//JuegoControlador script = obj.GetComponent(typeof(JuegoControlador));
			script.GOOOL();
		}
		else if(other.transform.name=="PorteriaNivel1"){
			GameObject obj = GameObject.Find("JuegoControlador");
		 	Nivel1 script = obj.GetComponent<Nivel1>();
			//JuegoControlador script = obj.GetComponent(typeof(JuegoControlador));
			script.GOOOL();
		}
		else if(other.transform.name=="PorteriaNivel2"){
			GameObject obj = GameObject.Find("JuegoControlador");
		 	Nivel2 script = obj.GetComponent<Nivel2>();
			//JuegoControlador script = obj.GetComponent(typeof(JuegoControlador));
			script.GOOOL();
		}
		else if(other.transform.name=="PorteriaNivel3"){
			GameObject obj = GameObject.Find("JuegoControlador");
		 	Nivel3 script = obj.GetComponent<Nivel3>();
			//JuegoControlador script = obj.GetComponent(typeof(JuegoControlador));
			script.GOOOL();
		}
		else if(other.transform.name=="PorteriaNivel4"){
			GameObject obj = GameObject.Find("JuegoControlador");
		 	Nivel4 script = obj.GetComponent<Nivel4>();
			//JuegoControlador script = obj.GetComponent(typeof(JuegoControlador));
			script.GOOOL();
		}
    }
	
	
	public void setCamaraVolteada(bool camaraVolteada){
		this.camaraVolteada = camaraVolteada;
	}
	
    	
}





/*


public class MoverPelota : MonoBehaviour {

	private GameObject moveJoy;
	private GameObject _GameManager;
	public Vector3 movement;
	public float moveSpeed = 6.0f;
	public float jumpSpeed = 5.0f;
	public float drag = 2;
	private bool canJump = true;
	
	void Start()
	{
		moveJoy = GameObject.Find("LeftJoystick");
		_GameManager = GameObject.Find("_GameManager");
	}
	
	void Update () 
	{	
		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		
		Vector3 forwardForce = new Vector3();
		if (Application.platform == RuntimePlatform.Android) 
		{
			float tmpSpeed = moveJoy.GetComponent<Joystick>().position.y;
			forwardForce = forward * tmpSpeed * 1f * moveSpeed;
		}
		else
		{
			forwardForce = forward * Input.GetAxis("Vertical") * moveSpeed;
		}
		Debug.Log("Input.GetAxis(Vertical) = "+Input.GetAxis("Vertical"));
		rigidbody.AddForce(forwardForce);
		
		Vector3 right= Camera.main.transform.TransformDirection(Vector3.right);
		right.y = 0;
		right = right.normalized;
		
		Vector3 rightForce = new Vector3();
		if (Application.platform == RuntimePlatform.Android) 
		{
			float tmpSpeed = moveJoy.GetComponent<Joystick>().position.x;
			rightForce = right * tmpSpeed * 0.8f * moveSpeed;
		}
		else
		{
			rightForce= right * Input.GetAxis("Horizontal") * moveSpeed;
		}		
		rigidbody.AddForce(rightForce);
				
		if (canJump && Input.GetKeyDown(KeyCode.Space))
		{
			rigidbody.AddForce(Vector3.up * jumpSpeed * 100);
			canJump = false;
			_GameManager.GetComponent<GameManager>().BallJump();
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Destroy")
		{
			_GameManager.GetComponent<GameManager>().Death();
			Destroy(gameObject);
		}
		else if (other.tag == "Coin")
		{
			Destroy(other.gameObject);
			_GameManager.GetComponent<GameManager>().FoundCoin();
		}
		else if (other.tag == "SpeedBooster")
		{
			movement = new Vector3(0,0,0);
			_GameManager.GetComponent<GameManager>().SpeedBooster();
		}
		else if (other.tag == "JumpBooster")
		{
			movement = new Vector3(0,0,0);
			_GameManager.GetComponent<GameManager>().JumpBooster();
		}
		else if (other.tag == "Teleporter")
		{
			movement = new Vector3(0,0,0);
			_GameManager.GetComponent<GameManager>().Teleporter();
		}
    }
	
	void OnCollisionEnter(Collision collision)
	{
		if (!canJump)
		{
			canJump = true;
			_GameManager.GetComponent<GameManager>().BallHitGround();
		}
    }
	
	void OnGUI()
	{
		GUI.Label(new Rect(300,10,100,100),"X: " + moveJoy.GetComponent<Joystick>().position.x.ToString());
		GUI.Label(new Rect(300,30,100,100),"Y: " + moveJoy.GetComponent<Joystick>().position.y.ToString());
	}
}
*/