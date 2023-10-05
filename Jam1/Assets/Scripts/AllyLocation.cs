using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AllyLocation : MonoBehaviour
{   
    public GameObject aliado;
    public int quantity;

    public GameObject goldManager;
    public TMP_Text TMPtext;
    private void Start() {
        TMPtext.text=(quantity*5).ToString();
    }
    public delegate void OnAllyGet(GameObject Object,int quantity);
    public static OnAllyGet onAllyGet;
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (goldManager.GetComponent<GoldManager>().Pay(quantity*5))
        {
            onAllyGet?.Invoke(aliado, quantity);
        }
        
    }

}
