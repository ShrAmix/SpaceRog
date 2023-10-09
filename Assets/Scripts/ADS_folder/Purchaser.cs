using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {
        switch(product.definition.id)
        {
            case "com.ifoxp.space.rog.buyship2":
                ShipBuy(1);
                break;
            case "com.ifoxp.space.rog.buyship3":
                ShipBuy(2);
                break;
            case "com.ifoxp.space.rog.buyship4":
                ShipBuy(3);
                break;
            case "com.ifoxp.space.rog.buy100coins":
                AddCoins(100);
                break;
            case "com.ifoxp.space.rog.buy300coins":
                AddCoins(300);
                break;
            case "com.ifoxp.space.rog.buy500coins":
                AddCoins(500);
                break;
            case "com.ifoxp.space.rog.buy1000coins":
                AddCoins(1000);
                break;
            case "com.ifoxp.space.rog.buy2500coins":
                AddCoins(2500);
                break;
            case "com.ifoxp.space.rog.buy5000coins":
                AddCoins(5000);
                break;
        }
    }

    private void ShipBuy(int index)
    {
        PlayerPrefs.SetInt("ship"+$"{index}"+"Access", 1);
        Debug.Log("Purchase: shipBuy");
        ShipBuyActive.shipBuyActive.ShipWhy();
        GameManager.instance.LoadScene(7);
    }
    private void AddCoins(int index)
    {
        int coin = PlayerPrefs.GetInt("Money");
        coin += index;
        PlayerPrefs.SetInt("Money",coin);
        MenuScore.Instance.ScoreTXT(PlayerPrefs.GetInt("BestScore"), PlayerPrefs.GetInt("Money"));

    }
}
