using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;
using System.IO;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PanelAnswer;
    [SerializeField] public GameObject ButtonAns; 
    public static List<int> User_ansT = new List<int>();
    public int length_arr = 4;

    void Start(){
        // StartCoroutine(GetRequest("http://127.0.0.1/dashboard/website_project/FetchDataDB/FukuwaraiGame.php"));
    }

    IEnumerator GetRequest(string URL)
    {
        WWWForm form = new WWWForm();

        form.AddField("PathFace", 1);

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
                    // foreach(JSONNode n in jsonNode)
                    // {
                    //     idList.Add(n["id"]);
                    //     eyeleft.Add(n["Eyeleft"]);
                    //     eyeright.Add(n["Eyeright"]);
                    //     nose.Add(n["nose"]);
                    //     mouse.Add(n["mouse"]);
                    // }

                    // datalist.Add(idList);
                    // datalist.Add(eyeleft);
                    // datalist.Add(eyeright);
                    // datalist.Add(nose);
                    // datalist.Add(mouse);

                    // LoadImage("EyeLeft", eyeleft[0]);
                    // LoadImage("EyeRight", eyeright[0]);
                    // LoadImage("Nose", nose[0]);
                    // LoadImage("Mouth", mouse[0]);

                    break;
            }
        }
    }

    private void LoadImage(string folder, string filename){
        string path = @"C:\Users\INK\Desktop\Face-Detection\";
        string path_2 = folder + @"\" + filename; 

        byte[] bytes = File.ReadAllBytes(path + path_2);

        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(bytes);

        Texture2D targetTexture = ResizeTexture(loadTexture, 65, 45);

        Sprite sprite = Sprite.Create(targetTexture, new Rect(0, 0, targetTexture.width, targetTexture.height), new Vector2(0.5f, 0.5f));


        // OpenPathFace.transform.Find(folder).transform.GetComponent<SpriteRenderer>().sprite = sprite;

        // dataPathFace.Add(sprite);     
    }

    static Texture2D ResizeTexture(Texture2D srcTexture, int newWidth, int newHeight) {
        var resizedTexture = new Texture2D(newWidth, newHeight);
        Graphics.ConvertTexture(srcTexture, resizedTexture);
        return resizedTexture;
    }
}
