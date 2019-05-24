using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ustvari : MonoBehaviour
{
    public GameObject zvezda;
    GameObject stars;
    // Start is called before the first frame update
    void Start()
    {
        stars = new GameObject();
        
        for (int i = 1; i < 500001; i++) {
            //var zacZvezda = zvezda;

            //zacZvezda.transform.position = new Vector3(Random.Range(-5000f, 5000f), Random.Range(-5000f, 5000f), 0);

            GameObject sin = Instantiate(zvezda, new Vector3(Random.Range(-5000f, 5000f), Random.Range(-5000f, 5000f), 0), Quaternion.identity);

            sin.transform.parent = stars.transform;

            //zacZvezda.GetComponent<Spinn>().radij = i *0.001f;
            //zacZvezda.GetComponent<Spinn>().hitrost = Random.Range(0.5f, 10f);
            
        }
        Instantiate(stars, new Vector3(0, 0, 0), Quaternion.identity);
    }
    float kot = 0f;
    public float hitrost = (2 * Mathf.PI) / 10;
    public float radij = 5;
    void Update()

    {
        kot += hitrost * Time.deltaTime;
        stars.transform.position = new Vector3(Mathf.Cos(kot) * radij,
        Mathf.Sin(kot) * radij, stars.transform.position.z);

    }
}
