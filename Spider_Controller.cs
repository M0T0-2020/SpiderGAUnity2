using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System_SC = System;

public class Spider_Controller : MonoBehaviour
{
    //public GameObject;  
    //public Dictionary<string, GameObject[] > feet = new Dictionary<string, GameObject[] >();
    public GameObject spider_foot_0, spider_foot_1, spider_foot_2, spider_foot_3, spider_foot_4, spider_foot_5;

    public Vector3 start_pos, start_rotation;

    public bool is_rotaion = false;

    //Transform foot1, t_1, capsule;

    public bool is_Debug = false;
    public float cos_value = 0;
    public float arg_value = 0;

    public Gene gene;

    [HideInInspector]
    public Color32 f1_1_color, f1_2_color, f1_3_color, f2_1_color, f2_2_color, f2_3_color, f3_1_color, f3_2_color, f3_3_color;
    
    [Header("前足の第1関節 パラメータ")]
    public float f1_1_angle_x, f1_1_angle_z, f1_1_half_speed, w_x_1_1, w_z_1_1;
    
    [Header("前足の第2関節 パラメータ")]
    public float f1_2_angle_x, f1_2_angle_z, f1_2_half_speed, w_x_1_2, w_z_1_2;
    [Header("前足の第3関節 パラメータ")]
    public float f1_3_angle_x, f1_3_angle_z, f1_3_half_speed, w_x_1_3, w_z_1_3;
    
    
    [Header("中足の第1関節 パラメータ")]
    public float f2_1_angle_x, f2_1_angle_z, f2_1_half_speed, w_x_2_1, w_z_2_1;
    [Header("中足の第2関節 パラメータ")]
    public float f2_2_angle_x, f2_2_angle_z, f2_2_half_speed, w_x_2_2, w_z_2_2;
    [Header("中足の第3関節 パラメータ")]
    public float f2_3_angle_x, f2_3_angle_z, f2_3_half_speed, w_x_2_3, w_z_2_3;

    [Header("後足の第1関節 パラメータ")]
    public float f3_1_angle_x, f3_1_angle_z, f3_1_half_speed, w_x_3_1, w_z_3_1;
    [Header("後足の第2関節 パラメータ")]
    public float f3_2_angle_x, f3_2_angle_z, f3_2_half_speed, w_x_3_2, w_z_3_2;
    [Header("後足の第3関節 パラメータ")]
    public float f3_3_angle_x, f3_3_angle_z, f3_3_half_speed, w_x_3_3, w_z_3_3;
    // Start is called before the first frame update
    public void Start()
    {
        //Debug.Log(transform.name);
        
        string base_name = transform.name;
        gene = new Gene( base_name,
        spider_foot_0, spider_foot_3,
        spider_foot_1, spider_foot_4,
        spider_foot_2, spider_foot_5);
        
        gene.set_gene(1, 
        f1_1_angle_x, f1_2_angle_x, f1_3_angle_x,
        f1_1_angle_z, f1_2_angle_z, f1_3_angle_z,
        f1_1_half_speed, f1_2_half_speed, f1_3_half_speed,
        w_x_1_1, w_x_1_2, w_x_1_3,
        w_z_1_1, w_z_1_2, w_z_1_3,
        f1_1_color, f1_2_color, f1_3_color,
        false
        );

        gene.set_gene(2,
        f2_1_angle_x, f2_2_angle_x, f2_3_angle_x,
        f2_1_angle_z, f2_2_angle_z, f2_3_angle_z,
        f2_1_half_speed, f2_2_half_speed, f2_3_half_speed,
        w_x_2_1, w_x_2_2, w_x_2_3,
        w_z_2_1, w_z_2_2, w_z_2_3,
        f2_1_color, f2_2_color, f2_3_color,
        false
        );

        gene.set_gene(3,
        f3_1_angle_x, f3_2_angle_x, f3_3_angle_x,
        f3_1_angle_z, f3_2_angle_z, f3_3_angle_z,
        f3_1_half_speed, f3_2_half_speed, f3_3_half_speed,
        w_x_3_1, w_x_3_2, w_x_3_3,
        w_z_3_1, w_z_3_2, w_z_3_3,
        f3_1_color, f3_2_color, f3_3_color,
        false
        );

        start_pos = transform.position;
        start_rotation = transform.localEulerAngles;
        is_rotaion = false;
        if(is_Debug){
            Debug.Log(start_pos);
           //Debug.Log( "gene.foot1.g1_2.half_speed : " + gene.foot1.g1_2.half_speed );
           //Debug.Log( "gene.foot1.g1_1.rotation_angle_x : " + gene.foot1.g1_1.rotation_angle_x );
        }
        
    }

    public void Reset(bool is_Transform)
    {
        string base_name = transform.name;
        gene = new Gene( base_name,
        spider_foot_0, spider_foot_3,
        spider_foot_1, spider_foot_4,
        spider_foot_2, spider_foot_5);
        
        gene.set_gene(1, 
        f1_1_angle_x, f1_2_angle_x, f1_3_angle_x,
        f1_1_angle_z, f1_2_angle_z, f1_3_angle_z,
        f1_1_half_speed, f1_2_half_speed, f1_3_half_speed,
        w_x_1_1, w_x_1_2, w_x_1_3,
        w_z_1_1, w_z_1_2, w_z_1_3,
        f1_1_color, f1_2_color, f1_3_color,
        is_Transform
        );

        gene.set_gene(2,
        f2_1_angle_x, f2_2_angle_x, f2_3_angle_x,
        f2_1_angle_z, f2_2_angle_z, f2_3_angle_z,
        f2_1_half_speed, f2_2_half_speed, f2_3_half_speed,
        w_x_2_1, w_x_2_2, w_x_2_3,
        w_z_2_1, w_z_2_2, w_z_2_3,
        f2_1_color, f2_2_color, f2_3_color,
        is_Transform
        );

        gene.set_gene(3,
        f3_1_angle_x, f3_2_angle_x, f3_3_angle_x,
        f3_1_angle_z, f3_2_angle_z, f3_3_angle_z,
        f3_1_half_speed, f3_2_half_speed, f3_3_half_speed,
        w_x_3_1, w_x_3_2, w_x_3_3,
        w_z_3_1, w_z_3_2, w_z_3_3,
        f3_1_color, f3_2_color, f3_3_color,
        is_Transform
        );

        start_pos = transform.position;
        start_rotation = transform.localEulerAngles;
        is_rotaion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(is_Debug)
        {
        Debug.Log( "gene.foot1.g1_1.w_x : " + gene.foot1.g1_1.w_x );
        Debug.Log( "gene.foot1.g1_1.rotation_angle_x : " + gene.foot1.g1_1.rotation_angle_x );
        //g.transform.Rotate( 12.0f, 0f ,0f );
        }
    }
}


public class Gene
{
    public Gene_mini foot1, foot2, foot3;

    public Gene (string base_name, GameObject f1_1, GameObject f1_2, GameObject f2_1, GameObject f2_2, GameObject f3_1, GameObject f3_2)
    {
        //クラスをインスタンス化するときは new class-name で定義
        this.foot1 = new Gene_mini(base_name, f1_1, f1_2);
        this.foot2 = new Gene_mini(base_name, f2_1, f2_2);
        this.foot3 = new Gene_mini(base_name, f3_1, f3_2);
    }
    public void set_gene(int _type, float f1_angle_x, float f2_angle_x, float f3_angle_x,
                        float f1_angle_z, float f2_angle_z, float f3_angle_z,
                        float f1_half_speed, float f2_half_speed, float f3_half_speed,
                        float f1_w_x, float f2_w_x, float f3_w_x,
                        float f1_w_z, float f2_w_z, float f3_w_z,
                        Color32 f1_color, Color32 f2_color, Color32 f3_color ,
                        bool is_Transform
                        )
    {
        if(_type == 1)
        {
            foot1.set_gene(f1_angle_x, f2_angle_x, f3_angle_x,
                            f1_angle_z, f2_angle_z, f3_angle_z,
                            f1_half_speed, f2_half_speed, f3_half_speed,
                            f1_w_x, f2_w_x, f3_w_x,
                            f1_w_z, f2_w_z, f3_w_z,
                            f1_color, f2_color, f3_color,
                            is_Transform
                            );
        }

        if(_type == 2)
        {
            foot2.set_gene(f1_angle_x, f2_angle_x, f3_angle_x,
                            f1_angle_z, f2_angle_z, f3_angle_z,
                            f1_half_speed, f2_half_speed, f3_half_speed,
                            f1_w_x, f2_w_x, f3_w_x,
                            f1_w_z, f2_w_z, f3_w_z,
                            f1_color, f2_color, f3_color,
                            is_Transform
                            );
        }

        if(_type == 3)
        {
            foot3.set_gene(f1_angle_x, f2_angle_x, f3_angle_x,
                            f1_angle_z, f2_angle_z, f3_angle_z,
                            f1_half_speed, f2_half_speed, f3_half_speed,
                            f1_w_x, f2_w_x, f3_w_x,
                            f1_w_z, f2_w_z, f3_w_z,
                            f1_color, f2_color, f3_color,
                            is_Transform
                            );
        }
    }
}

public class Gene_mini
{
    public F_Controller g1_1, g1_2, g2_1, g2_2, g3_1, g3_2;
    public Gene_mini (string base_name, GameObject f1, GameObject f2)
    {
        string f1_name = base_name +"/"+ "body/" + f1.name;
        string f2_name = base_name +"/"+ "body/" + f2.name;
        
        //Debug.Log("f1_name: "+ f1_name);
        //Debug.Log("f2_name: "+ f2_name);

        this.g1_1 = GameObject.Find(f1_name+"/GameObject(1)").GetComponent<F_Controller>();
        this.g2_1 = GameObject.Find(f1_name+"/GameObject(1)/GameObject(2)").GetComponent<F_Controller>();
        this.g3_1 = GameObject.Find(f1_name+"/GameObject(1)/GameObject(2)/GameObject(3)").GetComponent<F_Controller>();
    
        this.g1_2 = GameObject.Find(f2_name+"/GameObject(1)").GetComponent<F_Controller>();
        this.g2_2 = GameObject.Find(f2_name+"/GameObject(1)/GameObject(2)").GetComponent<F_Controller>();
        this.g3_2 = GameObject.Find(f2_name+"/GameObject(1)/GameObject(2)/GameObject(3)").GetComponent<F_Controller>();
    }

    public void debug_transform()
    {
        Debug.Log("position x: " +this.g1_1.transform.position.x + " y: " + 
        this.g1_1.transform.position.y + " z: " + this.g1_1.transform.position.z);
        Debug.Log("eulerAngles x: " +this.g1_1.transform.eulerAngles.x + " y: " + 
        this.g1_1.transform.eulerAngles.y + " z: " + this.g1_1.transform.eulerAngles.z);
    }

    public void set_gene(float f1_angle_x, float f2_angle_x, float f3_angle_x,
                             float f1_angle_z, float f2_angle_z, float f3_angle_z,
                                float f1_half_speed, float f2_half_speed, float f3_half_speed,
                                float f1_w_x, float f2_w_x, float f3_w_x,
                                float f1_w_z, float f2_w_z, float f3_w_z,
                                Color32 f1_color, Color32 f2_color, Color32 f3_color,
                                bool is_Transform
                                )
    {
        // transform
        if(is_Transform)
        {
        g1_1.ResetTransform();
        g1_2.ResetTransform();
        g2_1.ResetTransform();
        g2_2.ResetTransform();
        g3_1.ResetTransform();
        g3_2.ResetTransform();
        }

        // rotation x
        g1_1.rotation_angle_x = f1_angle_x;
        g1_2.rotation_angle_x = f1_angle_x;

        g2_1.rotation_angle_x = f2_angle_x;
        g2_2.rotation_angle_x = f2_angle_x;

        g3_1.rotation_angle_x = f3_angle_x;
        g3_2.rotation_angle_x = f3_angle_x;

        // rotation z
        g1_1.rotation_angle_z = f1_angle_z;
        g1_2.rotation_angle_z = f1_angle_z;

        g2_1.rotation_angle_z = f2_angle_z;
        g2_2.rotation_angle_z = f2_angle_z;

        g3_1.rotation_angle_z = f3_angle_z;
        g3_2.rotation_angle_z = f3_angle_z;

        // half speed
        g1_1.half_speed = f1_half_speed;
        g1_2.half_speed = f1_half_speed;
        
        g2_1.half_speed = f2_half_speed;
        g2_2.half_speed = f2_half_speed;

        g3_1.half_speed = f3_half_speed;
        g3_2.half_speed = f3_half_speed;

        // w_x
        g1_1.w_x = f1_w_x;
        g1_2.w_x = f1_w_x;
        
        g2_1.w_x = f2_w_x;
        g2_2.w_x = f2_w_x;
        
        g3_1.w_x = f3_w_x;
        g3_2.w_x = f3_w_x;

        // w_z
        g1_1.w_z = f1_w_z;
        g1_2.w_z = f1_w_z;
        
        g2_1.w_z = f2_w_z;
        g2_2.w_z = f2_w_z;
        
        g3_1.w_z = f3_w_z;
        g3_2.w_z = f3_w_z;

        //color
        g1_1.renderer1.material.color = f1_color;
        g1_2.renderer1.material.color = f1_color;

        g2_1.renderer1.material.color = f2_color;
        g2_2.renderer1.material.color = f2_color;
        
        g3_1.renderer1.material.color = f3_color;
        g3_2.renderer1.material.color = f3_color;

    }

}

[System_SC.SerializableAttribute]
public class DnaDataForSave {
    public Dictionary<string, float> data1_1, data1_2, data2_1, data2_2, data3_1, data3_2;
    //public 

}