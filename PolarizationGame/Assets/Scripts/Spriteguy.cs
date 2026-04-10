using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Spriteguy : MonoBehaviour
{
    Camera cam;
    [SerializeField] Person p;

    [SerializeField] Sprite sad;
    [SerializeField] Sprite neutral;
    [SerializeField] Sprite happy;
    [SerializeField] float sadbelow;
    [SerializeField] float happyabove;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        sprite = GetComponent<SpriteRenderer>();
        /*Color c = colors[Random.Range(0, colors.Count)];
        sprite.color = new Color(c.r, c.g, c.b, 1);*/
    }
    // Update is called once per frame
    void Update()
    {

        
            Vector3 relativePos = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
            if (relativePos.magnitude < 10.01f)
            {
                sprite.color = Color.yellow;
                if (Input.GetMouseButton(0))
                    sprite.color = Color.green;
            }
            else sprite.color = Color.white;

            if (p.Contentedness() <= sadbelow)
                sprite.sprite = sad;
            else if (p.Contentedness() >= happyabove)
                sprite.sprite = happy;
            else sprite.sprite = neutral;
        

    }
}
