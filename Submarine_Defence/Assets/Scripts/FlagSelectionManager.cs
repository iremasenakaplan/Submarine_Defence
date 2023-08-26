using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagSelectionManager : MonoBehaviour
{
    public GameObject flagPanel; // �lke bayraklar�n�n bulundu�u panel
    public Button flagButton;    // Ana bayrak butonu
    public Image flagImage;      // Ana bayrak resmi


    private void Start()
    {
        flagButton.onClick.AddListener(OpenFlagPanel);
    }

    private void OpenFlagPanel()
    {
        flagPanel.SetActive(true);
    }

    public void ChangeFlagImage(Sprite newFlag)
    {
        flagImage.sprite = newFlag;
    }
}
