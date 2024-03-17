using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPrediction : MonoBehaviour
{
    public GameObject predictionMarkerPrefab; // Prefab of the prediction marker object
    public int predictionSteps = 20; // Number of prediction steps
    public float predictionStepTime = 0.1f; // Time interval between prediction steps

    private GameObject predictionMarker; // Prediction marker object

    // Clears the prediction marker
    public void ClearPredictionMarker()
    {
        if (predictionMarker != null)
        {
            Destroy(predictionMarker);
        }
    }

    // Predicts and marks the landing position of the ball
    public void PredictBallLanding(Vector3 initialPosition, Vector3 initialVelocity)
    {
        ClearPredictionMarker(); // Clear previous prediction marker

        Vector3 currentPosition = initialPosition;
        Vector3 currentVelocity = initialVelocity;

        for (int i = 0; i < predictionSteps; i++)
        {
            // Calculate the next position based on current velocity and time step
            currentPosition += currentVelocity * predictionStepTime;

            // Apply gravity to the velocity
            currentVelocity += Physics.gravity * predictionStepTime;
        }

        // Add a small offset to the predicted landing position based on the current velocity
        Vector3 offset = currentVelocity.normalized * 0.1f; // Adjust the offset value as needed
        currentPosition += offset;

        // Create prediction marker object at the adjusted predicted landing position
        predictionMarker = Instantiate(predictionMarkerPrefab, currentPosition, Quaternion.identity);
    }
}
