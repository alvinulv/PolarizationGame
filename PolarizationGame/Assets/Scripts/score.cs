using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    [SerializeField] Person person;
    TMP_Text t;
    
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<TMP_Text>();
        t.color = person.transform.parent.GetComponentInChildren<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        t.text = person.racism.ToString();
    }
}
