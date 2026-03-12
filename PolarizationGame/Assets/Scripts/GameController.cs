using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Person[] people;
    [SerializeField] Level[] levels;
    [SerializeField] int CurrentLevel;
    [SerializeField] TMP_Text scoreText;
    float totalScore = 0;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void Next()
    {
        float levelScore = 0;
        if (levels[CurrentLevel].AllSeatsTaken())
        {
            for (int i = 0; i < people.Length; i++)
            {
                levelScore += people[i].Contentedness();
                people[i].Change();
            }
            totalScore += levelScore;
            levels[CurrentLevel].levelObject.SetActive(false);
            CurrentLevel++;
            levels[CurrentLevel].levelObject.SetActive(true);
        }
    }
    public void StartLevel()
    {

    }
    public void LeaveGame()
    {
        Application.Quit();
    }
}
