using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PanelAnswer;
    [SerializeField] public GameObject ButtonAns; 
    [SerializeField] public Sprite img_correct; 
    [SerializeField] public Sprite img_cancel; 
    public static List<int> User_Select = new List<int>();
    public static List<int> User_Ans = new List<int>();
    [SerializeField] public GameObject PanelTable; 
    public Sprite ImageUse{
        get; set;
    }
    private string imagePath{
        get; set;
    }

    private List<Sprite> Image_Mistake{
        get; set;
    } = new List<Sprite>();
    private List<string> Image_PathMistake{
        get; set;
    } = new List<string>();

    public int length_arr = 4;

    void Start(){
        StartCoroutine(GetRequest("http://127.0.0.1/dashboard/website_project/FetchDataDB/QuizGame.php", SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(GetRequest_Mistake("http://127.0.0.1/dashboard/website_project/FetchDataDB/QuizGame.php", SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator GetRequest(string URL, int Pathid)
    {
        WWWForm form = new WWWForm();

        form.AddField("PathFace", Pathid);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(URL, form))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    imagePath = webRequest.downloadHandler.text;
                    Debug.Log(webRequest.downloadHandler.text);
                    LoadImage(imagePath, Pathid);
                    break;
            }
        }
    }

    IEnumerator GetRequest_Mistake(string URL, int Pathid)
    {
        WWWForm form = new WWWForm();

        form.AddField("Path_Mistake", Pathid);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(URL, form))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    JSONNode jsonNode = JSON.Parse(webRequest.downloadHandler.text);
                    foreach(JSONNode n in jsonNode)
                        Image_PathMistake.Add(n["filename"]);

                    foreach(string s in Image_PathMistake){
                        Debug.Log(s);
                        LoadImage(s, Pathid);
                    }

                    break;
            }
        }
    }

    private void LoadImage(string filename, int id){
        string path = "";
        if(id == 1){
            path = @"C:\Users\INK\Desktop\Face-Detection\" + filename;
        }else if(id == 2){
            path = @"C:\Users\INK\Desktop\Face-Detection\" + filename;
        }else if(id == 3){
            path = @"C:\Users\INK\Desktop\Face-Detection\" + filename;
        }else if(id == 4){
            path = @"C:\Users\INK\Desktop\Face-Detection\" + filename;
        }

        byte[] bytes = File.ReadAllBytes(path);

        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(bytes);

        Sprite sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), new Vector2(0.5f, 0.5f));
        ImageUse = sprite;  
    }

        private void LoadImage_Mistake(string filename, int id){
        string path = "";
        if(id == 1 || id == 2){
            path = @"C:\Users\INK\Desktop\Path_Face\" + filename;
        }else if(id == 3){
            path = @"C:\Users\INK\Desktop\Path_Face\" + filename;
        }else if(id == 4){
            path = @"C:\Users\INK\Desktop\Path_Face\" + filename;
        }

        byte[] bytes = File.ReadAllBytes(path);

        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(bytes);

        Sprite sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), new Vector2(0.5f, 0.5f));
        Image_Mistake.Add(sprite);
    }
}
