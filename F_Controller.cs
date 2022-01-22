using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Controller : MonoBehaviour
{
    //public Transform point1;
    public Vector3 start_position;
    public Quaternion start_rotation;

    public Renderer renderer1;
    
    [HideInInspector]
    public float rotation_angle_x, rotation_angle_z;
    [HideInInspector]
    public float half_speed;
    private float speed;
    //y方向はいみがない
    [HideInInspector]
    public Color32 color1;
    private int cnt_x = 0;
    private int cnt_z = 0;
    private bool first = true;
    [HideInInspector]
    public float w_x = 1;
    [HideInInspector]
    public float w_z = -1;

    public bool is_Debug = false;

    //private float f_r_a_x, f_r_a_z, f_c_x, f_c_z, f_w_x, f_w_z; 
    // Start is called before the first frame update

    void Start()
    {
        if(is_Debug)
        {
        Debug.Log("transform.position.x :" + transform.position.x );
        Debug.Log("transform.position.y :" + transform.position.y );
        Debug.Log("transform.position.z :" + transform.position.z );
        
        Debug.Log("transform.localPosition.x :" + transform.localPosition.x );
        Debug.Log("transform.localPosition.y :" + transform.localPosition.y );
        Debug.Log("transform.localPosition.z :" + transform.localPosition.z );
        }
        start_position = transform.localPosition;
        //start_position = transform.position;

        start_rotation = transform.localRotation;
        //start_rotation = transform.rotation;
        //renderer1.material.color = color1;
        speed = 2*half_speed;

        if(is_Debug)
        {
        Debug.Log(w_x);
        Debug.Log(w_z);
        Debug.Log(rotation_angle_x);
        }
    }

    public void ResetTransform()
    {
        //this.transform.position = this.start_position;
        this.transform.localPosition = this.start_position;
        
        //this.transform.rotation = this.start_rotation;
        this.transform.localRotation = this.start_rotation;

        this.cnt_x = 0;
        this.cnt_z = 0;
        this.first = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!first)
        {
            cnt_z += 1;
            cnt_x += 1;

            Vector3 vec = new Vector3(w_x*rotation_angle_x/speed, 0.0f, w_z*rotation_angle_z/speed);
            
            if(is_Debug){
            Debug.Log(vec);
            Debug.Log(w_x);
            Debug.Log(rotation_angle_x);
            Debug.Log(speed);
            }
            transform.Rotate(vec);
            
            if(speed/2<=cnt_z)
            {
                cnt_z = 0;
                w_z *= -1;
            }

            if(speed<=cnt_x)
            {
                cnt_x = 0;
                w_x *= -1;
            }

        }
        else
        {
            cnt_z += 1;
            cnt_x += 1;

            Vector3 vec = new Vector3(w_x*rotation_angle_x/speed, 0.0f, w_z*rotation_angle_z/speed);
            if(is_Debug){
            Debug.Log(vec);
            Debug.Log(rotation_angle_x);

            }
            transform.Rotate(vec);

            if(speed/2<=cnt_z)
            {
                cnt_x = 0;
                w_x *= -1;
                
                cnt_z = 0;
                w_z *= -1;

                first = false;

            }
        }

    }
}
