using UnityEngine;

public class musuh2 : MonoBehaviour
{
    [Header("Pengaturan Musuh")]
    public int kerusakan = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            elara player = collision.gameObject.GetComponent<elara>();

            if (player != null)
            {
                player.TerimaKerusakan(kerusakan);
            }
        }
    }
}