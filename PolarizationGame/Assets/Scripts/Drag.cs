using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    Camera _camera;
    public Vector3 pos;
    public bool dragon;
    List<Vector3> destinations;
    Vector3 relativePos;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        destinations = new List<Vector3>();
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
            if (destinations.Count == 0)
            {
                transform.position = pos;
            }
            else
            {
                
                Vector3 d = destinations[0];
                for (int i = 1; i < destinations.Count; i++)
                {
                    Vector3 di = transform.position-destinations[i];
                    Vector3 ff = transform.position - d;
                    if (di.magnitude <ff.magnitude)
                        d = destinations[i];
                }
                transform.position = d;

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject seat = collision.gameObject;
        if (seat.CompareTag("Seat") && seat.GetComponent<Seat>().occupant == null)
        {
            destinations.Add(seat.transform.position);
            seat.GetComponent<Seat>().occupant = gameObject;
        }       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject seat = collision.gameObject;
        if (seat.CompareTag("Seat") && seat.GetComponent<Seat>().occupant == gameObject)
        {
            seat.GetComponent<Seat>().occupant = null;
            destinations.Remove(seat.transform.position);
        }
    }
}
