using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Person[] people;
    [SerializeField] Level[] levels;
    [SerializeField] int CurrentLevel;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject scoreScreen;
    [SerializeField] GameObject nextButton;
    [SerializeField] TMP_Text nextButtonText;
    [SerializeField] TMP_Text levelText;
    float totalScore = 0;
    Image i;
    void Start()
    {
        i = nextButton.GetComponent<Image>();
    }

    
    void Update()
    {
        if (levels[CurrentLevel].AllSeatsTaken())
        {
            i.color = Color.green;
        }
        else if (scoreScreen.activeInHierarchy == true)
            i.color = Color.white;
        else i.color = Color.red;
    }
    public void Next()
    {

        float levelScore = 0;
        if ((CurrentLevel + 1) < levels.Length)
        {
            if (levels[CurrentLevel].AllSeatsTaken() && scoreScreen.activeInHierarchy == false)
            {
                for (int i = 0; i < people.Length; i++)
                {
                    levelScore += people[i].Contentedness();
                    people[i].DeepCopy();
                }
                for (int j = 0; j < people.Length; j++)
                {
                    people[j].Change();
                    people[j].BackToStart();
                }
                totalScore += levelScore;
                levels[CurrentLevel].levelObject.SetActive(false);
                
                scoreText.text = "Score for this stage: " + levelScore.ToString();
                scoreScreen.SetActive(true);
                nextButtonText.text = "Next Level";
            }
            else if (scoreScreen.activeInHierarchy == true)
            {
                CurrentLevel++;
                scoreScreen.SetActive(false);
                levels[CurrentLevel].levelObject.SetActive(true);
                nextButtonText.text = "Complete";
            }
        }
        else
        {
            if (levels[CurrentLevel].AllSeatsTaken() && scoreScreen.activeInHierarchy == false)
            {
                for (int i = 0; i < people.Length; i++)
                {
                    levelScore += people[i].Contentedness();
                }
                for (int i = 0; i < people.Length; i++)
                {
                    people[i].BackToStart();
                }
                totalScore += levelScore;
                levels[CurrentLevel].levelObject.SetActive(false);
                scoreText.text = "Score for this stage: " + levelScore.ToString() + "\nTotal Score: " + totalScore.ToString();
                scoreScreen.SetActive(true);
                nextButtonText.text = "Try Again";
            }
            else if (scoreScreen.activeInHierarchy == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        levelText.text = "Level " + (CurrentLevel+1);
    }
    public void StartLevel()
    {

    }
    public void LeaveGame()
    {
        Application.Quit();
    }
}
