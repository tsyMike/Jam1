using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{
    public GameObject target=null;
    public int HealthValue=3;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        target.GetComponent<Damageable>().MaxHealth+=HealthValue;
        target.GetComponent<Damageable>().Health+=HealthValue;
    }

    
}
