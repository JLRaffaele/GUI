using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AddButtonScript : MonoBehaviour {

	public Material tracerMat;
	//int numCreated = 1;
    public bool addingPlanet = false;
	//bool clicked = false;
    //public string planetName = "";

	// Use this for initialization
	void Start ()
	{
	}

	public void AddNewPlanet(){

		addingPlanet = true;

    }
	

    
}
