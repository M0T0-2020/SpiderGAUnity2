using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public enum GAName {SGA, IGS, SS, CHC, ER, MGG}
    [SerializeField]
    GAName ga_name = GAName.SGA;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ga_name);        
        Debug.Log("1"+ga_name);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
