using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCheckbox : MonoBehaviour
{
    [SerializeField] private GameManager G1;

    void Start()
    {
        G1.SetAnswerObject();
        G1.ButtonAns.SetActive(false);
    }


    void Update()
    {
        if(G1.isShowbtn){
            G1.ButtonAns.SetActive(true);
        }
    }
}
