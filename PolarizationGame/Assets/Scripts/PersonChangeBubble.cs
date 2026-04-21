using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonChangeBubble : MonoBehaviour
{
    [SerializeField] GameObject person;
    [SerializeField] TMP_Text text;
        



    void Start()
    {

    }
    private void OnEnable()
    {
        text.text = person.GetComponent<Person>().ChangeDialogue();
    }

    void Update()
    {
        
    }
}
