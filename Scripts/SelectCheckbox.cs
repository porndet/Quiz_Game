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

    private int randomNum{
        get; set;
    }

    void Start()
    {
        G1.ButtonAns.SetActive(false);
        Invoke("SetAnswerObject", 1.0f);
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

        randomNum = Random.Range(1, 4);
        selectAns[randomNum].transform.Find("Image").gameObject.GetComponent<Image>().sprite = G1.ImageUse;
        GameManager.User_Ans.Add(randomNum);

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
                GameManager.User_Select.Add(i + 1);
            }
        }

        // for(int i = 0; i < GameManager.User_ansT.Count; i++){
        //     Debug.Log(GameManager.User_ansT[i]);
        // }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void User_SelectAnsLast(){
        for(int i = 0; i < G1.length_arr; i++){
            if(selectAns[i].transform.GetComponent<Toggle>().isOn){
                GameManager.User_Select.Add(i + 1);
            }
        }

        for(int i = 0; i < GameManager.User_Select.Count; i++){
            Debug.Log(GameManager.User_Select[i]);
        }
    }
}
