using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCheckbox : MonoBehaviour
{
    [SerializeField] private GameManager G1;
    public Toggle[] selectAns{
        get; set;
    }
    public bool isShowbtn{
        get; set;
    }

    void Start()
    {
        SetAnswerObject();
        G1.ButtonAns.SetActive(false);
    }


    void Update()
    {
        if(isShowbtn){
            G1.ButtonAns.SetActive(true);
            isShowbtn = false;
        }
    }

    public void SetAnswerObject(){
        selectAns = new Toggle[G1.length_arr];

        for(int i = 0; i < G1.length_arr; i++){
            selectAns[i] = transform.GetChild(i).gameObject.GetComponent<Toggle>();
        }

        selectAns[0].onValueChanged.AddListener((bool on) => {
            if(on) {
                for(int i = 0; i < 4; i++){
                    if(selectAns[i] != selectAns[0]){
                        selectAns[i].isOn = false;
                    }
                }
            isShowbtn = true;
            }
        });

        selectAns[1].onValueChanged.AddListener((bool on) => {
            if(on) {
                for(int i = 0; i < 4; i++){
                    if(selectAns[i] != selectAns[1]){
                        selectAns[i].isOn = false;
                    }
                }
            }
            isShowbtn = true;
        });

        selectAns[2].onValueChanged.AddListener((bool on) => {
            if(on) {
                for(int i = 0; i < 4; i++){
                    if(selectAns[i] != selectAns[2]){
                        selectAns[i].isOn = false;
                    }
                }
            }
            isShowbtn = true;
        });

        selectAns[3].onValueChanged.AddListener((bool on) => {
            if(on) {
                for(int i = 0; i < 4; i++){
                    if(selectAns[i] != selectAns[3]){
                        selectAns[i].isOn = false;
                    }
                }
            }
            isShowbtn = true;
        });
    }


    public void User_SelectAns(){
        for(int i = 0; i < G1.length_arr; i++){
            if(selectAns[i].transform.GetComponent<Toggle>().isOn){
                GameManager.User_ansT.Add(i + 1);
            }
        }

        // for(int i = 0; i < GameManager.User_ansT.Count; i++){
        //     Debug.Log(GameManager.User_ansT[i]);
        // }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
