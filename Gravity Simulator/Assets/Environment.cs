using UnityEngine;
using System.Collections;
using Assets;
// Singleton class for accessing "global" variables and methods.
public class Environment : MonoBehaviour {

	public static Environment env;


	// The rate at which distance is converted from real world meters to unity sizes.
	// Example: Planet is 12756000m, unity representation is 20m. Rate is  actual / rep = 637800.
	private float _distanceConversionRate = 1;

	// Property for distanceConversionRate.
	public float DistanceConversionRate
	{
		get { return _distanceConversionRate;  }
		set { _distanceConversionRate = value; }
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
		Int128 test = (Int128)40000000000000000000000000000;

		if (body1 != null && body2 != null)
		{
			// The force of gravity these objects enact on each other.
			float Fgrav = (body1.mass * body2.mass) / Mathf.Pow(Vector2.Distance(body1.position, body2.position), 2);

			// Vector of force applied.
			Vector2 forceVector = (body2.position - body1.position) * Fgrav;


			Debug.Log(forceVector.ToString());

			// Apply the force.
			body1.AddForce(forceVector * Time.deltaTime);
			body2.AddForce(forceVector * Time.deltaTime * -1);
		}
		else throw new System.ArgumentException("null parameter.");
	}

	// Use this for initialization
	void Start ()
	{
		// Add each already existent rigid body to the list.
		foreach (GameObject body in GameObject.FindGameObjectsWithTag("Body"))
			AllBodies.Add(body);
	}
	
	// Update is called once per frame
	void Update ()
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
