using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{   
    public int gold;
    public TMP_Text text;
    // Start is called before the first frame update
    private void Start() {
        text.text=gold.ToString();
    }
    public void Gain(int amount)
    {
        gold+=amount;
        text.text=gold.ToString();
    }
    public bool Pay(int amount)
    {
        if (gold>=amount)
        {   
            gold-=amount;
            text.text=gold.ToString();
            return(true);
        }
        return(false);
    }
}
