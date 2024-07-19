using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float respawnTimeSeconds = 8;
    [SerializeField] private int goldGained = 1;

    [Header("References")]
    [SerializeField] private Wallet wallet;

    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void CollectCoin()
    {
        boxCollider.enabled = false;
        meshRenderer.enabled = false;
        //GameEventsManager.instance.goldEvents.GoldGained(goldGained);
        //GameEventsManager.instance.miscEvents.CoinCollected();
        wallet.AddCoins(goldGained);
        StopAllCoroutines();
        StartCoroutine(RespawnAfterTime());
    }

    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTimeSeconds);
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            CollectCoin();
        }
    }
}
