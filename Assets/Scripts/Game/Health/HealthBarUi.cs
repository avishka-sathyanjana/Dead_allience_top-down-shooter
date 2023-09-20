using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;  // Reference to the foreground image of the health bar

    public void UpdateHealthBar(HealthController healthController)
    {
        _healthBarForegroundImage.fillAmount = healthController.RemainingHealthPercentage; //fill ammout requird 0-1
    }
}