using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private Collider col;

    public PlayerLives playerLives;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.StartCoroutine(player.Respawn());
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider>();

    }
}