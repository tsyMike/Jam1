using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyLocation : MonoBehaviour
{   
    public GameObject aliado;
    public int quantity;
    public delegate void OnAllyGet(GameObject Object,int quantity);
    public static OnAllyGet onAllyGet;
    
    private void OnCollisionEnter2D(Collision2D other) {
        onAllyGet?.Invoke(aliado, quantity);
    }

}
