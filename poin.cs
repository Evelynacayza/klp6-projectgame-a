using UnityEngine;

public class poin : MonoBehaviour
{
    [Header("Pengaturan Poin")]
    public int nilaiPoin = 50;

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            elara player = other.GetComponent<elara>();

            if (player != null)
            {
                player.TambahPoin(nilaiPoin);

                // Mainkan suara (jika ada)
                // if (suaraPoin != null) AudioSource.PlayClipAtPoint(suaraPoin, transform.position);

                // Hancurkan objek poin ini agar tidak bisa diambil lagi
                Destroy(gameObject);
            }
        }
    }
}