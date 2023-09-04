using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class MenuManager : MonoBehaviour
{

    [SerializeField] ShipScriptable[] shipParameters;
    [SerializeField] Slider gunHealth;
    [SerializeField] Slider gunDamage;
    [SerializeField] Slider gunFireR;
    [SerializeField] Image gunCountryFlag;
    [SerializeField] TMP_Text gunName;
    [SerializeField] TMP_Text gunPrice;
    [SerializeField] TMP_Text bankText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] GameObject gunLock;
    [SerializeField] Button playButton;
    [SerializeField] Button unlockGun;
    [SerializeField] Transform gunParent;
    [SerializeField] Toggle[] qualityToggles;
    [SerializeField] Toggle[] controlToggles;
    [SerializeField] Toggle vibrationToggle;

    int currentTurretIndex = 0;
    GameObject currentGun;
    int bankAccount;

        public static int currentGunIndex;

    

    // Start is called before the first frame update
    void Start()
    {
      //  PlayerPrefs.SetInt(Application.identifier + "BANK", 100000);
        if(PlayerPrefs.GetInt(Application.identifier + "Vibration")==1)
            vibrationToggle.SetIsOnWithoutNotify(true);
        bankAccount = PlayerPrefs.GetInt(Application.identifier + "BANK");
        bankText.text = bankAccount.ToString();
        levelText.text = "Lvl."+PlayerPrefs.GetInt(Application.identifier + "Level").ToString();
        gunHealth.value = shipParameters[currentTurretIndex].hp;
        gunDamage.value = shipParameters[currentTurretIndex].damage;
        gunFireR.value = shipParameters[currentTurretIndex].fireRate;
        gunName.text = shipParameters[currentTurretIndex].name;
        gunCountryFlag.sprite = shipParameters[currentTurretIndex].flag;
        currentGun = Instantiate(shipParameters[currentTurretIndex].menuGun, gunParent);
        CheckLockStatus();
        SetupQuality();

        SetControl(PlayerPrefs.GetInt(Application.identifier + "Control"));
    }

    // Update is called once per frame
    private void SetupQuality(){
        SetQuality(PlayerPrefs.GetInt(Application.identifier + "Quality", 1));
    }

    public void SetQuality(int quality){
        foreach (var item in qualityToggles)
        {
            item.SetIsOnWithoutNotify( false );
        }
        qualityToggles[quality].SetIsOnWithoutNotify( true );
        PlayerPrefs.SetInt(Application.identifier + "Quality", quality);
        QualitySettings.SetQualityLevel(quality, true);
    }

    public void SetControl(int control){
        foreach (var item in controlToggles)
        {
            item.SetIsOnWithoutNotify( false );
        }
        controlToggles[control].SetIsOnWithoutNotify( true );
        PlayerPrefs.SetInt(Application.identifier + "Control", control);
    }

    public void SetPro(){
        PlayerPrefs.SetInt(Application.identifier + "IsPro", 1);
    }

    public void MenuGoLeft() {
        if (currentTurretIndex > 0) {
            Destroy(currentGun);
            currentTurretIndex--;
            gunHealth.value = shipParameters[currentTurretIndex].hp;
            gunDamage.value = shipParameters[currentTurretIndex].damage;
            gunFireR.value = shipParameters[currentTurretIndex].fireRate;
            gunName.text = shipParameters[currentTurretIndex].name;
            gunCountryFlag.sprite = shipParameters[currentTurretIndex].flag;
            currentGun = Instantiate(shipParameters[currentTurretIndex].menuGun, gunParent);
            CheckLockStatus();
            currentGunIndex = currentTurretIndex;
        }
    }


    public void MenuGoRight()
    {
        if (currentTurretIndex < shipParameters.Length-1)
        {
            Destroy(currentGun);
            currentTurretIndex++;
            gunHealth.value = shipParameters[currentTurretIndex].hp;
            gunDamage.value = shipParameters[currentTurretIndex].damage;
            gunFireR.value = shipParameters[currentTurretIndex].fireRate;
            gunName.text = shipParameters[currentTurretIndex].name;
            gunCountryFlag.sprite = shipParameters[currentTurretIndex].flag;
            currentGun = Instantiate(shipParameters[currentTurretIndex].menuGun, gunParent);
            CheckLockStatus();
            currentGunIndex = currentTurretIndex;
        }
    }


    private void CheckLockStatus()
    {
        if (shipParameters[currentTurretIndex].price == 0 || PlayerPrefs.GetInt(shipParameters[currentTurretIndex].gunCode) == 1)
        {
            gunLock.transform.localPosition = new Vector3(gunLock.transform.localPosition.x, 2000, gunLock.transform.localPosition.z);
            playButton.gameObject.SetActive(true);
           // gunLock.SetActive(false);
        }
        else {
            playButton.gameObject.SetActive(false);
            gunLock.transform.localPosition = new Vector3(gunLock.transform.localPosition.x, 100, gunLock.transform.localPosition.z);
            gunPrice.text = shipParameters[currentTurretIndex].price.ToString();
        }

        if(bankAccount>= shipParameters[currentTurretIndex].price){
            unlockGun.interactable = true;
        }else{
            unlockGun.interactable = false;
        }
    }

    public void UnlockGunWithCoin()
    {
        if(bankAccount>=shipParameters[currentTurretIndex].price){
            bankAccount-= shipParameters[currentTurretIndex].price;
            bankText.text = bankAccount.ToString();
            PlayerPrefs.SetInt(Application.identifier + "BANK", bankAccount);
            PlayerPrefs.SetInt(shipParameters[currentTurretIndex].gunCode, 1);
            gunLock.transform.localPosition = new Vector3(gunLock.transform.localPosition.x, 2000, gunLock.transform.localPosition.z);
            playButton.gameObject.SetActive(true);
        }
    }

    public void UnlockGunWithPurchase()
    {
        PlayerPrefs.SetInt(shipParameters[currentTurretIndex].gunCode, 1);
        gunLock.transform.localPosition = new Vector3(gunLock.transform.localPosition.x, 2000, gunLock.transform.localPosition.z);
        playButton.gameObject.SetActive(true);
    }


    public void Play(){
        SceneManager.LoadScene(1);
    }

    public void BuyMoney(int amount){
        bankAccount+=amount;
        PlayerPrefs.SetInt(Application.identifier + "BANK", bankAccount);
        bankText.text = bankAccount.ToString();

    }


    public void SetLevel(TMP_InputField level){
        PlayerPrefs.SetInt(Application.identifier+"Level", int.Parse(level.text));
        levelText.text = "Lvl."+PlayerPrefs.GetInt(Application.identifier + "Level").ToString();
    }

    public void SetAccount(TMP_InputField acc){
        bankAccount = int.Parse(acc.text);
        PlayerPrefs.SetInt(Application.identifier+"BANK", bankAccount);
        bankText.text = bankAccount.ToString();
    }

    public void SetVibration(){
        if(vibrationToggle.isOn)
            PlayerPrefs.SetInt(Application.identifier + "Vibration", 1);
        else
            PlayerPrefs.SetInt(Application.identifier + "Vibration", 0);
    }
}
