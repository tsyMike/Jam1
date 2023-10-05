using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KnockbackFeedback : MonoBehaviour
{   
    
    [SerializeField] 
    private Rigidbody2D rb2d;
    [SerializeField] 
    private float strenght = 16, delay = 0.15f; 
    
    public UnityEvent OnBegin, OnDone;

    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction=(transform.position-sender.transform.position);
        rb2d.AddForce(direction*strenght,ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    private IEnumerator Reset(){
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
    }

}
