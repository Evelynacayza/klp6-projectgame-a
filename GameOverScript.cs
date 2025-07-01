using UnityEngine;
using UnityEngine.UI; // Untuk Text dan Button (jika menggunakan UI lama)
using TMPro; // Untuk TextMeshPro (jika menggunakan TMPro)
using UnityEngine.SceneManagement; // Untuk memuat scene

public class GameOverScript : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI finalScoreText; // Atau public Text finalScoreText; jika tidak pakai TMPro

    [Header("Scene Management")]
    public string gameSceneName = "SampleScene"; // Nama scene game utama Anda

    private int score; // Variabel untuk menyimpan skor

    void Start()
    {
        // Pastikan Time.timeScale diatur ke 1 untuk melanjutkan waktu permainan
        // jika sebelumnya dihentikan (misalnya saat game over).
        Time.timeScale = 1f;

        // Ambil skor dari PlayerPrefs atau variabel statis lainnya.
        // Di sini saya asumsikan skor disimpan di PlayerPrefs dengan key "Score".
        // Anda mungkin perlu menyesuaikan cara Anda menyimpan dan mengambil skor.
        score = PlayerPrefs.GetInt("Score", 0); // Default ke 0 jika tidak ada skor

        // Tampilkan skor akhir
        if (finalScoreText != null)
        {
            finalScoreText.text = "Score : " + score.ToString();
        }
        else
        {
            Debug.LogError("Final Score Text (TextMeshProUGUI/Text) is not assigned in the inspector!");
        }
    }

    // Fungsi yang dipanggil ketika tombol "RETRY" diklik
    public void OnRetryButtonClick()
    {
        Debug.Log("Retry button clicked!");
        SceneManager.LoadScene("SampleScene"); // Muat ulang scene game
    }

    // Fungsi yang dipanggil jika ada tombol "QUIT" atau sejenisnya
    public void OnQuitButtonClick()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit(); // Keluar dari aplikasi (hanya berfungsi di build game)
    }
}