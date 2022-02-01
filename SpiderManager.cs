using System_sc = System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderManager : MonoBehaviour
{
    public string core_name;
    public int seed;
    public bool is_Debug;
    public float start_pos_z = 0;
    public float Range_x = 20;
    public GameObject Spider;
    public int num_spider = 50;
    public float mutation_rate_angle = 0.5f;
    public float mutation_rate_speed = 0.5f;
    public float mutation_rate_w = 0.5f;
    public float mutation_rate_color = 0.01f;

    public int angle_max = 180;
    public int angle_min = 10;
    public int half_speed_max = 50;
    public int half_speed_min = 15;

    [SerializeField]
    private GameObject[] SpiderArray;
    private FootDna[] FootDnaArray;
    private float[] ScoreArray;
    //private bool is_rotation = false;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void CreateManager()
    {
        Random.InitState(this.seed);
        this.SpiderArray = new GameObject[this.num_spider];
        this.FootDnaArray = new FootDna[this.num_spider];
        this.ScoreArray = new float[this.num_spider];

        for (int i = 0; i < this.num_spider; i++)
        {
            GameObject _spider = Instantiate(Spider) as GameObject;
            Spider_Controller SC = _spider.GetComponent<Spider_Controller>();
            _spider.name = this.core_name + "_" + i;

            //Random.InitState(seed);

            float px = this.Range_x*i;
            if(is_Debug)
            {
            Debug.Log("px: " + px);
            }
            _spider.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            _spider.transform.position = new Vector3(px, 1.5f, this.start_pos_z);
            dna_a dna1 = new dna_a(angle_max:this.angle_max, angle_min:this.angle_min,
                        half_speed_max:this.half_speed_max, half_speed_min:this.half_speed_min,
                        mutation_rate_angle:mutation_rate_angle, mutation_rate_speed:mutation_rate_speed,
                        mutation_rate_w:mutation_rate_w, mutation_rate_color:mutation_rate_color);
            dna_a dna2 = new dna_a(angle_max:this.angle_max, angle_min:this.angle_min,
                        half_speed_max:this.half_speed_max, half_speed_min:this.half_speed_min,
                        mutation_rate_angle:mutation_rate_angle, mutation_rate_speed:mutation_rate_speed,
                        mutation_rate_w:mutation_rate_w, mutation_rate_color:mutation_rate_color);
            dna_a dna3 = new dna_a(angle_max:this.angle_max, angle_min:this.angle_min,
                        half_speed_max:this.half_speed_max, half_speed_min:this.half_speed_min,
                        mutation_rate_angle:mutation_rate_angle, mutation_rate_speed:mutation_rate_speed,
                        mutation_rate_w:mutation_rate_w, mutation_rate_color:mutation_rate_color);

            FootDna f_dna = new FootDna(SC, dna1, dna2, dna3);
            f_dna.set_gene(false);

            this.SpiderArray[i] = _spider;
            this.FootDnaArray[i] = f_dna;
            this.ScoreArray[i] = 0.0f;
        }
        if(is_Debug)
        {
            Debug.Log("Start");
        }
    }

    public void ResetDnaArray(DNA_set[] NewFootDnaSetArray, int seed)
    {
        Random.InitState(seed);
        //this.SpiderArray = new GameObject[num_spider];
        //this.FootDnaArray = new FootDna[num_spider];
        //this.ScoreArray = new float[num_spider];

        for (int i = 0; i < this.num_spider; i++)
        {
            DNA_set new_fdna = NewFootDnaSetArray[i];
            GameObject _spider = this.SpiderArray[i];
            Spider_Controller SC = _spider.GetComponent<Spider_Controller>();

            float px = this.Range_x*i;
            _spider.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            _spider.transform.position = new Vector3(px, 1.5f, this.start_pos_z);

            FootDna f_dna = new FootDna(SC, new_fdna.dna1, new_fdna.dna2, new_fdna.dna3);
            f_dna.set_gene(true);

            this.SpiderArray[i] = _spider;
            this.FootDnaArray[i] = f_dna;
            this.ScoreArray[i] = 0.0f;
        }
    }
    public void cal_scores()
    {
        for (int i = 0; i < this.SpiderArray.Length; i++)
        {
            GameObject tmp_spider = this.SpiderArray[i];
            FootDna tmp_fdna = this.FootDnaArray[i];

            //ひっくり返ったらゼロ
            if( tmp_fdna.spider_c.is_rotaion)
            {
                this.ScoreArray[i] += 0.0f;
            }
            else
            {
                float x_d = Mathf.Pow(tmp_spider.transform.position.x - tmp_fdna.spider_c.start_pos.x, 2);
                float z_d = Mathf.Pow(tmp_spider.transform.position.z - tmp_fdna.spider_c.start_pos.z, 2);
                float score = x_d + z_d;
                this.ScoreArray[i] += score;
            }

            Spider_Controller SC = tmp_spider.GetComponent<Spider_Controller>();
            float px = this.Range_x*i;
            tmp_spider.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            tmp_spider.transform.position = new Vector3(px, 1.5f, this.start_pos_z);
            FootDna f_dna = new FootDna(SC, tmp_fdna.dna1, tmp_fdna.dna2, tmp_fdna.dna3);
            f_dna.set_gene(true);
        }
    }

    public Cal_Result_Data get_result(bool sort)
    {
        for (int i = 0; i < this.SpiderArray.Length; i++)
        {
            GameObject tmp_spider = this.SpiderArray[i];
            FootDna tmp_fdna = this.FootDnaArray[i];

            //ひっくり返ったらゼロ
            if( tmp_fdna.spider_c.is_rotaion)
            {
                this.ScoreArray[i] += 0.0f;
            }
            else
            {
                float x_d = Mathf.Pow(tmp_spider.transform.position.x - tmp_fdna.spider_c.start_pos.x, 2);
                float z_d = Mathf.Pow(tmp_spider.transform.position.z - tmp_fdna.spider_c.start_pos.z, 2);
                float score = x_d + z_d;
                this.ScoreArray[i] += score;
            }
        }
        if(sort)
        {
            for (int i = 0; i < this.ScoreArray.Length; i++)
            {
                for (int k = i; k > 0; k--)
                {
                    float score_k_m1 = this.ScoreArray[k-1];
                    float score_k = this.ScoreArray[k];
                    if(score_k > score_k_m1)
                    {
                        this.ScoreArray[k-1] = score_k;
                        this.ScoreArray[k] = score_k_m1;

                        FootDna footdna_k = this.FootDnaArray[k];
                        this.FootDnaArray[k] = this.FootDnaArray[k-1];
                        this.FootDnaArray[k-1] = footdna_k;
                    }
                    else{break;}
                }
            }
        }
        Cal_Result_Data Data = new Cal_Result_Data();
        Data.ScoreArray = this.ScoreArray;
        Data.footdnaArray = this.FootDnaArray;
        return Data;
    }


    // Update is called once per frame
    void Update()
    {
    }
}