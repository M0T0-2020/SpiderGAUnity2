using cs_system = System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MutationRates
{
    public float mutation_rate_angle;
    public float mutation_rate_speed;
    public float mutation_rate_w;
    public float mutation_rate_color;
}

public class GA_Manager1 : MonoBehaviour
{
    public GameObject spider_manager;
    public float mutation_rate_angle;
    public float mutation_rate_speed;
    public float mutation_rate_w;
    public float mutation_rate_color;

    private int num_Managers;
    
    public float Range_x = 20;
    public float Range_z = 200;

    public bool is_Debug;
    public int seed = 42;
    public int num_spider = 150;

    public float trail_time = 120f;
    public int trail_num = 50;
    public int save_spider_num = 3;

    private int cnt_trail;
    [SerializeField]
    private float acc_time;

    public int angle_max = 180;
    public int angle_min = 10;
    public int half_speed_max = 50;
    public int half_speed_min = 15;
    private int trail_cnt = 1;
    private int mean_cnt = 1;
    public int mean_num = 1;


    public float[] prevScoreArray;
    public FootDna[] prevFootDnaArray;
    public float prevMaxScore = (float)-1e10;
    public enum GAName {SGA, IGS, SS, CHC, ER, MGG}
    [SerializeField]
    GAName ga_name = GAName.SGA;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spider_m = Instantiate(spider_manager) as GameObject;
        SpiderManager spider_m_sc = spider_m.GetComponent<SpiderManager>();
        
        spider_m.name = "GAM_" + this.ga_name;
        spider_m_sc.name = "GAM_" + this.ga_name;
        spider_m_sc.num_spider = this.num_spider;

        spider_m_sc.save_spider_num = this.save_spider_num;
        spider_m_sc.mutation_rate_angle = this.mutation_rate_angle;
        spider_m_sc.mutation_rate_speed = this.mutation_rate_speed;
        spider_m_sc.mutation_rate_w = this.mutation_rate_w;
        spider_m_sc.mutation_rate_color = this.mutation_rate_color;

        spider_m_sc.angle_max = this.angle_max;
        spider_m_sc.angle_max = this.angle_max;
        spider_m_sc.half_speed_max = this.half_speed_max;
        spider_m_sc.half_speed_min = this.half_speed_min;

        spider_m_sc.Range_x = Range_x;
        spider_m_sc.start_pos_z = Range_z;

        spider_m_sc.seed = this.seed;
        spider_m_sc.is_Debug = this.is_Debug;

        spider_m_sc.CreateManager();
    }

    // Update is called once per frame
    void Update()
    {
        acc_time += Time.deltaTime;
        if(acc_time >= trail_time)
        {
            //acc_time = 0;
            //trail_cnt += 1;
        }
    }
}
