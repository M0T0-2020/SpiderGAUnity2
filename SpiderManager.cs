using System_sc = System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderManager : MonoBehaviour
{
    public string core_name;
    public int seed;
    public bool is_Debug;
    public bool is_elite_strategy;

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
    public int save_spider_num;

    [SerializeField]
    private GameObject[] SpiderArray;
    private FootDna[] FootDnaArray;
    private float[] ScoreArray;
    private FootDna[] SaveDnaArray;
    private float[] SaveScoreArray;

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

        this.SaveDnaArray = new FootDna[save_spider_num];
        this.SaveScoreArray = new float[save_spider_num];

        for (int i = 0; i < this.SpiderArray.Length; i++)
        {
            GameObject _spider = Instantiate(Spider) as GameObject;
            Spider_Controller SC = _spider.GetComponent<Spider_Controller>();
            _spider.name = this.core_name + "_" + i;

            //Random.InitState(seed);

            float px = this.Range_x*i;
            _spider.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            _spider.transform.position = new Vector3(px, 1.5f, this.start_pos_z);
            dna_a dna1 = new dna_a(this.angle_max, this.angle_min, this.half_speed_max, this.half_speed_min);
            dna_a dna2 = new dna_a(this.angle_max, this.angle_min, this.half_speed_max, this.half_speed_min);
            dna_a dna3 = new dna_a(this.angle_max, this.angle_min, this.half_speed_max, this.half_speed_min);

            FootDna f_dna = new FootDna(SC, dna1, dna2, dna3, this.mutation_rate_angle, this.mutation_rate_speed,
                                        this.mutation_rate_w, this.mutation_rate_color);
            f_dna.set_gene(false);

            this.SpiderArray[i] = _spider;
            this.FootDnaArray[i] = f_dna;
            this.ScoreArray[i] = 0.0f;
        }
        if(is_Debug)
        {
        Debug.Log("Start");
        this.SpiderArray[0].GetComponent<Spider_Controller>().gene.foot1.debug_transform();
        }
    }

    public void DestroySpiderGameObjects()
    {
        for (int i = 0; i < this.num_spider; i++)
        {
            Destroy(this.SpiderArray[i]);
        }
    }

    public void ResetDnaArray(FootDna[] SaveArray, int seed)
    {
        Random.InitState(seed);
        //this.SpiderArray = new GameObject[num_spider];
        //this.FootDnaArray = new FootDna[num_spider];
        //this.ScoreArray = new float[num_spider];

        for (int i = 0; i < this.num_spider; i++)
        {
            int idx = i%SaveArray.Length;
            FootDna save_f_dna = SaveArray[idx];

            //Debug.Log(save_f_dna.dna1.f1_color);
            save_f_dna.self_mutation();
            
            //Debug.Log(save_f_dna.dna1.f1_color);
            
            GameObject _spider = this.SpiderArray[i];
            Spider_Controller SC = _spider.GetComponent<Spider_Controller>();
            
            _spider.name = this.core_name + "_" + i;

            float px = -10000 + this.Range_x*i;
            _spider.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            _spider.transform.position = new Vector3(px, 1.5f, this.start_pos_z);

            FootDna f_dna = new FootDna(SC, save_f_dna.dna1, save_f_dna.dna2, save_f_dna.dna3, this.mutation_rate_angle,
                                            this.mutation_rate_speed, this.mutation_rate_w, this.mutation_rate_color);
            f_dna.set_gene(true);
            //Debug.Log(f_dna.dna1.f1_color);

            this.SpiderArray[i] = _spider;
            this.FootDnaArray[i] = f_dna;
            this.ScoreArray[i] = 0.0f;
        }

        if(is_elite_strategy)
        {
            for (int i = 0; i < SaveArray.Length; i++)
            {
                int idx = this.num_spider + i;
                FootDna save_f_dna = SaveArray[i];
                
                GameObject _spider = this.SpiderArray[idx];
                Spider_Controller SC = _spider.GetComponent<Spider_Controller>();
                
                _spider.name = this.core_name + "_" + idx;

                float px = -10000 + this.Range_x*idx;
                _spider.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
                _spider.transform.position = new Vector3(px, 1.5f, this.start_pos_z);

                FootDna f_dna = new FootDna(SC, save_f_dna.dna1, save_f_dna.dna2, save_f_dna.dna3, this.mutation_rate_angle,
                                            this.mutation_rate_speed, this.mutation_rate_w, this.mutation_rate_color);
                f_dna.set_gene(true);

                this.SpiderArray[idx] = _spider;
                this.FootDnaArray[idx] = f_dna;
                this.ScoreArray[idx] = 0.0f;
            }   
        }

        if(is_Debug)
        {
        Debug.Log("Second");
        this.SpiderArray[0].GetComponent<Spider_Controller>().gene.foot1.debug_transform();
        }
    }
    public void cal_scores()
    {
        for (int i = 0; i < this.SpiderArray.Length; i++)
        {
            GameObject tmp_spider = this.SpiderArray[i];
            FootDna tmp_fdna = this.FootDnaArray[i];

            //ひっくり返ったらゼロ
            if( Mathf.Abs( tmp_fdna.spider_c.start_rotation.x - tmp_spider.transform.eulerAngles.x) > 130.0f)
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
            float px = -10000 + this.Range_x*i;
            tmp_spider.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            tmp_spider.transform.position = new Vector3(px, 1.5f, this.start_pos_z);
            FootDna f_dna = new FootDna(SC, tmp_fdna.dna1, tmp_fdna.dna2, tmp_fdna.dna3, this.mutation_rate_angle,
                                            this.mutation_rate_speed, this.mutation_rate_w, this.mutation_rate_color);
            f_dna.set_gene(true);
        }
    }

    public Cal_Result_Data get_result()
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

        for (int i = 0; i < this.save_spider_num; i++)
        {
            this.SaveDnaArray[i] = this.FootDnaArray[i];
            this.SaveScoreArray[i] = this.ScoreArray[i];
        }

        Cal_Result_Data Data = new Cal_Result_Data();
        Data.ScoreArray = this.SaveScoreArray;
        Data.footdnaArray = this.SaveDnaArray;
        return Data;
    }


    // Update is called once per frame
    void Update()
    {
    }
}