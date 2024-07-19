using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisintegration : MonoBehaviour
{
    [SerializeField] private float respawnTimeSeconds = 2;
    public ParticleSystem disintegrationParticles;
    private MeshRenderer meshRenderer;

    private bool canCast = true;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && canCast)
        {
            DoDisintegration();
        }
    }

    void DoDisintegration()
    {
        meshRenderer.enabled = false;
        canCast = false;
        disintegrationParticles.Play();
        StartCoroutine(RespawnAfterTime());
    }

    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(respawnTimeSeconds);
        meshRenderer.enabled = true;
        canCast = true;
    }
}
