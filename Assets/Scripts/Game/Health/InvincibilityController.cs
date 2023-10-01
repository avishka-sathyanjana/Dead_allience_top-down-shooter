using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private HealthController _healthController;
    private AudioSource _audioSource;
    public AudioClip invincibilitySound;


    private void Awake()
    {
        _healthController = GetComponent<HealthController>(); //get health controller comonent
        _audioSource = GetComponent<AudioSource>();
    }

    public void StartInvincibility(float invincibilityDuration)
    {
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration));
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration) //IEnumerattor can execut the code in multiple frames
    {
        _healthController.IsInvincible = true;

                // Play the invincibility sound if available
        if (invincibilitySound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(invincibilitySound);
        }

        yield return new WaitForSeconds(invincibilityDuration); ///tell to wait for period of time
        _healthController.IsInvincible = false;
    }
}
