using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    private AudioSource _audioSource;
    public AudioClip deathSound;

    public float RemainingHealthPercentage //to get the remaining ealth
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    //not to take damege in short period of time
    public bool IsInvincible
    {
        get;
        set;

    }

    public UnityEvent OnDied;

    public UnityEvent OnDamaged;

    public UnityEvent OnHealthChanged;

     private void Awake()
    {
        _audioSource = GetComponent<AudioSource>(); //get AudioSource component
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth == 0) //when current health is 0, it will not take damage
        {
            return;
        }

        if (IsInvincible)
        {
            return;
        }

        _currentHealth -= damageAmount; //reducng the health
        OnHealthChanged.Invoke();

        if (_currentHealth < 0) //not going for minus health
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {

           if(deathSound != null && _audioSource != null){
                _audioSource.PlayOneShot(deathSound);
           }
            OnDied.Invoke();  //this event will fire and tell all its subscribers that the object has died
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (_currentHealth == _maximumHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;
        OnHealthChanged.Invoke();

        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }
}