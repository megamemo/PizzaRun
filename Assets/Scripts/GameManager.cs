using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public int levelCurrent = 1;
    public float playTime;
    public bool gameStopped;

    [SerializeField] private TextMeshProUGUI levelText;
    
    
    void Awake()
    {
        levelText.text = "LEVEL " + (levelCurrent);

        InvokeRepeating("NextLevel", 10.0f, 10.0f);
    }

    private void FixedUpdate()
    {
        if (!gameStopped)
        {
            playTime += Time.fixedDeltaTime;
        }
    }

    private void NextLevel()
    {
        if (!gameStopped)
        {
            levelCurrent++;
            levelText.text = "LEVEL " + (levelCurrent);
        }
    }
}
