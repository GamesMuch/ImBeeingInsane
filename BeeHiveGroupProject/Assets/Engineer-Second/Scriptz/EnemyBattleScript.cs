using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleScript : MonoBehaviour
{
    public AllyBattleScript allyBattleScript;
    public BattleUIScript battleUIScript;
    public GameObject attackTarget;
    public float damageAmount = 3;

    public Slider hpBar;
    public float maxHp;
    public float currentHp;
    public float damageModifier = 0.33f;
    public float damageTaken = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpBar.maxValue = maxHp;
        currentHp = maxHp;
        hpBar.value = currentHp;
    }


    public void TimeToAttack()
    {
            ChooseTarget();
            DoDamage();
    }

    void ChooseTarget()
    {
        int randomNumber = Random.Range(0,battleUIScript.alliesInScene.Length);
        attackTarget = battleUIScript.alliesInScene[randomNumber];
        Debug.Log("target that is attacked" + attackTarget);
    }

    void DoDamage()
    {
        allyBattleScript.damageTaken = damageAmount;
        allyBattleScript = attackTarget.GetComponent<AllyBattleScript>();
        allyBattleScript.TakeDamage();
        Debug.Log(allyBattleScript.damageTaken + " is the amount of damage the enemy gives");
    }

    public void TakeDamage()
    {
        currentHp -= damageTaken * damageModifier;
        hpBar.value = currentHp;
    }

}
