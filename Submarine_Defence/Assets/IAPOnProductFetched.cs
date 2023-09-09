using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

public class IAPOnProductFetched : MonoBehaviour
{
    // [SerializeField]
    // Text title;
    [SerializeField]
    TMP_Text price;

    public void OnProductFetched(Product product)
    {
        // if (title != null)
        // {
        //     title.text = product.metadata.localizedTitle;
        // }

        if (price != null)
        {
            price.text = product.metadata.localizedPriceString;
        }
    }
}
