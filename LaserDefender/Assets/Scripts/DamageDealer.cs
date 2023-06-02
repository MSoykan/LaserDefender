 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    
    public int GetDamageAmount()
    {
        return damage;
    }
    public void Hit()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(DisableAfterSeconds(2.5f));
    }

    private IEnumerator DisableAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }

}
