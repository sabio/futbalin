using UnityEngine;
using System.Collections;

public class MovimientoCancha : MonoBehaviour {

   public float speed = 10.0F;
   public float rotationSpeed = 100.0F;
   public GameObject pelota;
   public float translation = 0F;
	
	void Update() {
		/*Debug.Log ("valor de input vertical" + Input.GetAxis("Vertical") );
		Debug.Log ("valor de input horizontal" + Input.GetAxis("Horizontal") );
        translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
		*/
    }
	
	void FixedUpdate() {
	/*	if (translation >0)
			pelota.rigidbody.AddForce(10,0,10);
	*/
	}	
}
