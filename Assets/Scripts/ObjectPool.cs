using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Stack<GameObject> pool = new Stack<GameObject>();
    private T prefab;

    public ObjectPool(int initialCount, T prefab)
    {
        this.prefab = prefab;
        SupplyPool(initialCount);
    }

    public T GetObject()
    {
        if(pool.Count > 0)
        {
            GameObject obj = pool.Pop();
            obj.SetActive(true);
            return obj.GetComponent<T>();
        }
        else
        {
            SupplyPool(1);
            GameObject obj = pool.Pop();
            obj.SetActive(true);
            return obj.GetComponent<T>();
        }
    }

    public void RetrieveObject(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Push(obj.gameObject);
    }

    private void SupplyPool(int supplyCount)
    {
        for (int i = 0; i < supplyCount; ++i)
        {
            GameObject newObj = GameObject.Instantiate(prefab.gameObject);
            newObj.SetActive(false);
            pool.Push(newObj);
        }
    }
}
