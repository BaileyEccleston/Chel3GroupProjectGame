using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeBridge : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform player1;
    public Transform player2;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set the number of points to 2 (Start and End)
        lineRenderer.positionCount = 2;
    }

    void LateUpdate()
    {
        if (player1 != null && player2 != null)
        {
            // Update the positions to match the current player locations
            lineRenderer.SetPosition(0, player1.position);
            lineRenderer.SetPosition(1, player2.position);
        }
    }
}
