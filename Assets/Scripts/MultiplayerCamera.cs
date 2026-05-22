using UnityEngine;

public class MultiplayerCamera : MonoBehaviour
{

    private Transform player1;

    private Transform player2;

    public float smoothSpeed = 5f;

    public Vector3 offset= new Vector3(0, 5, -10);


    public float minFOV = 40f;
    public float maxFOV = 70f;
    public float zoomLimiter = 10f;

    private Camera cam;

    void Start()
    {
        player1 = GameObject.Find("Player1").GetComponent<Transform>();
        player2 = GameObject.Find("Player2").GetComponent<Transform>();
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null)
            return;
        Move();
        Zoom();
    }
    void Move()
    {
        Vector3 centerPoint = (player1.position + player2.position) / 2f;

        Vector3 targetPosition=centerPoint+offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
    void Zoom()
    {

        float distance = Vector3.Distance(
            player1.position,
            player2.position
        );

        float targetFOV = Mathf.Lerp(
            minFOV,
            maxFOV,
            Mathf.Clamp01(distance / zoomLimiter)
        );


        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,
            targetFOV,
            Time.deltaTime * smoothSpeed
        );
    }
}
