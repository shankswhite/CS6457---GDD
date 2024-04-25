using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Waypoint_AI : MonoBehaviour
{
    public float min = 2f;
    public float max = 3f;
    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.z;
        max = transform.position.z + 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * 2, max - min) + min);
        //transform.position = new Vector3(Mathf.PingPong(xxxxxx) + min, transform.position.y, transform.position.z);
 
    }

}
