using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadDna : MonoBehaviour
{
    public string pathname;
    public Color32 colorf1_1, colorf2_1, colorf3_1, colorf1_2, colorf2_2, colorf3_2, colorf1_3, colorf2_3, colorf3_3;
    public GameObject spider;
    [SerializeField]
    //DNA_set dna;
    // Start is called before the first frame update
    void Start()
    {
        //read
        DnaDataForSave dnaforset = XmlUtil.Deserialize<DnaDataForSave>(pathname);
        //Color32(byte r, byte g, byte b, byte a)
        colorf1_1 = new Color32(dnaforset.f1_color_1_r, dnaforset.f1_color_1_g, dnaforset.f1_color_1_b, dnaforset.f1_color_1_a);
        colorf2_1 = new Color32(dnaforset.f1_color_2_r, dnaforset.f1_color_2_g, dnaforset.f1_color_2_b, dnaforset.f1_color_2_a);
        colorf3_1 = new Color32(dnaforset.f1_color_3_r, dnaforset.f1_color_3_g, dnaforset.f1_color_3_b, dnaforset.f1_color_3_a);
        
        colorf1_2 = new Color32(dnaforset.f2_color_1_r, dnaforset.f2_color_1_g, dnaforset.f2_color_1_b, dnaforset.f2_color_1_a);
        colorf2_2 = new Color32(dnaforset.f2_color_2_r, dnaforset.f2_color_2_g, dnaforset.f2_color_2_b, dnaforset.f2_color_2_a);
        colorf3_2 = new Color32(dnaforset.f2_color_3_r, dnaforset.f2_color_3_g, dnaforset.f2_color_3_b, dnaforset.f2_color_3_a);
        
        colorf1_3 = new Color32(dnaforset.f3_color_1_r, dnaforset.f3_color_1_g, dnaforset.f3_color_1_b, dnaforset.f3_color_1_a);
        colorf2_3 = new Color32(dnaforset.f3_color_2_r, dnaforset.f3_color_2_g, dnaforset.f3_color_2_b, dnaforset.f3_color_2_a);
        colorf3_3 = new Color32(dnaforset.f3_color_3_r, dnaforset.f3_color_3_g, dnaforset.f3_color_3_b, dnaforset.f3_color_3_a);

        //f2_color_1_r, f2_color_1_g, f2_color_1_b, f2_color_1_a
        dna_a dna1 = new dna_a(angle_max:dnaforset.angle_max_1, angle_min:dnaforset.angle_min_1,
                                half_speed_max:dnaforset.half_speed_max_1, half_speed_min:dnaforset.half_speed_min_1,
                                mutation_rate_angle:0.0f, mutation_rate_speed:0.0f, mutation_rate_w:0.0f,
                                mutation_rate_color:0.0f);
        dna_a dna2 = new dna_a(angle_max:dnaforset.angle_max_2, angle_min:dnaforset.angle_min_2,
                                half_speed_max:dnaforset.half_speed_max_2, half_speed_min:dnaforset.half_speed_min_2,
                                mutation_rate_angle:0.0f, mutation_rate_speed:0.0f, mutation_rate_w:0.0f,
                                mutation_rate_color:0.0f);
        dna_a dna3 = new dna_a(angle_max:dnaforset.angle_max_3, angle_min:dnaforset.angle_min_3,
                                half_speed_max:dnaforset.half_speed_max_3, half_speed_min:dnaforset.half_speed_min_3,
                                mutation_rate_angle:0.0f, mutation_rate_speed:0.0f, mutation_rate_w:0.0f,
                                mutation_rate_color:0.0f);
        
        dna1.f1_angle_x = dnaforset.f1_angle_x_1;
        dna1.f1_angle_z = dnaforset.f1_angle_z_1;
        dna1.f1_half_speed = dnaforset.f1_half_speed_1;
        dna1.f1_color = colorf1_1;

        dna1.f2_angle_x = dnaforset.f2_angle_x_1;
        dna1.f2_angle_z = dnaforset.f2_angle_z_1;
        dna1.f2_half_speed = dnaforset.f2_half_speed_1;
        dna1.f2_color = colorf2_1;
        
        dna1.f3_angle_x = dnaforset.f3_angle_x_1;
        dna1.f3_angle_z = dnaforset.f3_angle_z_1;
        dna1.f3_half_speed = dnaforset.f3_half_speed_1;
        dna1.f3_color = colorf3_1;
        
        
        dna2.f1_angle_x = dnaforset.f1_angle_x_2;
        dna2.f1_angle_z = dnaforset.f1_angle_z_2;
        dna2.f1_half_speed = dnaforset.f1_half_speed_2;
        dna2.f1_color = colorf1_2;
        
        dna2.f2_angle_x = dnaforset.f2_angle_x_2;
        dna2.f2_angle_z = dnaforset.f2_angle_z_2;
        dna2.f2_half_speed = dnaforset.f2_half_speed_2;
        dna2.f2_color = colorf2_2;
        
        dna2.f3_angle_x = dnaforset.f3_angle_x_2;
        dna2.f3_angle_z = dnaforset.f3_angle_z_2;
        dna2.f3_half_speed = dnaforset.f3_half_speed_2;
        dna2.f3_color = colorf3_2;
        
        
        dna3.f1_angle_x = dnaforset.f1_angle_x_3;
        dna3.f1_angle_z = dnaforset.f1_angle_z_3;
        dna3.f1_half_speed = dnaforset.f1_half_speed_3;
        dna3.f1_color = colorf1_3;
        
        dna3.f2_angle_x = dnaforset.f2_angle_x_3;
        dna3.f2_angle_z = dnaforset.f2_angle_z_3;
        dna3.f2_half_speed = dnaforset.f2_half_speed_3;
        dna3.f2_color = colorf2_3;
        
        dna3.f3_angle_x = dnaforset.f3_angle_x_3;
        dna3.f3_angle_z = dnaforset.f3_angle_z_3;
        dna3.f3_half_speed = dnaforset.f3_half_speed_3;
        dna3.f3_color = colorf3_3;
        
        Spider_Controller spider_cnt = spider.GetComponent<Spider_Controller>();
        FootDna f_dna = new FootDna(spider_cnt, dna1, dna2, dna3);
        f_dna.set_gene(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
