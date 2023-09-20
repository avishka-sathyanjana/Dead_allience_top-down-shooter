using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private HealthController _healthController;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>(); //get health controller comonent
    }

    public void StartInvincibility(float invincibilityDuration)
    {
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration));
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration) //IEnumerattor can execut the code in multiple frames
    {
        _healthController.IsInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration); ///tell to wait for period of time
        _healthController.IsInvincible = false;
    }
}
