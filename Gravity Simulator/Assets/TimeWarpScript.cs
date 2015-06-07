using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimeWarpScript : MonoBehaviour 
{
	// Array of possible speeds.
	private readonly float[] VALID_TIME_WARPS = {0.25F, 0.5F, 1, 2, 4, 10, 25, 50, 100};

	// Current index of VALID_TIME_WARPS.
	private int timeWarpIndex = 2;

	// Used for pausing.
	private float activeTimeWarp = 1F;
	private bool frozen = false;

	public float TimeWarp
	{
		get { return GameObject.Find("Environment").GetComponent<Environment>().TimeWarp; }
		set { GameObject.Find("Environment").GetComponent<Environment>().TimeWarp = value; }
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnSlowDownClick()
	{
		if (timeWarpIndex > 0)
		{
			--timeWarpIndex;

			TimeWarp = VALID_TIME_WARPS[timeWarpIndex];

			activeTimeWarp = TimeWarp;
		}
	}

	public void OnFastForwardClick()
	{
		if (timeWarpIndex < 8)
		{
			++timeWarpIndex;

			TimeWarp = VALID_TIME_WARPS[timeWarpIndex];

			activeTimeWarp = TimeWarp;

			displayFadingUIText("Time Warp: X" + TimeWarp, 30F, GameObject.Find("TimeWarpText").GetComponent<Text>());
		}
	}

	public void OnPauseClick()
	{
		if (frozen)
		{
			TimeWarp = activeTimeWarp;
			displayFadingUIText("Unpaused", 3F, GameObject.Find("TimeWarpText").GetComponent<Text>());
		}
		else
		{
			TimeWarp = 0;
			displayFadingUIText("Paused", 3F, GameObject.Find("TimeWarpText").GetComponent<Text>());

		}

		frozen = !frozen;

	}


	private void displayFadingUIText(string textToDisplay, float duration, Text fadingTextObject)
	{

		fadingTextObject.text = textToDisplay;

		// Colors for transitioning.
		Color originalTextColor = fadingTextObject.color;
		Color finalColor = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, 0);

		//transition
		//for(float t = 0.0F; t < duration; t+= Time.deltaTime)
		//	fadingTextObject.color = Color.Lerp(originalTextColor, finalColor, t / duration);

		//fadingTextObject.text = "";
		//fadingTextObject.color = originalTextColor;
	}
}
