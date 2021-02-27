using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectActiver : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null;
    [SerializeField]
    private float second = 1;
    [SerializeField]
    private UnityEvent onAfterSecond;

    private float time;

    public void OnActive()
    {
        target.SetActive(true);
        time = 0;
    }

    public void OnDeActive()
    {
        target.SetActive(false);
    }

    public void Update()
    {
        if(target.activeSelf)
        {
            time += Time.deltaTime;

            if (time >= second)
                onAfterSecond.Invoke();
        }
    }
}
