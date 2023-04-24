using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Shop : MonoBehaviour
{
   

    public GameObject ShopCanvas;
   

   
    public void OpenShop()
    {
        ShopCanvas.SetActive(true);
    }
    public void CloseShop()
    {
        ShopCanvas.SetActive(false);
    }
}
