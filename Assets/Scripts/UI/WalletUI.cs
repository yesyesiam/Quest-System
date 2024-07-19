using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Wallet _wallet;

    private void Start()
    {
        GoldChange(_wallet.currentCoins);
    }

    private void OnEnable()
    {
        _wallet.CoinsChanged += GoldChange;
    }

    private void OnDisable()
    {
        _wallet.CoinsChanged -= GoldChange;
    }

    private void GoldChange(int gold)
    {
        goldText.text = gold.ToString();
    }
}
