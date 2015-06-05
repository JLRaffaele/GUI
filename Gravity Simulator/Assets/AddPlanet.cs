using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;


public class AddPlanet : MonoBehaviour {

	public Material tracerMat;
	//int numCreated = 1;
    public bool addingPlanet = false;
    //public string planetName = "";

	// Use this for initialization
	void Start () {
        GameObject.Find("SizeInput").GetComponent<InputField>().text = "1000";
        GameObject.Find("TrailDurationInput").GetComponent<InputField>().text = "10";
	}

	public void AddNewPlanet(){

        addingPlanet = !addingPlanet;
    }
	

    
}
