using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleteStatus : MonoBehaviour
{
    public bool canServe; //Holding ball, can only walk side to side
    public bool isServing; //Actively serving, can not move at all

    public GameObject servePoint;
}
