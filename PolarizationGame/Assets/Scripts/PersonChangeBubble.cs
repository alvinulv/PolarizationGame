using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonChangeBubble : MonoBehaviour
{
    [SerializeField] Person person;
    TMP_Text text;




    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Awake()
    {
        //text.text = person.ChangeDialouge();
    }

    void Update()
    {
        
    }
}
