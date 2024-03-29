using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SelectCheckbox : MonoBehaviour
{
    [SerializeField] private GameManager G1;
    [SerializeField] private GameObject Panel_Fade;
    [SerializeField] private GameObject Quiznumber;

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
        Quiznumber.GetComponent<Text>().text = "Quiz " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
        Invoke("SetAnswerObject", 0.7f);
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

        randomNum = Random.Range(0, 3);
        selectAns[randomNum].transform.Find("Image").gameObject.GetComponent<Image>().sprite = G1.ImageUse;
        GameManager.User_Ans.Add(randomNum);

        for(int i = 0; i < 4; i++){
            if(i != randomNum){
                selectAns[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite = G1.Image_Mistake[0];
                G1.Image_Mistake.RemoveAt(0);
            }
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

        Panel_Fade.SetActive(false);
    }

    public void User_SelectAns(){
        for(int i = 0; i < G1.length_arr; i++){
            if(selectAns[i].transform.GetComponent<Toggle>().isOn){
                GameManager.User_Select.Add(i);
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Loadimage_CorrectCacel(string path){
        byte[] bytes = File.ReadAllBytes(path);
        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), new Vector2(0.5f, 0.5f));
        // imgCorect_Cancel.Add(sprite);
    }

    public void User_SelectAnsLast(){
        List<int> CheckAns = new List<int>();
        int Score = 0;
        // string img_correct = @"C:\Users\INK\Desktop\2D Game\Quiz Game\Assets\Image\checked.png";
        // string img_mistake = @"C:\Users\INK\Desktop\2D Game\Quiz Game\Assets\Image\cancel.png";

        // Loadimage_CorrectCacel(img_correct);
        // Loadimage_CorrectCacel(img_mistake);

        for(int i = 0; i < G1.length_arr; i++){
            if(selectAns[i].transform.GetComponent<Toggle>().isOn){
                GameManager.User_Select.Add(i);
            }
        }

        for(int i = 0; i < GameManager.User_Select.Count; i++){
            if(GameManager.User_Select[i] == GameManager.User_Ans[i]){
                CheckAns.Add(1);
            }else{
                CheckAns.Add(0);
            }
        }

        G1.PanelTable.SetActive(true);

        for(int i = 0; i < CheckAns.Count; i++){
            if(CheckAns[i] == 1){
                Score += 25;
                G1.PanelTable.transform.Find("Row").transform.GetChild(i).gameObject.GetComponent<Image>().sprite = G1.img_correct;
            }else{
                G1.PanelTable.transform.Find("Row").transform.GetChild(i).gameObject.GetComponent<Image>().sprite = G1.img_cancel;
            }
        }

        if(Score < 75)
            G1.TextScore.GetComponent<Text>().text = "<color=red>" + Score.ToString() + "</color>/100";
        else
            G1.TextScore.GetComponent<Text>().text = "<color=green>" + Score.ToString() + "</color>/100";

    }
}
