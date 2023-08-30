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
        _wonPanel.SetActive(true);
    }


    public void NextLevel() {
        SceneManager.LoadScene("GameScene");
    }

    public void Home()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Unity'nin Update iþlevi, düþman sayýsýný güncellemek için kullanýlabilir


}
