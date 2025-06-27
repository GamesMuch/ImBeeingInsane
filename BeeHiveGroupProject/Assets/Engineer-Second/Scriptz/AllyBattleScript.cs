using UnityEngine;
using UnityEngine.UI;

public class AllyBattleScript : MonoBehaviour
{
    public Slider hpBar;
    public float maxHp;
    public float currentHp;
    public float damageAmount = 3;
    public float defendAmount = 0.5f;
    public float damageTaken = 0;
    private float damageModifier = 1;

    public BattleUIScript battleUIScript;
    public EnemyBattleScript enemyBattleScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpBar.maxValue = maxHp;
        currentHp = maxHp;
        hpBar.value = currentHp;
    }


    public void TakeDamage()
    {
        if (enemyBattleScript.attackTarget = gameObject)
        {
            currentHp -= damageTaken * damageModifier;
            hpBar.value = currentHp;
            Debug.Log("Current HP value: " + currentHp);
            Debug.Log(damageModifier + "damage modifier when calculating total damage done");

            if (damageModifier != 1)
            {
                damageModifier = 1;
                Debug.Log("reset damageModifier to " + damageModifier);
            }
            CheckHP();
        }
        
    }

    public void CheckForDefense()
    {

        if (battleUIScript.affectedFigher = gameObject)
        {
            damageModifier = 0.5f;
            Debug.Log("Set damage modifier to" + damageModifier);

        }
    }

    public void DoDamage()
    {
        battleUIScript.affectedFigher = battleUIScript.enemiesInScene[battleUIScript.chosenEnemy];
        enemyBattleScript = battleUIScript.affectedFigher.GetComponent<EnemyBattleScript>();
        enemyBattleScript.damageTaken = damageAmount;
        enemyBattleScript.TakeDamage();
    }

    void CheckHP()
    {
        if (currentHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
