using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject levelObject;
    [SerializeField] Seat[] levelSeats;

    public bool AllSeatsTaken()
    {
        for (int i = 0; i < levelSeats.Length; i++)
        {
            if (levelSeats[i].occupant == null)
            {
                return false;
            }
        }
        return true;
    }
}
