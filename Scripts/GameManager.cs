using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PanelAnswer;
    [SerializeField] public GameObject ButtonAns;
    public int length_arr = 4;
    public List<int> User_ans = new List<int>();
}
