using UnityEngine;
using System.Collections;

public class AddVelocity : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
		this.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, 23));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
