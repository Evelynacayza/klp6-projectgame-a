using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class elara : MonoBehaviour
{
    // Variabel yang bisa diatur dari Inspector Unity
    [Header("Pengaturan Gerakan")]
    public float kecepatanJalan = 7f;
    public float kekuatanLompat = 16f;
    public int lompatanMaksimal = 2;

    [Header("Pengecekan Tanah")]
    public bool sedangDiTanah; // Variabel ini tidak perlu diatur dari Inspector, karena akan dihitung di FixedUpdate
    public Transform titikCekTanah; 
    public float radiusCekTanah = 0.2f;
    public LayerMask layerTanah;

    [Header("Pengaturan Nyawa")]
    public int nyawaMaksimal = 3;

    [Header("Pengaturan UI")]
    public TextMeshProUGUI teksSkor; 
    
    [Header("Pengaturan Scene Game Over")] 
    public string namaSceneGameOver = "GameOverScene"; 

    [Header("Pengaturan Jatuh")]
    public float batasJatuhY = -10f;



    // Variabel privat untuk komponen dan state
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float inputHorizontal;
    private bool sedangMenghadapKanan = true;
    private int nyawaSaatIni;
    private int skorSaatIni;
    private int sisaLompatan;

    Animator anim;

    // Start dipanggil sebelum frame pertama
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        nyawaSaatIni = nyawaMaksimal;

        skorSaatIni = 0;
        UpdateTeksSkor();

        sisaLompatan = lompatanMaksimal;
        anim = GetComponent<Animator>();
    }

    // Update dipanggil setiap frame (untuk input)
    void Update()
    {
        if (sedangDiTanah == true)
        {
            anim.SetBool("lompat", true);
        }
        else
        {
            anim.SetBool("lompat", false);
        }
        
        if (sedangMenghadapKanan == false && inputHorizontal > 0 ||
               sedangMenghadapKanan == true && inputHorizontal < 0)
        {
            anim.SetBool("jalan", false);
        }
        else if (inputHorizontal != 0)
        {
            anim.SetBool("jalan", true);
        }
        else
        {
            anim.SetBool("jalan", false);
        }

        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && sisaLompatan > 0)
        {
            Lompat();
        }

        BalikArahSprite();

        if (transform.position.y < batasJatuhY)
        {
            Mati(); 
        }
    }

    // FixedUpdate dipanggil dalam interval waktu yang tetap (untuk fisika)
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputHorizontal * kecepatanJalan, rb.linearVelocity.y);

        bool sedangDiTanah = Physics2D.OverlapCircle(titikCekTanah.position, radiusCekTanah, layerTanah);

        if (sedangDiTanah)
        {
            sisaLompatan = lompatanMaksimal;
        }
    }

    private void Lompat()
    {
        rb.linearVelocity = new Vector2( rb.linearVelocity.x, kekuatanLompat);
        sisaLompatan--; 
    }

    private void BalikArahSprite()
    {
        if (inputHorizontal < 0 && sedangMenghadapKanan)
        {
            sedangMenghadapKanan = false;
            spriteRenderer.flipX = true;
        }
        else if (inputHorizontal > 0 && !sedangMenghadapKanan)
        {
            sedangMenghadapKanan = true;
            spriteRenderer.flipX = false;
        }
    }

    public void TambahPoin(int nilaiPoin)
    {
        skorSaatIni += nilaiPoin;
        UpdateTeksSkor(); // Setiap kali skor berubah, update teks di layar
        Debug.Log("Poin didapat! Skor sekarang: " + skorSaatIni);
    }

    private void UpdateTeksSkor()
    {
        if (teksSkor != null)
        {
            teksSkor.text = "Score: " + skorSaatIni;
        }
    }

    public void TerimaKerusakan(int jumlahKerusakan)
    {
        nyawaSaatIni -= jumlahKerusakan;

        if (nyawaSaatIni <= 0)
        {
            Mati();
        }
    }

    private void Mati()
    {
        PlayerPrefs.SetInt("Score", skorSaatIni);
        PlayerPrefs.Save(); // Penting untuk memastikan data tersimpan
        SceneManager.LoadScene(namaSceneGameOver); // Memuat scene Game Over yang ditentukan
    }


    private void OnDrawGizmosSelected()
    {
        if (titikCekTanah == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(titikCekTanah.position, radiusCekTanah);
    }
}