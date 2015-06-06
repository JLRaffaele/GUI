using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

// Singleton class for accessing "global" variables and methods.
public class Environment : MonoBehaviour
{
	#region Varaibles and properties
	public static Environment env;

    // The multiplier at which time flows.
    public int timeWarp = 1;

	// The number of planets created.
    private  int numberOfPlanetsCreated = 0;

	// Material for trail.
    public Material tracerMat;

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
        get { return _allBodies; }
        set { _allBodies = value; }
    }
	#endregion

	#region Start, Update, Awake ect.
	// Use this for initialization
    void Start()
    {
        // adjust time.
        Time.timeScale = TimeWarp;

        // Add each already existent rigid body to the list.
        foreach (GameObject body in GameObject.FindGameObjectsWithTag("Body"))
            AllBodies.Add(body);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Apply gravity between each and every combination of objects.
        for (int i = 0; i < AllBodies.Count - 1; i++)
            for (int j = i + 1; j < AllBodies.Count; j++)
                ApplyGravity(AllBodies[i].GetComponent<Rigidbody2D>(), AllBodies[j].GetComponent<Rigidbody2D>());

        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
			if (GameObject.Find("Add").GetComponent<AddButtonScript>().addingPlanet)
				CreateNewPlanet();
        }

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
	#endregion

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
		else
			throw new System.ArgumentException("Null parameter.");
	}
	private void CreateNewPlanet()
	{
		numberOfPlanetsCreated++;
		string planetName = "Planet" + numberOfPlanetsCreated;

		GameObject newPlanet = new GameObject(planetName);
		GameObject addButton = GameObject.Find("Add");

		Sprite planetsprite = AssetDatabase.LoadAssetAtPath("Assets/Images/Sun.png", typeof(Sprite)) as Sprite;


		newPlanet.AddComponent<SpriteRenderer>();
		newPlanet.GetComponent<SpriteRenderer>().sprite = planetsprite;

		newPlanet.tag = "Body";

		newPlanet.AddComponent<Rigidbody2D>();
		newPlanet.GetComponent<Rigidbody2D>().mass = float.Parse(GameObject.Find("SizeInput").GetComponent<InputField>().text);
		newPlanet.GetComponent<Rigidbody2D>().drag = 0;
		newPlanet.GetComponent<Rigidbody2D>().angularDrag = 0;
		newPlanet.GetComponent<Rigidbody2D>().gravityScale = 0;

		newPlanet.AddComponent<TrailRenderer>();
		newPlanet.GetComponent<TrailRenderer>().startWidth = 0.1F;
		newPlanet.GetComponent<TrailRenderer>().endWidth = 0.1F;
		newPlanet.GetComponent<TrailRenderer>().time = float.Parse(GameObject.Find("TrailDurationInput").GetComponent<InputField>().text);
		newPlanet.GetComponent<TrailRenderer>().material = tracerMat;
		//tracer color cannot be changed by default through script

		Vector2 pos = Input.mousePosition;
		pos = Camera.main.ScreenToWorldPoint(pos);
		newPlanet.transform.position = pos;

		newPlanet.AddComponent<AddVelocity>();
		addButton.GetComponent<AddButtonScript>().addingPlanet = false;

		AllBodies.Add(newPlanet);
	}
    

}