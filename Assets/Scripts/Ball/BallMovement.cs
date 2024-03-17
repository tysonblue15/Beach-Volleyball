using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("References")]
    public GameObject gameManager;
    GameManager gameManagerInstance;
    public Rigidbody rb;
    public GameObject owner;
    GameObject ownerHitPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerInstance = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(0, 0, 0);

        if(owner != null)
        {
            if (owner.GetComponent<AthleteStatus>().canServe || owner.GetComponent<AthleteStatus>().isServing)
            {
                transform.position = owner.GetComponent<AthleteStatus>().servePoint.transform.position;
                rb.isKinematic = true; 
            }
        }
    }

    private void FixedUpdate()
    {
        if(owner == null)
        {
            //transform.position = new Vector3(transform.position.x + hor, transform.position.y, transform.position.z + frwrd); //Movement of ball

        }
    }

    public void Force(float hor, float ver, float frwd)
    {

        rb.AddForce(new Vector3(hor, ver, frwd) * gameManagerInstance.serveTotalMultiplier);
        StartCoroutine(Predict());
    }

    IEnumerator Predict()
    {
        yield return new WaitForSeconds(.1f);
        GetComponent<BallPrediction>().PredictBallLanding(transform.position, rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log(transform.position);
        }
    }
}
