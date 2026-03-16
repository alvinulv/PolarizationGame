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
    [SerializeField] float baseValue;
    public bool face;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
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
            if (face != people[i].face)
            {
                c -= racism / tolerance;
            }
            else
            {
                c += racism;
            }
            c += racism - people[i].racism;
        }
        return c*tolerance;
    }
    void DeepCopy()
    {
        preople.Clear();
        for (int i = 0; i < people.Count; i++)
        {
            preople.Add(new Person());
            preople[i].racism = people[i].racism;
            preople[i].openness = people[i].openness;
            preople[i].tolerance = people[i].tolerance;
        }
    }
    public void Change()
    {
        Debug.Log("Changing");
        DeepCopy();
        for (int i = 0; i < preople.Count; i++)
        {
            Debug.Log("Racism + " + preople[i].racism * openness);
            Debug.Log("Tolerance + " + preople[i].tolerance * openness);
            Debug.Log("Openness + " + preople[i].openness * openness);
            racism += preople[i].racism * openness;
            tolerance += preople[i].tolerance * openness;
            openness += preople[i].openness * openness;
            if (preople[i].face != face)
                racism = racism / openness;
        }
    }
    public void BackToStart()
    {
        transform.parent.position = startPos;
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
