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
    int others = 0;
    int sames = 0;
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
                c -= people[i].racism/tolerance;
            }
            c += baseValue;
        }
        return c;
    }
    public void DeepCopy()
    {
        //this might actually be useless
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
        othersameupdate();
        if (others > 0 && sames > 0)
        {
            racism -= openness;
            if (sames < 2)
                racism-=openness;
        }
        else racism += sames;
    }
    public void BackToStart()
    {
        p.transform.position = startPos;
        p.GetComponent<Drag>().pos = startPos;
    }
    public string Dialogue()
    {
        othersameupdate();
        if (Contentedness() >= 2)
        {
            return "I love all of my friends";
        }   
        if (others > 0)
        {
            if (racism > 3)
                if (!face)
                    return "I don't like Biggers";
                else return "I don't like them smalls";
            if (racism > 0)
            {
                if (others == 1)
                {
                    if (sames == 0)
                        return "I don't trust this guy";
                    if (face)
                        return "I'm not comfortable with the small faced guy";
                    else return "I'm not comfortable with the big faced guy";
                }
                if (sames == 0)
                {
                    return "I'm scared of these guys!";
                }
            }
        }
        if (others == 0 && sames == 0)
            return "I'm lonely";
        return "I don't have enough friends";
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
    void othersameupdate()
    {
        others = 0;
        sames = 0;
        //racism = racism*Contentedness();
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].face != face)
                others++;
            else sames++;
            /*if (people[i].face != face)
            {
                racism -= openness;
            }
            racism += r[i]/(people.Count);*/
        }
    }
}
