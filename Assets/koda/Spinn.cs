using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinn : MonoBehaviour
{
    public Transform trans;
    float kot = 0f;
    public float hitrost = (2 * Mathf.PI) / 10;
    public float radij = 5;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        kot += hitrost * Time.deltaTime;
        trans.position = new Vector3(Mathf.Cos(kot) * radij,
        Mathf.Sin(kot) * radij, trans.position.z);
        
    }
}
