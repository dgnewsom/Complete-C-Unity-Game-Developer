using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHP = 10;
    [SerializeField][Tooltip("Adds amount to maxHP when enemy dies")] int difficultyRamp = 1;

    int currentHP = 0;
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHP = maxHP;
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHP--;
        if(currentHP <= 1)
        {
            enemy.RewardGold();
            gameObject.SetActive(false);
            maxHP += difficultyRamp;
        }
    }
}
