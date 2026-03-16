using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    Camera _camera;
    public Vector3 pos;
    public bool dragon;
    Vector3 destination;
    Vector3 relativePos;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouse = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                pos = transform.position;
                relativePos = pos - mouse;
            }
            if (relativePos.magnitude < 10.01f)
            {
                dragon = true;
                transform.position = mouse + relativePos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragon = false;
            if (destination == new Vector3(0,0,0))
            {
                transform.position = pos;
            }
            else transform.position = destination;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Seat"))
        {
            if (collision.gameObject.GetComponent<Seat>().occupant == null)
            {
                destination = collision.gameObject.transform.position;
                collision.gameObject.GetComponent<Seat>().occupant = gameObject;
            }
        }       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Seat") && collision.gameObject.GetComponent<Seat>().occupant == gameObject)
        {
            collision.gameObject.GetComponent<Seat>().occupant = null;
            destination = new Vector3(0, 0, 0);
        }
    }
}
