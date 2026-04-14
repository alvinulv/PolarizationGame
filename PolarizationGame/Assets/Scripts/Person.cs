using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    float startR;
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
        startR = racism;
        othersameupdate();
        if (others > 0 && sames > 0)
        {
            racism -= openness;
            if (sames < 2)
                racism-=openness;
        }
        else racism += sames;
        ChangeDialogue();
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
    public string ChangeDialogue()
    {
        if (startR == 0)
        {
            //No racism
            switch (Random.Range(0, 4))
            {
                case 0: return "Why shouldn't we all be friends?";
                case 1: if (face)
                        return "I love my small faced friends";
                    else return "Biggas are cool";
                case 2: return "faceism is actually so stupid";
                        default: return "I don't get why some small and big faces hate eachother";
            }
        }    
        if (startR > racism)
        {
            //Less racism
            switch(Random.Range(0,3))
            {
                case 0: return "Maybe I had a caricature in my head";
                case 1: if (face)
                        return "The small faced guy was actually pretty chill";
                    else return "The big faced guy was actually pretty chill";
                        default: return "They're not so bad";
            }
        }
        if (startR == racism)
        {
            //No change
            if (Random.Range(0, 2) == 1)
            {
                if (face)
                    return "The small guys were scary. I prefer my friends";
                return "The big faced guys were scary. I prefer my friends";
            }
            return "They were what I expected them to be";
        }
        if (startR< racism)
        {
            if (face)
            {
                //More racism
                switch (Random.Range(0, 3))
                {
                    case 0: return "The people with small faces are not like us";
                    case 1: return "Smalls can't be trusted";
                    default: return "It's not natural to have a small face";
                }
            }
            else
            {
                switch (Random.Range(0, 3))
                {
                    case 0: return "Problem is we're not allowed to criticize biggers";
                    case 1: return "#ProudFaceist";
                    default: return "People with big faces are violent. I don't want them here";
                }
                
            }
        }
        return "ERROR";
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
