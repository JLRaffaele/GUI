using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimeWarpScript : MonoBehaviour
{
	// Array of possible speeds.
	private readonly float[] VALID_TIME_WARPS = { 0.25F, 0.5F, 1, 2, 4, 10, 25, 50, 100 };

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
	void Start()
	{
		GameObject.Find("TimeWarpText").GetComponent<Text>().text = "Time Warp: X" + TimeWarp;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnSlowDownClick()
	{
		if (timeWarpIndex > 0)
		{
			--timeWarpIndex;

			TimeWarp = VALID_TIME_WARPS[timeWarpIndex];

			activeTimeWarp = TimeWarp;

			GameObject.Find("TimeWarpText").GetComponent<Text>().text = "Time Warp: X" + TimeWarp;
		}
	}

	public void OnFastForwardClick()
	{
		if (timeWarpIndex < 8)
		{
			++timeWarpIndex;

			TimeWarp = VALID_TIME_WARPS[timeWarpIndex];

			activeTimeWarp = TimeWarp;

			GameObject.Find("TimeWarpText").GetComponent<Text>().text = "Time Warp: X" + TimeWarp;
		}
	}

	public void OnPauseClick()
	{
		if (frozen)
		{
			TimeWarp = activeTimeWarp;
			GameObject.Find("TimeWarpText").GetComponent<Text>().text = "Time Warp: X" + TimeWarp;
		}
		else
		{
			TimeWarp = 0;
			GameObject.Find("TimeWarpText").GetComponent<Text>().text = "Paused";


		}

		frozen = !frozen;

	}
}
