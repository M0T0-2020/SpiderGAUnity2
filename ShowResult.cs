using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResult : MonoBehaviour
{
    public float cal_time;
    [SerializeField]
    float t;
    public Vector3 startP;
    [SerializeField]
    List<float> scoreList = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        startP = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t>=cal_time)
        {
        
            float x_d = Mathf.Pow(transform.position.x - startP.x, 2);
            float z_d = Mathf.Pow(transform.position.z - startP.z, 2);
            float score = x_d + z_d;
            scoreList.Add(score);
            startP = transform.position;
            t = 0;
        }
    }
}
