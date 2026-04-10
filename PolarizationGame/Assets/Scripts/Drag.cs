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
    List<GameObject> swaps;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        destinations = new List<Vector3>();
        swaps = new List<GameObject>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouse = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                //takes the starting position, and finds the relative position of the smiley to the mouse
                pos = transform.position;
                relativePos = pos - mouse;
            }
            if (relativePos.magnitude < 10.01f)
            {
                //moves the smiley if the mouse is close enough
                dragon = true;
                transform.position = mouse + relativePos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragon = false;
            if (destinations.Count == 0 && swaps.Count== 0)
            {
                transform.position = pos;
            }
            else
            {
                Vector3 temp = Vector3.zero;
                int temp2 = 0;
                if (destinations.Count>0)
                {
                    temp = destinations[0];
                    for (int i = 1; i < destinations.Count; i++)
                    {
                        if (Vector3.Distance(transform.position, destinations[i]) < Vector3.Distance(transform.position, temp))
                            temp = destinations[i];
                    }
                }
                if (swaps.Count >0)
                {;
                    for (int i = 1; i < swaps.Count; i++)
                    {
                        if (Vector3.Distance(transform.position, swaps[i].transform.position) < Vector3.Distance(transform.position, swaps[temp2].transform.position))
                            temp2 = i;
                    }
                }
                if (swaps.Count != 0)
                {
                    if(destinations.Count == 0|| Vector3.Distance(transform.position, temp) > Vector3.Distance(transform.position, swaps[temp2].transform.position))
                    {
                        Debug.Log(swaps[temp2].transform.position);
                        temp = swaps[temp2].transform.position;
                        swaps[temp2].transform.position = pos;
                        swaps[temp2].GetComponent<Drag>().pos = pos;
                        Debug.Log(swaps[temp2].GetComponent<Drag>().pos);
                        pos = transform.position;
                    }
                }
                transform.position = temp;
                destinations.Clear();
                swaps.Clear();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ;
        GameObject seat = collision.gameObject;
        if (seat.CompareTag("Seat"))
        {
            if (seat.GetComponent<Seat>().occupant == null)
            {
                destinations.Add(seat.transform.position);
                seat.GetComponent<Seat>().occupant = gameObject;
            }
            else if (seat.GetComponent<Seat>().occupant != gameObject)
            {
                Debug.Log("asudha");
                swaps.Add(seat.GetComponent<Seat>().occupant);
            }
        }
        /*if (seat.CompareTag("personParent"))
        {
            Debug.Log(gameObject.name +pos+ collision.gameObject.name+seat.transform.position);
            seat.GetComponent<Drag>().destinations.Add(pos);
        }*/
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject seat = collision.gameObject;
        if (seat.CompareTag("Seat"))
        {
            if (seat.GetComponent<Seat>().occupant == gameObject)
            {
                seat.GetComponent<Seat>().occupant = null;
            }
            
            if (seat.GetComponent<Seat>().occupant != null)
            {
                swaps.Remove(seat.GetComponent<Seat>().occupant);
            }
            destinations.Remove(seat.transform.position);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject seat = collision.gameObject;
        if (seat.CompareTag("Seat") && seat.GetComponent<Seat>().occupant == null)
            seat.GetComponent<Seat>().occupant = gameObject;
    }
}
