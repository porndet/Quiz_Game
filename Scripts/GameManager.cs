using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PanelAnswer;
    [SerializeField] public GameObject ButtonAns;
    private const int lengthArr = 4;
    public bool isShowbtn{
        get; set;
    }

    public Toggle[] selectAns{
        get; set;
    }

    public Toggle ans{
        get; set;
    }

    public void SetAnswerObject(){
        selectAns = new Toggle[lengthArr];

        for(int i = 0; i < lengthArr; i++){
            selectAns[i] = PanelAnswer.transform.GetChild(i).gameObject.GetComponent<Toggle>();
        }

        selectAns[0].onValueChanged.AddListener((bool on) => {
            if(on) {
                for(int i = 0; i < 4; i++){
                    if(selectAns[i] != selectAns[0]){
                        selectAns[i].isOn = false;
                    }
                }
            ans = selectAns[0];
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
            ans = selectAns[1];
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
            ans = selectAns[2];
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
            ans = selectAns[3];
            isShowbtn = true;
        });
    }

    public void DisplayAnswer(){
        Debug.Log(ans);
    }
}
