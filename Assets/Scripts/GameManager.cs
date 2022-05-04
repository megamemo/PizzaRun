using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int levelCurrent;

    [SerializeField] private TextMeshProUGUI levelText;
    
    
    void Awake()
    {
        levelText.text = "LEVEL " + (levelCurrent + 1);

        InvokeRepeating("NextLevel", 10.0f, 10.0f);
    }

    void FixedUpdate()
    {
        
    }

    private void NextLevel()
    {
        levelCurrent++;
        levelText.text = "LEVEL " + (levelCurrent + 1);
    }
}
