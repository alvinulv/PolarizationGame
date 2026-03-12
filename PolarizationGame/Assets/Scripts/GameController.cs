using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Person[] people;
    [SerializeField] Level[] levels;
    [SerializeField] int CurrentLevel;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject scoreScreen;
    [SerializeField] GameObject finalScoreScreen;
    [SerializeField] TMP_Text finalScoreText;
    float totalScore = 0;
    bool inBetween = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void Next()
    {

        float levelScore = 0;
        if (CurrentLevel < levels.Length)
        {
            if (levels[CurrentLevel].AllSeatsTaken() && scoreScreen.activeInHierarchy == false)
            {
                for (int i = 0; i < people.Length; i++)
                {
                    levelScore += people[i].Contentedness();
                    people[i].Change();
                }
                totalScore += levelScore;
                levels[CurrentLevel].levelObject.SetActive(false);
                CurrentLevel++;
                scoreText.text = "Score for this stage: " + levelScore.ToString();
                scoreScreen.SetActive(true);
            }
            else if (scoreScreen.activeInHierarchy == true)
            {
                scoreScreen.SetActive(false);
                levels[CurrentLevel].levelObject.SetActive(true);
            }
        }
        else
        {
            if (levels[CurrentLevel].AllSeatsTaken() && finalScoreScreen.activeInHierarchy == false)
            {
                for (int i = 0; i < people.Length; i++)
                {
                    levelScore += people[i].Contentedness();
                    people[i].Change();
                }
                totalScore += levelScore;
                levels[CurrentLevel].levelObject.SetActive(false);
                finalScoreText.text = "Score for this stage: " + levelScore.ToString() + "\nTotal Score: " + totalScore.ToString();
                finalScoreScreen.SetActive(true);
            }
            else if (finalScoreScreen.activeInHierarchy == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
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
