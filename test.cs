using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public enum GAName {SGA, IGS, SS, CHC, ER, MGG}
    [SerializeField]
    GAName ga_name = GAName.SGA;
    public int angle_max, angle_min, half_speed_max, half_speed_min;

    public GameObject spider;
    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject _spider = Instantiate(spider) as GameObject;
        Spider_Controller SC = _spider.GetComponent<Spider_Controller>();
        _spider.name = "ndoifvnasdoivn";

        //Random.InitState(seed);

        float px = 10;
        _spider.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        _spider.transform.position = new Vector3(px, 1.5f, 0);
        dna_a dna1 = new dna_a(this.angle_max, this.angle_min, this.half_speed_max, this.half_speed_min);
        dna_a dna2 = new dna_a(this.angle_max, this.angle_min, this.half_speed_max, this.half_speed_min);
        dna_a dna3 = new dna_a(this.angle_max, this.angle_min, this.half_speed_max, this.half_speed_min);

        FootDna f_dna = new FootDna(SC, dna1, dna2, dna3, 1, 1, 1, 1);
        f_dna.set_gene(false);

        Debug.Log(ga_name);        
        //Debug.Log("1"+ga_name);   
        */     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
