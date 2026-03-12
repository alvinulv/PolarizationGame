using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    [SerializeField] List<Person> people;
    List<Person> preople;
    public float tolerance;
    public float openness;
    public float racism;
    public float conspiracy;
    [SerializeField] float baseValue;
    public bool face;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float Contentedness()
    {
        float c = baseValue;
        for (int i = 0; i < people.Count; i++)
        {
            if(face != people[i].face)
            {
                c -= racism/tolerance;
            }
        }
        return c;
    }
    void DeepCopy()
    {
        preople.Clear();
        for (int i = 0; i < people.Count; i++)
        {
            preople.Add(new Person());
            preople[i].racism = people[i].racism;
            preople[i].conspiracy = people[i].conspiracy;
            preople[i].openness = people[i].openness;
            preople[i].tolerance = people[i].tolerance;
        }
    }
    public void Change()
    {
        DeepCopy();
        for (int i = 0; i < preople.Count; i++)
        {
            racism += preople[i].racism * openness;
            tolerance += preople[i].tolerance * openness;
            conspiracy += preople[i].openness * openness;
            openness += preople[i].openness * openness;
            if (preople[i].face != face)
                racism = racism / openness;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("person"))
        {
            people.Add(collision.gameObject.GetComponent<Person>());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("person"))
        {
            people.Remove(collision.gameObject.GetComponent<Person>());
        }
    }
}
