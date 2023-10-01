using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    [SerializeField]
    private AudioClip[] _attackAudioClips; // Array of audio clips for attack sounds

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            // if (_attackAudioClips.Length > 0)
            // {
            //     AudioClip randomAttackClip = _attackAudioClips[Random.Range(0, _attackAudioClips.Length)];
            //     AudioSource.PlayClipAtPoint(randomAttackClip, transform.position);

            //     StartCoroutine(WaitForAudioClipToEndAndAttackNext(randomAttackClip.length));
            // }

            var healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.TakeDamage(_damageAmount);
        }
    }

    // private IEnumerator WaitForAudioClipToEndAndAttackNext(float clipLength)
    // {
    //     yield return new WaitForSeconds(clipLength);
    //     AttackWithRandomClip(); // Attack with the next random clip after the current one is finished
    // }

    // private void AttackWithRandomClip()
    // {
    //     if (_attackAudioClips.Length > 0)
    //     {
    //         AudioClip randomAttackClip = _attackAudioClips[Random.Range(0, _attackAudioClips.Length)];
    //         AudioSource.PlayClipAtPoint(randomAttackClip, transform.position);

    //         StartCoroutine(WaitForAudioClipToEndAndAttackNext(0.5f));
    //     }
    // }
}
