using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private SceneNames SceneName = SceneNames.SecretRoom;
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            GameInstance.Instance.LoadLevel(SceneName.ToString());
        }
    }
}
