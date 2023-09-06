using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RateMechanics : MonoBehaviour
{
    string appName;
    public float oppenStoreMinVal = 4;
    public int minUseCount = 2;
    string appUrl;

    Slider rateBar;
    float rate;
    bool rateOpened = false;
    int testCount = 0;
    int useCount = 0;
    public bool goToMenu = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        appName = Application.identifier;
        rateBar = transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        useCount = PlayerPrefs.GetInt(appName+"UseCount");
        PlayerPrefs.SetInt(appName+"UseCount", useCount+1);
        rate = PlayerPrefs.GetFloat(appName+"Rate", 0f);
        rateBar.value = rate;
        appUrl = "https://play.google.com/store/apps/details?id=" + Application.identifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) {
        
        // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if(useCount%minUseCount == minUseCount-1 && PlayerPrefs.GetInt(appName+"Rated")!=1 && !rateOpened){
                    OpenRatePanel();
                }
                else if(useCount%(minUseCount+1) == minUseCount && PlayerPrefs.GetInt(appName+"Rated")==1 && rate < 4 && !rateOpened){
                    OpenRatePanel();
                }
                else {
                    if(goToMenu)
                        SceneManager.LoadScene("MenuScene");
                    else
                        Application.Quit();
                }
            }
        }
    }


    public void OpenRatePanel(){
        rateOpened = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Later(){
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void RateOkay(){
        transform.GetChild(0).gameObject.SetActive(false);
        PlayerPrefs.SetFloat(appName+"Rate", rateBar.value);
        if(rateBar.value>=oppenStoreMinVal){
            
            Application.OpenURL(appUrl);
        }

        PlayerPrefs.SetInt(appName+"Rated", 1);
    }
}
