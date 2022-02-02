using System_CS = System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System_CS.SerializableAttribute]
public class DnaDataForSave {
    public Dictionary<string, float> data1_1, data1_2, data2_1, data2_2, data3_1, data3_2;
    //public 

}
public struct ScoreData
{
    public float max, min, mean, std;
}
public class DataManager : MonoBehaviour
{
    List<float> ScoreMax, ScoreMin, ScoreMean, ScoreStd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        
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
