using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spriteguy : MonoBehaviour
{
    [SerializeField] Person p;
    [SerializeField] List<Color> colors;
    [SerializeField] Sprite sad;
    [SerializeField] Sprite neutral;
    [SerializeField] Sprite happy;
    [SerializeField] float sadbelow;
    [SerializeField] float happyabove;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Color c = colors[Random.Range(0, colors.Count)];
        sprite.color = new Color(c.r, c.g, c.b, 1);
    }
    // Update is called once per frame
    void Update()
    {
        if (p.Contentedness() <= sadbelow)
            sprite.sprite = sad;
        else if (p.Contentedness() >= happyabove)
            sprite.sprite = happy;
        else sprite.sprite = neutral;
    }

}
