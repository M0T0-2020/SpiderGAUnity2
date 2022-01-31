using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_checker : MonoBehaviour
{
    public GameObject Spider;
    Spider_Controller sp_cont;
    // Start is called before the first frame update
    void Start()
    {
        this.sp_cont = Spider.GetComponent<Spider_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "floor")
        {
            this.sp_cont.stop_move();
            //Debug.Log("hit!!!");
        }
    }
}
