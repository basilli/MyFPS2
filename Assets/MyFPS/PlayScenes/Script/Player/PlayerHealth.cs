using UnityEngine;
using System.Collections;
using MyFPS;

/* [0] ���� : PlayerHealth
*/

public class PlayerHealth : MonoBehaviour, IDamageable
{
    // [1] Variable.
    #region Variable
    // [ ] - 1) ü��.
    private float currentHealth;
    [SerializeField] private float maxHealth = 20f;
    private bool isDeath = false;
    // [ ] - 2) ������ ȿ��.
    public GameObject damageFlash;
    public AudioSource hurt01;
    public AudioSource hurt02;
    public AudioSource hurt03;
    // [ ] - 3) ���� ó��.
    public SceneFader fader;
    [SerializeField] private string loadToScene = "GameOver";
    #endregion Variable





    // [2] Unity Event Method.
    #region Unity Event Method
    // [ ] - 1) Start.
    private void Start()
    {
        // [ ] - [ ] - 1) �ʱ�ȭ.
        currentHealth = maxHealth;
    }
    #endregion Unity Event Method





    // [3] Custom Method.
    #region Custom Method
    // [ ] - 1) TakeDamage.
    public void TakeDamage(float damage)
    {
        // [ ] - [ ] - 1)  .
        currentHealth -= damage;
        Debug.Log($"Player CurrentHealth : {currentHealth}");
        // [ ] - [ ] - 2)  �ʱ�ȭ.
        StartCoroutine(DamageEffect());
        if (currentHealth <= 0 && isDeath == false)
        {
            Die();
        }
    }

    // [ ] - 2) DamageEffect.
    IEnumerator DamageEffect()
    {
        // [ ] - [ ] - 1) VFX.
        damageFlash.SetActive(true);
        // [ ] - [ ] - 2) SFX.
        int randNum = Random.Range(1, 4);
        if (randNum == 1)
        {
            hurt01.Play();
        }
        else if (randNum == 2)
        {
            hurt02.Play();
        }
        else
        {
            hurt03.Play();
        }
        yield return new WaitForSeconds(1f);
        damageFlash.SetActive(false);
    }

    // [ ] - 3) Die.
    private void Die()
    {
        // [ ] - [ ] - 1) .
        isDeath = true;
        // [ ] - [ ] - 2) ����ó��.
        fader.FadeTo(loadToScene);
    }
    #endregion Custom Method
}

// [ ] - ) 
// [ ] - [ ] - ) 
