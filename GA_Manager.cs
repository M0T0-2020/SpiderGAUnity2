using cs_system = System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct FootDna_Index
{
    public FootDna[] dna;
    public int[] index;
}
static class RandomUtils
{
    public static int[] get_disjoint_int(this cs_system.Random rnd,
                                            int num, int minInclusive, int maxInclusive)
    {
        List<int> list = new List<int>(num);
        bool flag = true;
        while(flag)
        {
            int idx = rnd.Next(minInclusive, maxInclusive+1);
            if(!list.Contains(idx))
            {
                list.Add(idx);
                if(list.Count==num)
                {
                    flag = false;
                }
            }
        }
        return list.ToArray();
    }

    public static FootDna_Index Choice(this cs_system.Random rnd,
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
        FootDna_Index data;
        data.dna = SelectDnaArray;
        data.index = SelectedIndexArray.ToArray();
        return data;
    }
}

public class ExpCalulator
{
    private float t;
    public ExpCalulator(float t)
    {
        this.t = t;    
    }

    public void set_t_value(float t)
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
    FootDna[] StoreFootDnaArray;
    float[] StoreScoreArray;
    Dictionary<int, List<int>> FamilyGroupIndexDic;
    public GameObject spider_manager;
    public GameObject Data_manager_obj;
    private DataManager DataManager_sc;
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
    
    //float MaxScore = (float)-1e10;
    public enum GAName {SGA, IGS, SS, CHC, ER, MGG}
    [SerializeField]
    GAName GA_Name = GAName.SGA;

    private SpiderManager spider_m_sc;
    [SerializeField]
    float exp_t = -1;
    ExpCalulator exp_with_t;
    float[] RankingWeight = new float[4] {0.35f, 0.3f, 0.2f, 0.15f};


    // Start is called before the first frame update
    void Start()
    {
        this.DataManager_sc = Data_manager_obj.GetComponent<DataManager>();
        this.DataManager_sc.check_make_dir();

        //set num_spider to even value
        num_spider = 2*(int)(num_spider/2);
        
        StoreFootDnaArray = new FootDna[num_spider];
        StoreScoreArray = new float[num_spider];
        FamilyGroupIndexDic = new Dictionary<int, List<int>>(num_spider);

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
        exp_with_t = new ExpCalulator(this.exp_t);
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
                Cal_Result_Data data = this.spider_m_sc.get_result(sort:true);
                ScoreData scoredata = this.DataManager_sc.UpdateScore(data.ScoreArray);

                this.DataManager_sc.SaveDNAData(data.footdnaArray[0],
                                    this.trail_cnt.ToString()+"_"+this.GA_Name.ToString()+"_1.xml");
                this.DataManager_sc.SaveDNAData(data.footdnaArray[1],
                                    this.trail_cnt.ToString()+"_"+this.GA_Name.ToString()+"_2.xml");
                this.DataManager_sc.SaveDNAData(data.footdnaArray[2],
                                    this.trail_cnt.ToString()+"_"+this.GA_Name.ToString()+"_3.xml");
                this.DataManager_sc.SaveDNAData(data.footdnaArray[3],
                                    this.trail_cnt.ToString()+"_"+this.GA_Name.ToString()+"_4.xml");

                data.scoredata = scoredata;
                Debug.Log("MaxScore: " + scoredata.max + "  MeanScore:" + scoredata.mean);
                if(GA_Name.ToString()=="SGA")
                {    
                    SGA(data);
                }
                if(GA_Name.ToString()=="IGS")
                {
                    IGS(data);
                }
                if(GA_Name.ToString()=="SS")
                {
                    SS(data);
                }
                if(GA_Name.ToString()=="CHC")
                {
                    CHC(data);
                }
                if(GA_Name.ToString()=="ER")
                {
                    ER(data);
                }
                if(GA_Name.ToString()=="MGG")
                {
                    MGG(data);
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
        dna.mutate();
        return dna;
    }

    void SGA(Cal_Result_Data data)
    {
        FootDna[] SortedFootDnaArray  = data.footdnaArray;
        float[] SortedScoreArray = data.ScoreArray;
        float[] SelectionWeightArray = new float[SortedScoreArray.Length];
        int length = SortedFootDnaArray.Length;
        float ScoreSum = 0;

        // first index value is max        
        this.exp_with_t.set_t_value(0.5f*data.scoredata.max);
        
        for(int i=0; i<length; i++)
        {
            SelectionWeightArray[i] = exp_with_t.Exp(SortedScoreArray[i]);
            ScoreSum += SelectionWeightArray[i];
        }
        for(int i=0; i<length; i++)
        {
            SelectionWeightArray[i] /= ScoreSum;
        }

        DNA_set[] ChildrenArray = new DNA_set[SortedFootDnaArray.Length];
        cs_system.Random rnd = new cs_system.Random();
        for (int i = 0; i < (int)(length/2); i++)
        {
            FootDna[] SelectDnaArray = rnd.Choice(choices:SortedFootDnaArray, weights:SelectionWeightArray,
                                                                        k:2, is_replacement:false).dna;
            DNA_set dna_set1 = CrossOver(SelectDnaArray[0], SelectDnaArray[1]);
            DNA_set dna_set2 = CrossOver(SelectDnaArray[0], SelectDnaArray[1]);
            ChildrenArray[2*i] = dna_set1;
            ChildrenArray[2*i+1] = dna_set2;
        }
        int seed = Random.Range(0,10000);
        this.spider_m_sc.ResetDnaArray(ChildrenArray, seed);
    }

    void IGS(Cal_Result_Data data)
    {
        FootDna[] FootDnaArray  = data.footdnaArray;
        float[] ScoreArray = data.ScoreArray;
        int length = FootDnaArray.Length; 

        DNA_set[] ChildrenArray = new DNA_set[FootDnaArray.Length];

        cs_system.Random rnd = new cs_system.Random();
        for (int i = 0; i < length; i++)
        {
            if(ScoreArray[i]<data.scoredata.mean)
            {
                int[] idx_arry = rnd.get_disjoint_int(2, 0, this.num_spider-1);
                int idx1 = idx_arry[0];
                int idx2 = idx_arry[1];

                DNA_set dna_set = CrossOver(FootDnaArray[idx1], FootDnaArray[idx2]);
                ChildrenArray[i] = dna_set;
            }
            else
            {
                DNA_set dna_set = FootDnaArray[i].get_DNA_set();
                ChildrenArray[i] = dna_set;
            }
        }

        int seed = Random.Range(0,10000);
        this.spider_m_sc.ResetDnaArray(ChildrenArray, seed);
    }

    void SS(Cal_Result_Data data)
    {
        FootDna[] SortedFootDnaArray  = data.footdnaArray;
        float[] SortedScoreArray = data.ScoreArray;
        float[] SelectionWeightArray = new float[SortedScoreArray.Length];
        int length = SortedFootDnaArray.Length; 
        float MinScore = data.scoredata.min;

        this.exp_with_t.set_t_value(0.05f*num_spider);
        
        float s = 0;
        for (int i = 1; i < length+1; i++)
        {
            SelectionWeightArray[i-1] = this.exp_with_t.Exp(1/i);
            s += SelectionWeightArray[i-1];
        }
        
        for (int i=0; i < length; i++)
        {
            SelectionWeightArray[i] /= s;
        }

        DNA_set[] ChildrenArray = new DNA_set[SortedFootDnaArray.Length];
        cs_system.Random rnd = new cs_system.Random();
        for (int i = 0; i < SortedFootDnaArray.Length; i++)
        {
            float score = SortedScoreArray[i];
            if(score>MinScore)
            {
                FootDna[] SelectDnaArray = rnd.Choice(choices:SortedFootDnaArray, weights:SelectionWeightArray,
                                                            k:2, is_replacement:true).dna;
                DNA_set dna_set = CrossOver(SelectDnaArray[0], SelectDnaArray[1]);
                ChildrenArray[i] = dna_set;
            }
            else
            {
                ChildrenArray[i] = SortedFootDnaArray[i].get_DNA_set();
            }
        }
        int seed = Random.Range(0,10000);
        this.spider_m_sc.ResetDnaArray(ChildrenArray, seed);
    }

    void CHC(Cal_Result_Data data)
    {
        FootDna[] FootDnaArray  = data.footdnaArray;
        float[] ScoreArray = data.ScoreArray;
        int length = FootDnaArray.Length;

        if(this.trail_cnt==1)
        {
            if(is_Debug)
            {
                Debug.Log("first CHC");
            }
            StoreFootDnaArray = FootDnaArray;
            StoreScoreArray = ScoreArray;
        }
        else
        {
            //cat data
            FootDna[] AllFootDnaArray = new FootDna[2*num_spider];
            float[] AllScoreArray = new float[2*num_spider];
            for (int i = 0; i < length; i++)
            {
                AllFootDnaArray[i] = StoreFootDnaArray[i];
                AllScoreArray[i] = StoreScoreArray[i];
            }
            for (int i = 0; i < length; i++)
            {
                AllFootDnaArray[length+i] = FootDnaArray[i];
                AllScoreArray[length+i] = ScoreArray[i];
            }

            //sort
            for (int i = 0; i < AllFootDnaArray.Length; i++)
            {
                for (int k = i; k > 0; k--)
                {
                    float score_k_m1 = AllScoreArray[k-1];
                    float score_k = AllScoreArray[k];
                    if(score_k > score_k_m1)
                    {
                        AllScoreArray[k-1] = score_k;
                        AllScoreArray[k] = score_k_m1;

                        FootDna footdna_k = AllFootDnaArray[k];
                        AllFootDnaArray[k] = AllFootDnaArray[k-1];
                        AllFootDnaArray[k-1] = footdna_k;
                    }
                    else{break;}
                }
            }

            // Store good dna
            for (int i = 0; i < length; i++)
            {
                StoreFootDnaArray[i] = AllFootDnaArray[i];
                StoreScoreArray[i] = AllScoreArray[i];
            }

        }
        
        DNA_set[] ChildrenArray = new DNA_set[FootDnaArray.Length];
        cs_system.Random rnd = new cs_system.Random();
        for (int i = 0; i < (int)(length/2); i++)
        {
            int[] idx_arry = rnd.get_disjoint_int(2, 0, this.num_spider-1);
            int idx1 = idx_arry[0];
            int idx2 = idx_arry[1];
            DNA_set dna_set1 = CrossOver(StoreFootDnaArray[idx1], StoreFootDnaArray[idx2]);
            DNA_set dna_set2 = CrossOver(StoreFootDnaArray[idx1], StoreFootDnaArray[idx2]);
            ChildrenArray[2*i] = dna_set1;
            ChildrenArray[2*i+1] = dna_set2;
        }
        int seed = Random.Range(0,10000);
        this.spider_m_sc.ResetDnaArray(ChildrenArray, seed);
    }

    void ER(Cal_Result_Data data)
    {
        FootDna[] FootDnaArray  = data.footdnaArray;
        float[] ScoreArray = data.ScoreArray;
        int length = FootDnaArray.Length; 
        cs_system.Random rnd = new cs_system.Random();

        if(this.trail_cnt==1)
        {
            if(is_Debug)
            {
                Debug.Log("first ER");
            }
            StoreFootDnaArray = FootDnaArray;
            StoreScoreArray = ScoreArray;
        }
        else
        {
            FootDna[] newStoreFootDnaArray = new FootDna[StoreFootDnaArray.Length];
            float[] newStoreScoreArray = new float[StoreFootDnaArray.Length];
            //take best 2 indivisual in each families
            foreach(List<int> idx_list in FamilyGroupIndexDic.Values)
            {
                int idx1 = idx_list[0];
                int idx2 = idx_list[1];

                int StoreIdx1 = idx_list[2];
                int StoreIdx2 = idx_list[3];

                float childScore1 = ScoreArray[idx1];
                float childScore2 = ScoreArray[idx2];
                float score1 = StoreScoreArray[StoreIdx1];
                float score2 = StoreScoreArray[StoreIdx2];

                FootDna[] familyFootDnaArry = new FootDna[] {
                    FootDnaArray[idx1], FootDnaArray[idx2],
                    StoreFootDnaArray[StoreIdx1], StoreFootDnaArray[StoreIdx2]
                };
                float[] scorearry = new float[] {childScore1, childScore2, score1, score2};

                //sort
                for (int i = 0; i < scorearry.Length; i++)
                {
                    for (int k = i; k > 0; k--)
                    {
                        float score_k_m1 = scorearry[k-1];
                        float score_k = scorearry[k];
                        if(score_k > score_k_m1)
                        {
                            scorearry[k-1] = score_k;
                            scorearry[k] = score_k_m1;

                            FootDna footdna_k = familyFootDnaArry[k];
                            familyFootDnaArry[k] = familyFootDnaArry[k-1];
                            familyFootDnaArry[k-1] = footdna_k;
                        }
                        else{break;}
                    }
                }

                //take
                newStoreFootDnaArray[idx1] = familyFootDnaArry[0];
                newStoreFootDnaArray[idx2] = familyFootDnaArry[1];
                newStoreScoreArray[idx1] = scorearry[0];
                newStoreScoreArray[idx2] = scorearry[1];
            }

            StoreFootDnaArray = newStoreFootDnaArray;
            StoreScoreArray = newStoreScoreArray;
        }

        DNA_set[] ChildrenArray = new DNA_set[FootDnaArray.Length];
        for (int i = 0; i < (int)(length/2); i++)
        {
            int[] idx_arry = rnd.get_disjoint_int(2, 0, this.num_spider-1);
            int idx1 = idx_arry[0];
            int idx2 = idx_arry[1];
            DNA_set dna_set1 = CrossOver(StoreFootDnaArray[idx1], StoreFootDnaArray[idx2]);
            DNA_set dna_set2 = CrossOver(StoreFootDnaArray[idx1], StoreFootDnaArray[idx2]);
            ChildrenArray[2*i] = dna_set1;
            ChildrenArray[2*i+1] = dna_set2;
            FamilyGroupIndexDic[i] = new List<int>{2*i, 2*i+1, idx1, idx2};
        }
        int seed = Random.Range(0,10000);
        this.spider_m_sc.ResetDnaArray(ChildrenArray, seed);
    }
    void MGG(Cal_Result_Data data)
    {
        FootDna[] FootDnaArray  = data.footdnaArray;
        float[] ScoreArray = data.ScoreArray;
        int length = FootDnaArray.Length; 
        cs_system.Random rnd = new cs_system.Random();
        
        if(this.trail_cnt==1)
        {
            if(is_Debug)
            {
                Debug.Log("first MGG");
            }
            StoreFootDnaArray = FootDnaArray;
            StoreScoreArray = ScoreArray;
        }
        else
        {
            FootDna[] newStoreFootDnaArray = new FootDna[StoreFootDnaArray.Length];
            float[] newStoreScoreArray = new float[StoreFootDnaArray.Length];
            //take best 2 indivisual in each families
            foreach(List<int> idx_list in FamilyGroupIndexDic.Values)
            {
                int idx1 = idx_list[0];
                int idx2 = idx_list[1];

                int StoreIdx1 = idx_list[2];
                int StoreIdx2 = idx_list[3];

                float childScore1 = ScoreArray[idx1];
                float childScore2 = ScoreArray[idx2];
                float score1 = StoreScoreArray[StoreIdx1];
                float score2 = StoreScoreArray[StoreIdx2];

                FootDna[] familyFootDnaArry = new FootDna[] {
                    FootDnaArray[idx1], FootDnaArray[idx2],
                    StoreFootDnaArray[StoreIdx1], StoreFootDnaArray[StoreIdx2]
                };
                float[] scorearry = new float[] {childScore1, childScore2, score1, score2};

                //sort
                for (int i = 0; i < scorearry.Length; i++)
                {
                    for (int k = i; k > 0; k--)
                    {
                        float score_k_m1 = scorearry[k-1];
                        float score_k = scorearry[k];
                        if(score_k > score_k_m1)
                        {
                            scorearry[k-1] = score_k;
                            scorearry[k] = score_k_m1;

                            FootDna footdna_k = familyFootDnaArry[k];
                            familyFootDnaArry[k] = familyFootDnaArry[k-1];
                            familyFootDnaArry[k-1] = footdna_k;
                        }
                        else{break;}
                    }
                }

                //take
                //best individual
                newStoreFootDnaArray[idx1] = familyFootDnaArry[0];
                newStoreScoreArray[idx1] = scorearry[0];

                //ranking select
                FootDna_Index dna_idx = rnd.Choice(choices:familyFootDnaArry, 
                                            weights:this.RankingWeight,k:1,is_replacement:true);
                newStoreFootDnaArray[idx2] = dna_idx.dna[0];
                newStoreScoreArray[idx2] = dna_idx.index[0];
            }

            StoreFootDnaArray = newStoreFootDnaArray;
            StoreScoreArray = newStoreScoreArray;
        }

        DNA_set[] ChildrenArray = new DNA_set[FootDnaArray.Length];
        for (int i = 0; i < (int)(length/2); i++)
        {
            int[] idx_arry = rnd.get_disjoint_int(2, 0, this.num_spider-1);
            int idx1 = idx_arry[0];
            int idx2 = idx_arry[1];
            DNA_set dna_set1 = CrossOver(StoreFootDnaArray[idx1], StoreFootDnaArray[idx2]);
            DNA_set dna_set2 = CrossOver(StoreFootDnaArray[idx1], StoreFootDnaArray[idx2]);
            ChildrenArray[2*i] = dna_set1;
            ChildrenArray[2*i+1] = dna_set2;
            FamilyGroupIndexDic[i] = new List<int>{2*i, 2*i+1, idx1, idx2};
        }
        int seed = Random.Range(0,10000);
        this.spider_m_sc.ResetDnaArray(ChildrenArray, seed);
    }

    public float get_acc_time()
    {
        return this.acc_time;
    }
}