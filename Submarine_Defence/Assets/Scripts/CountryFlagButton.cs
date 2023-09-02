using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryFlagButton : MonoBehaviour
{
    public Sprite countryFlag;

    private FlagSelectionManager flagSelectionManager;

    private void Start()
    {
        flagSelectionManager = FindObjectOfType<FlagSelectionManager>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(ChangeFlag);
    }

    private void ChangeFlag()
    {
        flagSelectionManager.ChangeFlagImage(countryFlag);
    }
}
