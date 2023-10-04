using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pooler
{
    private static Dictionary<string, Pool> pools= new Dictionary<string, Pool>();
    public static void Spawn(GameObject go, Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        string key= go.name.Replace("(Clone)","");

        if (pools.ContainsKey(key))
        {
            if (pools[key].inactive.Count==0)
            {
                Object.Instantiate(go, pos, rot, pools[key].parent.transform);          
            }
            else
            {
                obj = pools[key].inactive.Pop();
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.SetActive(true);

            }
        }
        else
        {
            GameObject newParent= new GameObject($"{key}_POOL");
            Object.Instantiate(go, pos, rot, newParent.transform);
            Pool newPool = new Pool(newParent);
            pools.Add(key, newPool);
        }
    }
    public static void Despawn(GameObject go)
    {
        string key= go.name.Replace("(Clone)","");

        if (pools.ContainsKey(key))
        {
            pools[key].inactive.Push(go);
            go.transform.position = pools[key].parent.transform.position;
            go.SetActive(false);
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");
            Pool newPool = new Pool(newParent);

            go.transform.SetParent(newParent.transform);

            pools.Add(key, newPool);
            pools[key].inactive.Push(go);
            go.SetActive(false);
        }
    }
}
