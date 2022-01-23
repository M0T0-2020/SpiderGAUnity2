using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTextController : MonoBehaviour
{
    public GameObject text, GA_manager_object;
    GA_Manager GA_Manager_sc;
    Text time_text;
    void Start()
    {
        this.time_text = this.text.GetComponent<Text>();
        this.GA_Manager_sc = GA_manager_object.GetComponent<GA_Manager>();

        this.time_text.text = this.GA_Manager_sc.get_acc_time().ToString();

    }
    void Update()
    {
        this.time_text.text = this.GA_Manager_sc.get_acc_time().ToString();
    }
}
