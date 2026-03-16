using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    [SerializeField] List<Person> people;
    [SerializeField] GameObject parent;
    List<float> r;
    List<float> t;
    List<float> o;
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
        r.Clear();
        o.Clear();
        t.Clear();
        for (int i = 0; i < people.Count; i++)
        {
            r.Add(people[i].racism);
            o.Add(people[i].openness);
            t.Add(people[i].tolerance);
        }
    }
    public void Change()
    {
        Debug.Log("Changing");
        DeepCopy();
        for (int i = 0; i < people.Count; i++)
        {
            Debug.Log("Racism + " + r[i] * openness);
            Debug.Log("Tolerance + " + t[i] * openness);
            Debug.Log("Openness + " + o[i] * openness);
            racism += r[i] * openness;
            tolerance += t[i] * openness;
            openness += o[i] * openness;
            if (people[i].face != face)
                racism = racism / openness;
        }
    }
    public void BackToStart()
    {
        
        parent.transform.position = startPos;
        
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
