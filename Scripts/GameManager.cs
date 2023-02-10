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
    [SerializeField] public GameObject imageQuestion;
    public static List<int> User_Select = new List<int>();
    public static List<int> User_Ans = new List<int>();
    public static List<Sprite> ImageFace = new List<Sprite>();
    [SerializeField] public GameObject PanelTable; 
    [SerializeField] public GameObject TextScore;
    public Sprite ImageUse{
        get; set;
    }
    public static bool FirstTimeActive{
        get; set;
    }
    private string imagePath{
        get; set;
    }
    private string ImageFace_FILENAME{
        get; set;
    }

    public List<Sprite> Image_Mistake{
        get; set;
    } = new List<Sprite>();
    private List<string> Image_PathMistake{
        get; set;
    } = new List<string>();

    public int length_arr = 4;

    void Start(){
        StartCoroutine(GetRequest("http://127.0.0.1/dashboard/website_project/FetchDataDB/QuizGame.php", SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(GetRequest_Mistake("http://127.0.0.1/dashboard/website_project/FetchDataDB/QuizGameMistake.php", SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(GetRequestFace("http://127.0.0.1/dashboard/website_project/FetchDataDB/JigsawGame.php"));
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
                    JSONNode jsonNode = JSON.Parse(webRequest.downloadHandler.text);
                    foreach(JSONNode n in jsonNode)
                        imagePath = n["file_path"];

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
                        LoadImage_Mistake(s, Pathid);
                    }

                    break;
            }
        }
    }

    IEnumerator GetRequestFace(string URL)
    {
        WWWForm form = new WWWForm();

        form.AddField("FaceDB", 1);

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
                        ImageFace_FILENAME = n["file_path"];

                    if(!FirstTimeActive)
                        LoadImageFace(ImageFace_FILENAME);

                    imageQuestion.GetComponent<Image>().sprite = ImageFace[0];
                    break;
            }
        }
    }

    private void LoadImage(string filename, int id){
        string path = @"C:\Users\INK\Desktop" + filename;
        byte[] bytes = File.ReadAllBytes(path);

        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(bytes);

        Sprite sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), new Vector2(0.5f, 0.5f));
        ImageUse = sprite;  
    }

    private void LoadImage_Mistake(string filename, int id){
        string path = "";
        if(id == 1 || id == 2){
            path = @"C:\Users\INK\Desktop\Path_Face\Eye\" + filename;
        }else if(id == 3){
            path = @"C:\Users\INK\Desktop\Path_Face\Nose\" + filename;
        }else if(id == 4){
            path = @"C:\Users\INK\Desktop\Path_Face\Mouth\" + filename;
        }

        byte[] bytes = File.ReadAllBytes(path);

        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(bytes);

        Sprite sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), new Vector2(0.5f, 0.5f));
        Image_Mistake.Add(sprite);
    }

    private void LoadImageFace(string filename){
        string path = @"C:\Users\INK\Desktop" + filename;
        
        byte[] bytes = File.ReadAllBytes(path);

        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(bytes);

        Sprite sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), new Vector2(0.5f, 0.5f));
        ImageFace.Add(sprite); 

        FirstTimeActive = true;
    }
}
