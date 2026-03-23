using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    [SerializeField] List<Person> people;
    [SerializeField] GameObject p;
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
        r = new List<float>();
        t = new List<float>();
        o = new List<float>();
        startPos = p.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            BackToStart();
    }
    public float Contentedness()
    {
        float c = 0;
        for (int i = 0; i < people.Count; i++)
        {
            if (racism > 0&&face != people[i].face)
            {
                c -= racism / tolerance;
            }
            c += baseValue;
        }
        return c;
    }
    public void DeepCopy()
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
        //racism = racism*Contentedness();
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].face != face)
            {
                racism -= openness;
            }
            racism += r[i]/(people.Count+1);
        }
    }
    public void BackToStart()
    {
        p.transform.position = startPos;
        p.GetComponent<Drag>().pos = startPos;
    }
    public string Dialogue()
    {

        return "ø";
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
