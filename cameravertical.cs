using UnityEngine;

public class cameravertical : MonoBehaviour
{
    [Header("Target yang Diikuti")]
    public Transform target;

    [Header("Pengaturan Kamera")]
    public float smoothing = 5f; 
    public Vector3 offset = new Vector3(0, 2, -10); 

    private float yPosisiTertinggi;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target != null)
        {
            // Atur posisi tertinggi awal sama dengan posisi awal pemain
            yPosisiTertinggi = target.position.y;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        
        if (target.position.y > yPosisiTertinggi)
        {
            
            yPosisiTertinggi = target.position.y;
        }

        
        Vector3 posisiTujuan = new Vector3(transform.position.x, yPosisiTertinggi, transform.position.z) + offset;
        
        posisiTujuan.x = 0;
        
        transform.position = Vector3.Lerp(transform.position, posisiTujuan, smoothing * Time.deltaTime);
    }
}

