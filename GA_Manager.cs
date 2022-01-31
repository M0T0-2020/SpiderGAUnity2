using cs_system = System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class RandomUtils
{
    public static FootDna[] Choice(this cs_system.Random rnd,
                    IEnumerable<FootDna> choices, IEnumerable<float> weights,
                    int k = 1, bool is_replacement = true)
    {
        var cumulativeWeight= new List<float>();
        float last= 0;
        foreach (var cur in weights)
        {
            last += cur;
            cumulativeWeight.Add(last);
        }
        FootDna[] SelectDnaArray = new FootDna[k];
        List<int> SelectedIndexArray = new List<int>();
        int cnt = 0;
        while(cnt<k)
        {
            double choice = rnd.NextDouble();
            int i= 0;
            foreach (var cur in choices)
            {
                if (choice < cumulativeWeight[i])
                {
                    if(is_replacement)
                    {
                        SelectDnaArray[cnt] = cur;
                        SelectedIndexArray.Add(i);
                        cnt += 1;
                    }
                    else
                    {
                        bool flag = true;
                        foreach (int idx in SelectedIndexArray)
                        {
                            if(idx==i)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if(flag)
                        {
                            SelectDnaArray[cnt] = cur;
                            SelectedIndexArray.Add(i);
                            cnt += 1;
                            break;
                        }
                    }

                    break;
                }
                i++;
            }
        }
        return SelectDnaArray;
    }
}

public class ExpCalulator
{
    private float t;
    public ExpCalulator(float t)
    {
        this.t = t;    
    }

    public float Exp(float x)
    {
        float value = Mathf.Exp(x/this.t);
        return value;
    }
}

public class GA_Manager : MonoBehaviour
{
    public GameObject spider_manager;
    public float mutation_rate_angle;
    public float mutation_rate_speed;
    public float mutation_rate_w;
    public float mutation_rate_color;

    private int num_Managers;
    
    public float Range_x = 20;
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
    
    private int mean_cnt = 0;
    public int mean_num = 1;

    public float prevMaxScore = (float)-1e10;
    
    float MaxScore = (float)-1e10;
    public enum GAName {SGA, IGS, SS, CHC, ER, MGG}
    [SerializeField]
    GAName GA_Name = GAName.SGA;

    private SpiderManager spider_m_sc;

    public float exp_t = 5;
    ExpCalulator exp_with_t;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spider_m = Instantiate(this.spider_manager) as GameObject;
        this.spider_m_sc = spider_m.GetComponent<SpiderManager>();

        spider_m.name = "Spider Manager " + GA_Name.ToString();

        spider_m_sc.num_spider = this.num_spider;

        spider_m_sc.mutation_rate_angle = this.mutation_rate_angle;
        spider_m_sc.mutation_rate_speed = this.mutation_rate_speed;
        spider_m_sc.mutation_rate_w = this.mutation_rate_w;
        spider_m_sc.mutation_rate_color = this.mutation_rate_color;

        spider_m_sc.angle_max = this.angle_max;
        spider_m_sc.angle_max = this.angle_max;
        spider_m_sc.half_speed_max = this.half_speed_max;
        spider_m_sc.half_speed_min = this.half_speed_min;

        spider_m_sc.Range_x = Range_x;
        spider_m_sc.start_pos_z = 0;

        spider_m_sc.seed = this.seed;
        spider_m_sc.is_Debug = this.is_Debug;

        spider_m_sc.CreateManager();

        exp_with_t = new ExpCalulator(exp_t);
    }

    // Update is called once per frame
    void Update()
    {
        this.acc_time += Time.deltaTime;
        if(this.acc_time >= this.trail_time)
        {
            this.mean_cnt += 1;
            if(this.mean_cnt<this.mean_num)
            {
                this.spider_m_sc.cal_scores();
            }
            else
            {
                Cal_Result_Data data = this.spider_m_sc.get_result();
                
                if(GA_Name.ToString()=="SGA")
                {
                    SGA(data);
                }
                
                this.mean_cnt = 0;
            }
            this.acc_time = 0;
            this.trail_cnt += 1;
        }
    }

    public DNA_set CrossOver(FootDna FootDna1, FootDna FootDna2)
    {
        dna_a dna1 = CrossOverUtil.get(FootDna1.dna1, FootDna2.dna1);
        dna_a dna2 = CrossOverUtil.get(FootDna1.dna2, FootDna2.dna2);
        dna_a dna3 = CrossOverUtil.get(FootDna1.dna3, FootDna2.dna3);

        DNA_set dna;
        dna.dna1 = dna1;
        dna.dna2 = dna2;
        dna.dna3 = dna3;
        return dna;
    }

    void SGA(Cal_Result_Data data)
    {
        FootDna[] SortedFootDnaArray  = data.footdnaArray;
        float[] SortedScoreArray = data.ScoreArray;
        float[] SelectionWeightArray = new float[SortedScoreArray.Length];
        float ScoreSum = 0;
        float MaxScore = (float)-1e10;

        for(int i=0; i<SortedScoreArray.Length; i++)
        {
            if(MaxScore < SortedScoreArray[i])
            {
                MaxScore = SortedScoreArray[i];
            }
            
            SelectionWeightArray[i] = exp_with_t.Exp(SortedScoreArray[i]);
            ScoreSum += SelectionWeightArray[i];
        }
        for(int i=0; i<SortedScoreArray.Length; i++)
        {
            SelectionWeightArray[i] /= ScoreSum;
        }

        Debug.Log(MaxScore);

        DNA_set[] ChildrenArray = new DNA_set[SortedFootDnaArray.Length];
        cs_system.Random rnd= new cs_system.Random();
        for (int i = 0; i < SortedFootDnaArray.Length; i++)
        {
            FootDna[] SelectDnaArray = rnd.Choice(SortedFootDnaArray, SelectionWeightArray, 2, false);
            DNA_set dna_set = CrossOver(SelectDnaArray[0], SelectDnaArray[1]);
            ChildrenArray[i] = dna_set;
        }
        int seed = Random.Range(0,10000);
        this.spider_m_sc.ResetDnaArray(ChildrenArray, seed);
    }

    public float get_acc_time()
    {
        return this.acc_time;
    }
}
