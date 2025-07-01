using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Variabel untuk menampung posisi player (target yang akan diikuti)
    public Transform target;

    // Seberapa halus pergerakan kamera? Semakin kecil nilainya, semakin lambat kamera mengikuti.
    public float kecepatanHalus = 0.125f;

    // Variabel untuk menyimpan jarak awal antara kamera dan player
    private Vector3 offset;

    void Start()
    {
        // Hitung dan simpan jarak awal saat permainan dimulai
        offset = transform.position - target.position;
    }

    // LateUpdate adalah tempat terbaik untuk logika kamera
    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Tentukan posisi tujuan kamera
        Vector3 posisiTujuan = target.position + offset;

        // Gunakan fungsi Lerp untuk bergerak dari posisi saat ini ke posisi tujuan secara halus
        Vector3 posisiHalus = Vector3.Lerp(transform.position, posisiTujuan, kecepatanHalus);

        // Terapkan posisi baru ke kamera
        transform.position = posisiHalus;
    }
}