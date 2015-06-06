using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

// Singleton class for accessing "global" variables and methods.
public class Environment : MonoBehaviour
{

    public static Environment env;

    // The multiplier at which time flows.
    public int timeWarp = 1;

	// The number of planets created.
    private  int numCreated = 0;

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
        {
            if (body1 == null)
                throw new System.ArgumentException("body 1 is a null parameter.");
            else
                throw new System.ArgumentException("body 2 is a null parameter.");
        }
    }

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
			if (GameObject.Find("Add").GetComponent<AddPlanet>().addingPlanet)
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

	private void CreateNewPlanet()
	{
		numCreated++;
		string planetName = "Planet" + numCreated;
		var newPlanet = new GameObject(planetName);
		// GameObject newPlanet = GameObject.Find(GameObject.Find("Add").GetComponent<AddPlanet>().planetName);
		GameObject addButton = GameObject.Find("Add");

		var planetsprite = AssetDatabase.LoadAssetAtPath("Assets/Sun.png", typeof(Sprite)) as Sprite;


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

		var pos = Input.mousePosition;
		pos = Camera.main.ScreenToWorldPoint(pos);
		pos.z = 0;
		newPlanet.transform.position = pos;

		newPlanet.AddComponent<AddVelocity>();
		addButton.GetComponent<AddPlanet>().addingPlanet = false;

		GameObject.Find("Environment").GetComponent<Environment>().AllBodies.Add(newPlanet);
		GameObject.Find("Add").GetComponent<AddPlanet>().addingPlanet = false;
	}
    

}