using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TextBubble : MonoBehaviour
{
    [SerializeField] RectTransform bubbleCamCheck;
    [SerializeField] GameObject triangleLeft;
    [SerializeField] GameObject triangleRight;
    [SerializeField] RectTransform noTriBubble;
    [SerializeField] GameObject bubble;
    [SerializeField] TMP_Text bubbleText;

    Person p;
    Drag drag;
    Camera _camera;
    Vector3 pos;
    Vector3 relativePos;
    bool toggle = false;
    float bubbleX;
    float bubbleY;



    void Start()
    {
        bubbleX = noTriBubble.position.x - transform.position.x;
        bubbleY = noTriBubble.position.y - transform.position.y;
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        drag = GetComponent<Drag>();
        p = GetComponentInChildren<Person>();
    }

    void Update()
    {
        Vector3 mouse = _camera.ScreenToWorldPoint(Input.mousePosition);
        pos = transform.position;
        relativePos = pos - mouse;
        if (relativePos.magnitude < 10.01f && !drag.dragon && !toggle && transform.position.y < 4)
        {

            bubbleText.text = p.Dialogue();

            triangleLeft.SetActive(true);
            triangleRight.SetActive(false);

            float finalX = bubbleX;
            float finalY = bubbleY;
            noTriBubble.position = new Vector3(bubbleX, bubbleY, 0);

            if ((bubble.transform.position.x + bubbleCamCheck.position.x) < -8.88f)
            {
                finalX = -bubbleX;
                triangleLeft.SetActive(false);
                triangleRight.SetActive(true);
            }
            if ((bubble.transform.position.y + bubbleCamCheck.position.y) > 5)
            {
                finalY -= (bubble.transform.position.y + bubbleCamCheck.position.y) - 5;
            }

            noTriBubble.position = new Vector3(transform.position.x + finalX, transform.position.y + finalY, 0);
            bubble.SetActive(true);

            toggle = true;
        }
        else if ((relativePos.magnitude > 10.01f && toggle) || (drag.dragon && toggle))
        {
            bubble.SetActive(false);
            toggle = false;
        }


    }

    
}
