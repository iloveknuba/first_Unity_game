using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Shop : MonoBehaviour
{
   

    public GameObject HeroShopCanvas;
    public GameObject EnemyShopCanvas;

    public TextMeshProUGUI heroCoins;
    public TextMeshProUGUI enemyCoins;

   
    public void OpenHeroShop()
    {
        HeroShopCanvas.SetActive(true);
    }
    public void CloseHeroShop()
    {
        HeroShopCanvas.SetActive(false);
    }

    private void Update()
    {
        heroCoins.text = ScoreManager.instance.playerScoreText.text;
        enemyCoins.text = ScoreManager.instance.enemyScoreText.text;
    }




    public void OpenEnemyShop()
    {
        EnemyShopCanvas.SetActive(true);
    }
    public void CloseEnemyShop()
    {
        EnemyShopCanvas.SetActive(false);
    }
}
