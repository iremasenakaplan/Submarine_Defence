using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TMP_Text enemiesInfo;
    [SerializeField] TMP_Text earned;
    [SerializeField] GameObject _wonPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateEnemyCount(int killedEnemies, int totalEnemies)
    {

        enemiesInfo.text = killedEnemies.ToString() + "/" + totalEnemies.ToString();
    }

    public void Won()
    {
        PlayerPrefs.SetInt(Application.identifier + "Level", PlayerPrefs.GetInt(Application.identifier + "Level") + 1);
        int earning = Random.Range(600, 1200);
        earned.text = "Earned : " + earning;
        PlayerPrefs.SetInt(Application.identifier + "BANK", PlayerPrefs.GetInt(Application.identifier + "BANK")+earning);
        _wonPanel.SetActive(true);
    }


    public void NextLevel() {
        SceneManager.LoadScene("GameScene");
    }

    public void Home()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Unity'nin Update i�levi, d��man say�s�n� g�ncellemek i�in kullan�labilir


}
