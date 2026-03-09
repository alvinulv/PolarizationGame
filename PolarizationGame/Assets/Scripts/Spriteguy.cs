using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spriteguy : MonoBehaviour
{
    [SerializeField] List<Color> colors;
    // Start is called before the first frame update
    void Start()
    {
        Color c = colors[Random.Range(0, colors.Count)];
        GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
