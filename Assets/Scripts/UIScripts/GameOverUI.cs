﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void ActiveUI(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
