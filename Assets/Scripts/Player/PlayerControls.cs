using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    GameManager gameManagerInstance;
    GameObject ball;

    [Header("Script References")]
    [SerializeField] AthleteStatus athleteStatusRefernce;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction serveAction;

    [SerializeField] float speed = 25;
    public float turnSpeed = 15f;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerInstance = gameManager.GetComponent<GameManager>();

        athleteStatusRefernce = GetComponent<AthleteStatus>();

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        serveAction = playerInput.actions.FindAction("A");

        ball = gameManager.GetComponent<GameManager>().ball;
    }

    // Update is called once per frame
    void Update()
    {
        if (!athleteStatusRefernce.isServing)
        {
            MovePlayer();
        }

        if (serveAction.IsPressed() && (athleteStatusRefernce.canServe))
        {
            StartCoroutine(Serve());
        }
    }

    void MovePlayer()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();
        
        if (!athleteStatusRefernce.canServe && !athleteStatusRefernce.isServing)
        {
            Vector3 targetDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
            transform.position += targetDirection * speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
        else
        {
            Vector3 targetDirection = new Vector3(moveDirection.x, 0, Mathf.Abs(moveDirection.y));
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            athleteStatusRefernce.servePoint.transform.rotation = Quaternion.Slerp(athleteStatusRefernce.servePoint.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
    }

    IEnumerator Serve()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();

        athleteStatusRefernce.canServe = false;
        athleteStatusRefernce.isServing = true;
        ball.transform.rotation = athleteStatusRefernce.servePoint.transform.rotation;
        ball.GetComponent<BallMovement>().rb.isKinematic = false;

        ball.GetComponent<BallMovement>().Force(moveDirection.x * gameManagerInstance.serveHorizontalMultiplier, gameManagerInstance.serveHeightForce, 
            gameManagerInstance.serveForwardForce);
        ball.GetComponent<BallMovement>().owner = null;
        yield return new WaitForSeconds(.5f);
        athleteStatusRefernce.isServing = false;
    }
}
