using System_sc = System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossOverUtil
{
    public static dna_a get(dna_a dna1, dna_a dna2)
    {
        if(Random.value < 0.5f)
        {
            return dna1;
        }
        else
        {
            return dna2;
        }
    }
}
public struct Cal_Result_Data
{
    public float[] ScoreArray;
    public FootDna[] footdnaArray;
    public ScoreData scoredata;
}

public struct DNA_set
{
    public dna_a dna1, dna2, dna3;

    public void mutate()
    {
        dna1.mutation_a();
        dna2.mutation_a();
        dna3.mutation_a();
    } 
}

public class dna_a
{
    public float mutation_rate_angle, mutation_rate_speed, mutation_rate_w, mutation_rate_color;
    public int angle_max, angle_min, half_speed_max, half_speed_min;
    private List<int> W = new List<int> {-1, 1};
    //足のいろ
    public Color32 f1_color, f2_color, f3_color;
    //第1関節 パラメータ
    public float f1_angle_x, f1_angle_z, f1_half_speed, w_x_1, w_z_1;
    
    //第2関節 パラメータ
    public float f2_angle_x, f2_angle_z, f2_half_speed, w_x_2, w_z_2;
    //第3関節 パラメータ
    public float f3_angle_x, f3_angle_z, f3_half_speed, w_x_3, w_z_3;

    public dna_a(int angle_max, int angle_min, int half_speed_max, int half_speed_min,
            float mutation_rate_angle, float mutation_rate_speed, float mutation_rate_w, float mutation_rate_color)
    {
        this.angle_max = angle_max;
        this.angle_min = angle_min;
        this.half_speed_max = half_speed_max;
        this.half_speed_min = half_speed_min;

        this.mutation_rate_angle = mutation_rate_angle;
        this.mutation_rate_speed = mutation_rate_speed;
        this.mutation_rate_color = mutation_rate_color;
        this.mutation_rate_w = mutation_rate_w;

        f1_color = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);
        f2_color = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);
        f3_color = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);

        f1_angle_x = Random.Range(angle_min,angle_max);
        f1_angle_z = Random.Range(angle_min,angle_max);
        f1_half_speed = Random.Range(half_speed_min,half_speed_max);
        w_x_1 = W[(int)Random.Range(0,2)];
        w_z_1 = W[(int)Random.Range(0,2)];

        f2_angle_x = Random.Range(angle_min,angle_max);
        f2_angle_z = Random.Range(angle_min,angle_max);
        f2_half_speed = Random.Range(half_speed_min,half_speed_max);
        w_x_2 = W[(int)Random.Range(0,2)];
        w_z_2 = W[(int)Random.Range(0,2)];

        f3_angle_x = Random.Range(angle_min,angle_max);
        f3_angle_z = Random.Range(angle_min,angle_max);
        f3_half_speed = Random.Range(half_speed_min,half_speed_max);
        w_x_3 = W[(int)Random.Range(0,2)];
        w_z_3 = W[(int)Random.Range(0,2)];
    }

    private float mutate(float value, int min, int max)
    {
        float new_value;
        int a = (int)Mathf.Max(-10, min - (int)value);
        int b = (int)Mathf.Min(10, max - (int)value);
        new_value = value + (float)Random.Range(a,b);
        return new_value;
    }

    public void mutation_a()
    {
        //angle_x
        if(Random.value<this.mutation_rate_angle)
        {this.f1_angle_x = mutate(this.f1_angle_x, this.angle_min, this.angle_max);}
        if(Random.value<this.mutation_rate_angle)
        {this.f2_angle_x = mutate(this.f2_angle_x, this.angle_min, this.angle_max);}
        if(Random.value<this.mutation_rate_angle)
        {this.f3_angle_x = mutate(this.f3_angle_x, this.angle_min, this.angle_max);}

        //angle_z
        if(Random.value<this.mutation_rate_angle)
        {this.f1_angle_z = this.mutate(this.f1_angle_z, this.angle_min, this.angle_max);}
        if(Random.value<this.mutation_rate_angle)
        {this.f2_angle_z = this.mutate(this.f2_angle_z, this.angle_min, this.angle_max);}
        if(Random.value<this.mutation_rate_angle)
        {this.f3_angle_z = this.mutate(this.f3_angle_z, this.angle_min, this.angle_max);}
        
        //hald_speed
        if(Random.value<this.mutation_rate_speed)
        {this.f1_half_speed = this.mutate(this.f1_half_speed, this.half_speed_min, this.half_speed_max);}
        if(Random.value<this.mutation_rate_speed)
        {this.f2_half_speed = this.mutate(this.f2_half_speed, this.half_speed_min, this.half_speed_max);}
        if(Random.value<this.mutation_rate_speed)
        {this.f3_half_speed = this.mutate(this.f3_half_speed, this.half_speed_min, this.half_speed_max);}

        // W
        if(Random.value<this.mutation_rate_w) {this.w_x_1 = W[(int)Random.Range(0,2)];}
        if(Random.value<this.mutation_rate_w) {this.w_z_1 = W[(int)Random.Range(0,2)];}
        if(Random.value<this.mutation_rate_w) {this.w_x_2 = W[(int)Random.Range(0,2)];}
        if(Random.value<this.mutation_rate_w) {this.w_z_2 = W[(int)Random.Range(0,2)];}
        if(Random.value<this.mutation_rate_w) {this.w_x_3 = W[(int)Random.Range(0,2)];}
        if(Random.value<this.mutation_rate_w) {this.w_z_3 = W[(int)Random.Range(0,2)];}

        //color
        if(Random.value<this.mutation_rate_color) {this.f1_color = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);}
        if(Random.value<this.mutation_rate_color) {this.f2_color = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);}
        if(Random.value<this.mutation_rate_color) {this.f3_color = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);}
    }
    
}

public class FootDna 
{
    public Spider_Controller spider_c;
    private List<int> W = new List<int> {-1, 1};
    public dna_a dna1, dna2, dna3;

    public FootDna(Spider_Controller SC, dna_a dna1, dna_a dna2, dna_a dna3)
    {
        this.spider_c = SC;
        this.dna1 = dna1;
        this.dna2 = dna2;
        this.dna3 = dna3;
    }

    public void set_gene(bool is_Transform)
    {
        //1
        this.spider_c.f1_1_angle_x = this.dna1.f1_angle_x;
        this.spider_c.f1_2_angle_x = this.dna1.f2_angle_x;
        this.spider_c.f1_3_angle_x = this.dna1.f3_angle_x;

        this.spider_c.f1_1_angle_z = this.dna1.f1_angle_z;
        this.spider_c.f1_2_angle_z = this.dna1.f2_angle_z;
        this.spider_c.f1_3_angle_z = this.dna1.f3_angle_z;
        
        this.spider_c.f1_1_half_speed = this.dna1.f1_half_speed;
        this.spider_c.f1_2_half_speed = this.dna1.f2_half_speed;
        this.spider_c.f1_3_half_speed = this.dna1.f3_half_speed;

        this.spider_c.f1_1_color = this.dna1.f1_color;
        this.spider_c.f1_2_color = this.dna1.f2_color;
        this.spider_c.f1_3_color = this.dna1.f3_color;

        //2
        this.spider_c.f2_1_angle_x = this.dna2.f1_angle_x;
        this.spider_c.f2_2_angle_x = this.dna2.f2_angle_x;
        this.spider_c.f2_3_angle_x = this.dna2.f3_angle_x;

        this.spider_c.f2_1_angle_z = this.dna2.f1_angle_z;
        this.spider_c.f2_2_angle_z = this.dna2.f2_angle_z;
        this.spider_c.f2_3_angle_z = this.dna2.f3_angle_z;
        
        this.spider_c.f2_1_half_speed = this.dna2.f1_half_speed;
        this.spider_c.f2_2_half_speed = this.dna2.f2_half_speed;
        this.spider_c.f2_3_half_speed = this.dna2.f3_half_speed;

        this.spider_c.f2_1_color = this.dna2.f1_color;
        this.spider_c.f2_2_color = this.dna2.f2_color;
        this.spider_c.f2_3_color = this.dna2.f3_color;
        
        //3
        this.spider_c.f3_1_angle_x = this.dna3.f1_angle_x;
        this.spider_c.f3_2_angle_x = this.dna3.f2_angle_x;
        this.spider_c.f3_3_angle_x = this.dna3.f3_angle_x;

        this.spider_c.f3_1_angle_z = this.dna3.f1_angle_z;
        this.spider_c.f3_2_angle_z = this.dna3.f2_angle_z;
        this.spider_c.f3_3_angle_z = this.dna3.f3_angle_z;
        
        this.spider_c.f3_1_half_speed = this.dna3.f1_half_speed;
        this.spider_c.f3_2_half_speed = this.dna3.f2_half_speed;
        this.spider_c.f3_3_half_speed = this.dna3.f3_half_speed;

        this.spider_c.f3_1_color = this.dna3.f1_color;
        this.spider_c.f3_2_color = this.dna3.f2_color;
        this.spider_c.f3_3_color = this.dna3.f3_color;

        this.spider_c.Reset(is_Transform);
    }

    public void mutation()
    {
        this.dna1.mutation_a();
        this.dna2.mutation_a();
        this.dna3.mutation_a();
    }

    public DNA_set get_DNA_set()
    {
        DNA_set dna_set;
        dna_set.dna1 = this.dna1;
        dna_set.dna2 = this.dna2;
        dna_set.dna3 = this.dna3;
        return dna_set;
    }
}