using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TMP_Text enemiesInfo;
    [SerializeField] TMP_Text earned;
    [SerializeField] TMP_Text destroyed;
    //[SerializeField] TMP_Text torpedoCount;
    [SerializeField] GameObject _wonPanel;
    [SerializeField] GameObject _losePanel;
    [SerializeField] GameObject rankEarnPanel;
    [SerializeField] RanksScriptable ranksList;
    [SerializeField] Animator damageEffectAnim;
    [SerializeField] Image rankImage;
    [SerializeField] Image rankEarnImage;
    int killed;
    int totalEnemyCount;
    //int torpedoCount;;
    int earning;
    bool finished = false;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateEnemyCount(int killedEnemies, int totalEnemies)
    {
        totalEnemyCount = totalEnemies;
        //torpedoCount = totalEnemyCount + 5;
        killed = killedEnemies;
        enemiesInfo.text = killedEnemies.ToString() + "/" + totalEnemies.ToString();
    }

    public void Won()
    {
        if(!finished){
            int currentLevel = PlayerPrefs.GetInt(Application.identifier + "Level");
            PlayerPrefs.SetInt(Application.identifier + "Level", currentLevel + 1);
            earning = killed*100 + UnityEngine.Random.Range(0, 100);
            earned.text = "Earned : " + earning;
            destroyed.text = "Destroyed : " + killed.ToString();
            finished = true;
            for(int i = 1; i<ranksList.rankList.Length; i++){

                if(ranksList.rankList[i].level == currentLevel+1){
                    rankImage.sprite = ranksList.rankList[i].rankIcon;
                    rankEarnImage.sprite = ranksList.rankList[i].rankIcon;
                    rankEarnPanel.SetActive(true);
                    StartCoroutine(RankFill(1.0f));
                    
                    break;
                }
                if(ranksList.rankList[i].level > currentLevel+1){
                    rankImage.sprite = ranksList.rankList[i].rankIcon;
                    StartCoroutine(RankFill((float)(currentLevel - ranksList.rankList[i-1].level )/(float)(ranksList.rankList[i].level - ranksList.rankList[i-1].level)));
                    
                    break;
                }
            }
        
            PlayerPrefs.SetInt(Application.identifier + "BANK", PlayerPrefs.GetInt(Application.identifier + "BANK")+earning);
            _wonPanel.SetActive(true);
        }
    }

    public void LoseGame()
    {
        if(!finished){
            finished = true;
            _losePanel.SetActive(true);
        }
    }

    IEnumerator RankFill(float progress){
       // rankImage.fillAmount = progress;
        while(rankImage.fillAmount<progress){
            rankImage.fillAmount += 0.01f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void MultiplyEarning(){
        PlayerPrefs.SetInt(Application.identifier + "BANK", PlayerPrefs.GetInt(Application.identifier + "BANK")+earning*2);
        earning *= 3;
        earned.text = "Earned : " + earning;
    }

    public void AdWatchEarn(){
        Action adWatchResult = MultiplyEarning;
        GoogleMobileAdsDemoScript.Instance.UserChoseToWatchAd(adWatchResult);
    }


    public void NextLevel() {
        SceneManager.LoadScene("GameScene");
    }

    public void Home()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GetDamage(){
        if(!finished){
           // healthBar.value = healthBar.value-damage;
            damageEffectAnim.Play("Damage");
        }
    }

    // Unity'nin Update i�levi, d��man say�s�n� g�ncellemek i�in kullan�labilir


}
