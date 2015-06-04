using UnityEngine;
using System.Collections;
// Singleton class for accessing "global" variables and methods.
public class Environment : MonoBehaviour {

	public static Environment env;

	// The multiplier at which time flows.
	public int timeWarp = 1;


	public int TimeWarp
	{
		get { return timeWarp; }
		set 
		{ 
			Time.timeScale = value;
			timeWarp = value;
		}
	}


	// A list meant to contain every Rigid body attached to each object on the scene.
	private System.Collections.Generic.List<GameObject> _allBodies = new System.Collections.Generic.List<GameObject>();

	public System.Collections.Generic.List<GameObject> AllBodies
	{
		get { return _allBodies;  }
		set { _allBodies = value; }
	}

	// Applies the force of gravity to the objects passed in.
	public void ApplyGravity(Rigidbody2D body1, Rigidbody2D body2)
	{
		if (body1 != null && body2 != null)
		{
			// The force of gravity these objects enact on each other.
			float Fgrav = (body1.mass * body2.mass) / Mathf.Pow(Vector2.Distance(body1.position, body2.position), 2);

			// Vector of force applied.
			Vector2 forceVector = (body2.position - body1.position) * Fgrav;

			// Apply the force.
			body1.AddForce(forceVector * Time.deltaTime);
			body2.AddForce(forceVector * Time.deltaTime * -1);
		}
		else throw new System.ArgumentException("null parameter.");
	}

	// Use this for initialization
	void Start ()
	{
		// adjust time.
		Time.timeScale = TimeWarp;

		// Add each already existent rigid body to the list.
		foreach (GameObject body in GameObject.FindGameObjectsWithTag("Body"))
			AllBodies.Add(body);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		// Apply gravity between each and every combination of objects.
		for (int i = 0; i < AllBodies.Count - 1; i++)
			for (int j = i + 1; j < AllBodies.Count; j++)
				ApplyGravity(AllBodies[i].GetComponent<Rigidbody2D>(), AllBodies[j].GetComponent<Rigidbody2D>());
	}

	// Singleton method.
	void Awake()
	{
		if (env != null)
			GameObject.Destroy(env);
		else
			env = this;

		DontDestroyOnLoad(this);
	}
}
