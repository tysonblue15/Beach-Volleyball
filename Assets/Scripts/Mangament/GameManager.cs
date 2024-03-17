using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] athletes;
    public GameObject ball;

    [Header("Serve Stats")]
    public float serveHorizontalMultiplier = 75;
    public float serveHeightForce = 100;
    public float serveForwardForce = 100;
    public float serveTotalMultiplier = 7;

    // Start is called before the first frame update
    void Start()
    {
        if(athletes[0].tag == "Player")
        {
            athletes[0].GetComponent<AthleteStatus>().canServe = true;
            ball.GetComponent<BallMovement>().owner = athletes[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
