using System_CS = System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DnaDataForSave {
    //1
    public int angle_max_1, angle_min_1, half_speed_max_1, half_speed_min_1;
    //足のいろ
    //コンストラクタ Color32(byte r, byte g, byte b, byte a)
    byte f1_color_1_r, f1_color_1_g, f1_color_1_b, f1_color_1_a;
    byte f2_color_1_r, f2_color_1_g, f2_color_1_b, f2_color_1_a;
    byte f3_color_1_r, f3_color_1_g, f3_color_1_b, f3_color_1_a;
    //第1関節 パラメータ
    public float f1_angle_x_1, f1_angle_z_1, f1_half_speed_1, w_x_1_1, w_z_1_1;
    
    //第2関節 パラメータ
    public float f2_angle_x_1, f2_angle_z_1, f2_half_speed_1, w_x_2_1, w_z_2_1;
    //第3関節 パラメータ
    public float f3_angle_x_1, f3_angle_z_1, f3_half_speed_1, w_x_3_1, w_z_3_1;

    //2
    public int angle_max_2, angle_min_2, half_speed_max_2, half_speed_min_2;
    //足のいろ
    byte f1_color_2_r, f1_color_2_g, f1_color_2_b, f1_color_2_a;
    byte f2_color_2_r, f2_color_2_g, f2_color_2_b, f2_color_2_a;
    byte f3_color_2_r, f3_color_2_g, f3_color_2_b, f3_color_2_a;
    //第1関節 パラメータ
    public float f1_angle_x_2, f1_angle_z_2, f1_half_speed_2, w_x_1_2, w_z_1_2;   
    //第2関節 パラメータ
    public float f2_angle_x_2, f2_angle_z_2, f2_half_speed_2, w_x_2_2, w_z_2_2;
    //第3関節 パラメータ
    public float f3_angle_x_2, f3_angle_z_2, f3_half_speed_2, w_x_3_2, w_z_3_2;

    //3
    public int angle_max_3, angle_min_3, half_speed_max_3, half_speed_min_3;
    //足のいろ
    byte f1_color_3_r, f1_color_3_g, f1_color_3_b, f1_color_3_a;
    byte f2_color_3_r, f2_color_3_g, f2_color_3_b, f2_color_3_a;
    byte f3_color_3_r, f3_color_3_g, f3_color_3_b, f3_color_3_a;
    //第1関節 パラメータ
    public float f1_angle_x_3, f1_angle_z_3, f1_half_speed_3, w_x_1_3, w_z_1_3;    
    //第2関節 パラメータ
    public float f2_angle_x_3, f2_angle_z_3, f2_half_speed_3, w_x_2_3, w_z_2_3;
    //第3関節 パラメータ
    public float f3_angle_x_3, f3_angle_z_3, f3_half_speed_3, w_x_3_3, w_z_3_3;

    public void set_dna1(dna_a dna1)
    {
        angle_max_1 = dna1.angle_max;
        angle_min_1 = dna1.angle_max;
        half_speed_max_1 = dna1.angle_max;
        half_speed_min_1 = dna1.angle_max;

        f1_color_1_r = dna1.f1_color.r;
        f1_color_1_g = dna1.f1_color.g;
        f1_color_1_b = dna1.f1_color.b;
        f1_color_1_a = dna1.f1_color.a;

        f2_color_1_r = dna1.f2_color.r;
        f2_color_1_g = dna1.f2_color.r;
        f2_color_1_b = dna1.f2_color.r;
        f2_color_1_a = dna1.f2_color.r;

        f3_color_1_r = dna1.f3_color.r;
        f3_color_1_g = dna1.f3_color.r;
        f3_color_1_b = dna1.f3_color.r;
        f3_color_1_a = dna1.f3_color.r;

        f1_angle_x_1 = dna1.f1_angle_x;
        f1_angle_z_1 = dna1.f1_angle_z;
        f1_half_speed_1 = dna1.f1_half_speed;
        w_x_1_1 = dna1.w_x_1;
        w_z_1_1 = dna1.w_z_1;

        f2_angle_x_1 = dna1.f2_angle_x;
        f2_angle_z_1 = dna1.f2_angle_z;
        f2_half_speed_1 = dna1.f2_half_speed;
        w_x_2_1 = dna1.w_x_2;
        w_z_2_1 = dna1.w_z_2;

        f3_angle_x_1 = dna1.f3_angle_x;
        f3_angle_z_1 = dna1.f3_angle_z;
        f3_half_speed_1 = dna1.f3_half_speed;
        w_x_3_1 = dna1.w_x_3;
        w_z_3_1 = dna1.w_z_3;
    }

    public void set_dna2(dna_a dna2)
    {
        angle_max_2 = dna2.angle_max;
        angle_min_2 = dna2.angle_max;
        half_speed_max_2 = dna2.angle_max;
        half_speed_min_2 = dna2.angle_max;

        f1_color_2_r = dna2.f1_color.r;
        f1_color_2_g = dna2.f1_color.g;
        f1_color_2_b = dna2.f1_color.b;
        f1_color_2_a = dna2.f1_color.a;

        f2_color_2_r = dna2.f2_color.r;
        f2_color_2_g = dna2.f2_color.r;
        f2_color_2_b = dna2.f2_color.r;
        f2_color_2_a = dna2.f2_color.r;

        f3_color_2_r = dna2.f3_color.r;
        f3_color_2_g = dna2.f3_color.r;
        f3_color_2_b = dna2.f3_color.r;
        f3_color_2_a = dna2.f3_color.r;

        f1_angle_x_2 = dna2.f1_angle_x;
        f1_angle_z_2 = dna2.f1_angle_z;
        f1_half_speed_2 = dna2.f1_half_speed;
        w_x_1_2 = dna2.w_x_1;
        w_z_1_2 = dna2.w_z_1;

        f2_angle_x_2 = dna2.f2_angle_x;
        f2_angle_z_2 = dna2.f2_angle_z;
        f2_half_speed_2 = dna2.f2_half_speed;
        w_x_2_2 = dna2.w_x_2;
        w_z_2_2 = dna2.w_z_2;

        f3_angle_x_2 = dna2.f3_angle_x;
        f3_angle_z_2 = dna2.f3_angle_z;
        f3_half_speed_2 = dna2.f3_half_speed;
        w_x_3_2 = dna2.w_x_3;
        w_z_3_2 = dna2.w_z_3;
    }

    public void set_dna3(dna_a dna3)
    {
        angle_max_3 = dna3.angle_max;
        angle_min_3 = dna3.angle_max;
        half_speed_max_3 = dna3.angle_max;
        half_speed_min_3 = dna3.angle_max;

        f1_color_3_r = dna3.f1_color.r;
        f1_color_3_g = dna3.f1_color.g;
        f1_color_3_b = dna3.f1_color.b;
        f1_color_3_a = dna3.f1_color.a;

        f2_color_3_r = dna3.f2_color.r;
        f2_color_3_g = dna3.f2_color.r;
        f2_color_3_b = dna3.f2_color.r;
        f2_color_3_a = dna3.f2_color.r;

        f3_color_3_r = dna3.f3_color.r;
        f3_color_3_g = dna3.f3_color.r;
        f3_color_3_b = dna3.f3_color.r;
        f3_color_3_a = dna3.f3_color.r;

        f1_angle_x_3 = dna3.f1_angle_x;
        f1_angle_z_3 = dna3.f1_angle_z;
        f1_half_speed_3 = dna3.f1_half_speed;
        w_x_1_3 = dna3.w_x_1;
        w_z_1_3 = dna3.w_z_1;

        f2_angle_x_3 = dna3.f2_angle_x;
        f2_angle_z_3 = dna3.f2_angle_z;
        f2_half_speed_3 = dna3.f2_half_speed;
        w_x_2_3 = dna3.w_x_2;
        w_z_2_3 = dna3.w_z_2;

        f3_angle_x_3 = dna3.f3_angle_x;
        f3_angle_z_3 = dna3.f3_angle_z;
        f3_half_speed_1 = dna3.f3_half_speed;
        w_x_3_3 = dna3.w_x_3;
        w_z_3_3 = dna3.w_z_3;
    }
}
public struct ScoreData
{
    public float max, min, mean, std;
}
public class DataManager : MonoBehaviour
{
    [SerializeField]
    List<float> ScoreMax, ScoreMin, ScoreMean, ScoreStd;
    [SerializeField]
    List<DnaDataForSave> DNA_Lsit;

    private string Basepath;
    // Start is called before the first frame update
    void Start()
    {        
    }

    public void setBasePath(string BasePath)
    {
        this.Basepath = BasePath;
    }
    public void check_make_dir()
    {
        if(!System_CS.IO.Directory.Exists(Basepath))
        {
            System_CS.IO.Directory.CreateDirectory(Basepath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveDNAData(FootDna footdna, string file_name)
    {
        DnaDataForSave dna_data = new DnaDataForSave();
        dna_data.set_dna1(footdna.dna1);
        dna_data.set_dna2(footdna.dna2);
        dna_data.set_dna3(footdna.dna3);
        // example "." + "/" + "SGA" + "/" + "data1"
        string path = Basepath  + "/DNA_" + file_name;

        XmlUtil.Seialize<DnaDataForSave>(path, dna_data);
    }

    public void SaveScoreList()
    {
        XmlUtil.Seialize<List<float>>(Basepath+"/ScoreMax.xml", ScoreMax);
        XmlUtil.Seialize<List<float>>(Basepath+"/ScoreMin.xml", ScoreMin);
        XmlUtil.Seialize<List<float>>(Basepath+"/ScoreMean.xml", ScoreMean);
        XmlUtil.Seialize<List<float>>(Basepath+"/ScoreStd.xml", ScoreStd);
    }

    public ScoreData UpdateScore(float[] ScoreArray)
    {
        float mean = 0;
        float std = 0;
        float max = (float)-1e20;
        float min = (float)1e20;
        int length = ScoreArray.Length;
        for (int i = 0; i < length; i++)
        {
            float score = ScoreArray[i];
            mean += score/length;
            if(max < score)
            {
                max = score;
            }
            if(min>score)
            {
                min = score;
            }

        }
        for (int i = 0; i < length; i++)
        {
            float v = Mathf.Pow(ScoreArray[i]-mean, 2);
            std += v/length;
        }
        std = Mathf.Sqrt(std);

        ScoreMax.Add(max);
        ScoreMin.Add(min);
        ScoreMean.Add(mean);
        ScoreStd.Add(std);

        ScoreData data;
        data.max = max;
        data.min = min;
        data.mean = mean;
        data.std = std;
        return data;
    } 
}
