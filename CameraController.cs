using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float min_x, max_x, first_rotation, speed_x;
    [SerializeField]
    int w = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(min_x, transform.position.y, transform.position.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, first_rotation, transform.eulerAngles.z);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x  + w*speed_x, transform.position.y, transform.position.z);
        if(transform.position.x>=this.max_x | transform.position.x<=this.min_x)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            w *= -1;
        }
        
    }
}
