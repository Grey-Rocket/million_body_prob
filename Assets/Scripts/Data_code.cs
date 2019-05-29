using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Data_code : MonoBehaviour {
    public float DT = 0.0001f;     // default step size
    public const int MASS = 1;          // default mass of objects
    public const int G = 1;             // default gravitational pull
    public float speed = 1;

    public bool mode = true;

    public int trail_time = 200;

    public string path = "";
    public int copy_num = -1;
    public int n = 2;
    int[] masses;
    Vector3[] locations;
    Vector3[] velocities;
    Vector3[] forces;

    public GameObject sample = null;
    GameObject[] bodies;
    Color[] colors;

    // Use this for initialization
    void Start () {
        if(path.Equals("")) {
            Debug.Log("USING DEFAULTS");
            masses = new int[n];
            locations = new Vector3[n];
            forces = new Vector3[n];
            colors = new Color[n];

            // masses = new int[] { 1100, 1, 1, 1, 1 };        // DIS ALSO WORKS
            masses = new int[] { 1000000, 1000, 1, 1, 1, 1 };
            // testing
            forces = new Vector3[] {
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0),

                // new Vector3(10, 0, 0),  // DIS WORKS
                new Vector3(0, 0, 0),
                new Vector3(0, -0, 0),
                new Vector3(-0, 0, 0),
                new Vector3(0, 0, 0)
            };
            velocities = new Vector3[] {

                new Vector3(0, 0, 0),

                new Vector3(0, 10*4, 0),
                // new Vector3(0, 10, 0),  // DIS WORKS
                new Vector3(0, 10*2+10*4, 0),
                new Vector3(10*1, 0+10*4, 0),
                new Vector3(0, -10*2+10*4, 0),
                new Vector3(-10*2, 0+10*4, 0)
            };
            locations = new Vector3[] {

                new Vector3(0, 0, 0),

                new Vector3(500, 0, 0),

                // new Vector3(-10f, 0, 0), // DIS WORKS
                new Vector3(-3f+500, 0, 0),                // LEVO
                new Vector3(0+500, 9f/2, 0),               // GOR
                new Vector3(7f/2+500, 0, 0),               // DESNO
                new Vector3(0+500, -8f/2, 0)               // DOL
            };

            bodies = new GameObject[n];
            for (int i = 0; i < n; i++) {
                bodies[i] = (GameObject)Instantiate(sample);
                bodies[i].transform.position = locations[i];
            }

            colors = new Color[] { Color.white, Color.magenta, Color.red, Color.green, Color.yellow};
        } else {
            // read from file
            using (TextReader reader = File.OpenText(path)) {
                n = int.Parse(reader.ReadLine());
                
                locations = new Vector3[n];
                velocities = new Vector3[n];
                forces = new Vector3[n];
                masses = new int[n];

                // read data for each body
                for (int i = 0; i < n; i++) {
                    locations[i] = ReadVector(reader.ReadLine().Split('_'));
                    velocities[i] = ReadVector(reader.ReadLine().Split('_'))*speed;
                    //reader.ReadLine();
                    //forces[i] = Vector3.zero;
                    forces[i] = ReadVector(reader.ReadLine().Split('_'));

                    Debug.Log("Body[" + i + "].locations = " + locations[i]);
                    // Debug.Log("Body[" + i + "].velocities = " + velocities[i]);
                    // Debug.Log("Body[" + i + "].forces = " + forces[i]);
                    
                    masses[i] = int.Parse(reader.ReadLine());
                }

                Debug.Log("[SYS]: Podatki prebrani");


                bodies = new GameObject[n];
                for(int i = 0; i < n; i++) {
                    bodies[i] = (GameObject)Instantiate(sample);
                    bodies[i].transform.position = locations[i];
                }

                Debug.Log("[SYS]: Bodies generated");
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
    
        if(mode) {
            // every body acting on another body
            for (int i = 0; i < n; i++) {
                Vector3 s_loc = locations[i];

                ComputeForce(i);
                ComputeVelocity(i);
                ComputeLocation(i);
                // Debug.Log("Body[" + i + "]: " + s_loc + " ---> " + locations[i]);
                // TODO: Debug logs za velocity in forces
                // Debug.DrawLine(s_loc, locations[i], Color.white, trail_time);
            }
        } else {
            // only the center body acting on other bodies
            for(int j = 1; j < n; j++)
                forces[j] -= masses[j] * (locations[j] - locations[0]) / Mathf.Pow(((locations[j] - locations[0]).magnitude), 3);

            for (int i = 0; i < n; i++) {
                Vector3 s_loc = locations[i];

                ComputeVelocity(i);
                ComputeLocation(i);
                // Debug.Log("Body[" + i + "]: " + s_loc + " ---> " + locations[i]);
                // Debug.DrawLine(s_loc, locations[i], Color.white, trail_time);
            }
        }
	}

    /// <summary>
    /// Computes the sum of all forces working on body with index i,
    /// by applying Newthon's method. The resulting sum will be directly
    /// applied to the forces[i] of the body. The opposing forces will be
    /// applied to the correcponding forces[j] as well, to make the algorithm
    /// faster
    /// </summary>
    /// <param name="i">Index of the body on which forces will be computed</param>
    void ComputeForce(int i) {
        Vector3 sum = Vector3.zero;
        for(int j = 0; j < n; j++) {
            
            if(i != j) {
                Vector3 temp = masses[j] * (locations[j] - locations[i]) / Mathf.Pow(((locations[j] - locations[i]).magnitude), 3);

                // if (i != 0) forces[i] += temp;
                // if (j != 0) forces[j] -= temp;

                // if (i != 0)
                    sum += temp;
            }
        }
        forces[i] = sum;
    }

    /// <summary>
    /// Computes the changing velocity of body with index i, by applying the step DT
    /// in direction of force forces[i]
    /// </summary>
    /// <param name="i">Index of the body on which the forces will be applied</param>
    void ComputeVelocity(int i) {
        velocities[i] += DT * forces[i];
        // bodies[i].GetComponent<MeshRenderer>().material.Barva = velocities[i].magnitude;    // TODO
        bodies[i].GetComponent<Renderer>().material.SetFloat("Vector1_D2FF331E",velocities[i].x);
        bodies[i].GetComponent<Renderer>().material.SetFloat("Vector1_ED68406D", velocities[i].y);
        bodies[i].GetComponent<Renderer>().material.SetFloat("Vector1_1F8FEA63", velocities[i].z);
    }

    /// <summary>
    /// Computes the changing location of body with index i, by applying the step DT
    /// in direction of velocity velocities[i].
    /// This also updates the locations of the bodies[i]
    /// </summary>
    /// <param name="i">Index of the body on which the velocity will be applied</param>
    void ComputeLocation(int i) {
        locations[i] += DT * velocities[i];
        bodies[i].transform.position = locations[i];
    }

    /// <summary>
    /// Reads 3 integers from a previously read line, split into an array of strings data
    /// </summary>
    /// <param name="data">array, containing the data, from which the Vector will be created</param>
    /// <returns>Vector3 read from input</returns>
    Vector3 ReadVector(string[] data) {
        
        // better solution
        return new Vector3(
            float.Parse(data[0].Replace('.', ',')),
            float.Parse(data[1].Replace('.', ',')),
            float.Parse(data[2].Replace('.', ','))
        );
        /*
        return new Vector3(
            float.Parse(data[0])/100000,
            float.Parse(data[1])/100000,
            float.Parse(data[2])/100000
        );
        */
    }
}
