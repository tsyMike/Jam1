using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes it so The object becomes transparent when theres something behind it
public class DimObject : MonoBehaviour
{
    //public BoxCollider2D Trigger;
    private SpriteRenderer sr;
    public float opacity=0.5f;

    private bool transparent=false;

    private void Start() {
        sr=GetComponent<SpriteRenderer>();

    }
    private void OnTriggerStay2D(Collider2D other) {
        if (!transparent)
        {
            Color tmp = sr.color;
            tmp.a= opacity;
            sr.color=tmp;
            transparent=true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        Color tmp = sr.color;
        tmp.a= 255;
        sr.color=tmp;
        transparent=false;
    }
   
}
