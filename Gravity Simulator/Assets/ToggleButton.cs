using UnityEngine;
using System.Collections;

public class ToggleButton : MonoBehaviour {

	public GameObject objectToToggle;
	public bool initialState = false;

	public void Start()
	{
		if (initialState)
			objectToToggle.SetActive(true);
		else
			objectToToggle.SetActive(false);		
	}


	public void Toggle()
	{
		if(initialState)
			objectToToggle.SetActive(false);
		else
			objectToToggle.SetActive(true);

		initialState = !initialState;
	}
}
