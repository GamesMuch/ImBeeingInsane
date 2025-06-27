using UnityEngine;

public class BattleUIScript : MonoBehaviour
{
    public GameObject choosePlayer;
    public GameObject choosePlayer1;
    public GameObject choosePlayer2;
    public GameObject chooseAttack;
    public GameObject chooseDefend;
    public GameObject chooseHighPitch;
    public GameObject pickTargetEnemy;
    public GameObject pickTargetEnemy1;
    public GameObject pickTargetEnemy2;
    public GameObject pickTargetPlayer;
    public GameObject pickTargetPlayer1;
    public GameObject pickTargetPlayer2;
    public GameObject hpBarPlayer;
    public GameObject hpBarPlayer1;
    public GameObject hpBarPlayer2;
    public GameObject hpBarEnemy;
    public GameObject hpBarEnemy1;
    public GameObject hpBarEnemy2;


    public GameObject[] alliesInScene;
    public GameObject[] enemiesInScene;

    public int activeAlly = 0;
    public int chosenAction = 0;
    public int chosenEnemy = 0;
    public int chosenAlly = 0;
    public GameObject affectedFigher;

    private EnemyBattleScript enemyBattleScript;
    private AllyBattleScript allyBattleScript;

    public bool playerTurn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        choosePlayer.SetActive(false);
        choosePlayer1.SetActive(false);
        choosePlayer2.SetActive(false);
        chooseAttack.SetActive(false);
        chooseDefend.SetActive(false);
        chooseHighPitch.SetActive(false);
        pickTargetEnemy.SetActive(false);
        pickTargetEnemy1.SetActive(false);
        pickTargetEnemy2.SetActive(false);
        pickTargetPlayer.SetActive(false);
        pickTargetPlayer1.SetActive(false);
        pickTargetPlayer2.SetActive(false);
        hpBarPlayer.SetActive(false);
        hpBarPlayer1.SetActive(false);
        hpBarPlayer2.SetActive(false);
        hpBarEnemy.SetActive(false);
        hpBarEnemy1.SetActive(false);
        hpBarEnemy2.SetActive(false);

        FindFightersInScene();
        SetupUI();
    }


    void FindFightersInScene()
    {
        alliesInScene = GameObject.FindGameObjectsWithTag("Allies");
        enemiesInScene = GameObject.FindGameObjectsWithTag("Enemies");
    }

    void SetupUI()
    {
        if (alliesInScene.Length >= 1)
        {
            choosePlayer.SetActive(true);
            hpBarPlayer.SetActive(true);
        }
        if (alliesInScene.Length >= 2)
        {
            choosePlayer1.SetActive(true);
            hpBarPlayer1.SetActive(true);
        }
        if (alliesInScene.Length >= 3)
        {
            choosePlayer2.SetActive(true);
            hpBarPlayer2.SetActive(true);
        }

        if (enemiesInScene.Length >= 1)
        {
            hpBarEnemy.SetActive(true);
        }
        if (enemiesInScene.Length >= 2)
        {
            hpBarEnemy1.SetActive(true);
        }
        if (enemiesInScene.Length >= 3)
        {
            hpBarEnemy2.SetActive(true);
        }

    }


    public void ChoseAlly()
    {
        activeAlly = 0;
    }

    public void ChoseAlly1()
    {
        activeAlly = 1;
    }

    public void ChoseAlly2()
    {
        activeAlly = 2;
    }

    public void ChooseAction()
    {
        chooseAttack.SetActive(true);
        chooseDefend.SetActive(true);
        chooseHighPitch.SetActive(true);
    }

    public void ChooseTargetAttack()
    {
        if (enemiesInScene.Length >= 1)
        {
            pickTargetEnemy.SetActive(true);
        }
        if (enemiesInScene.Length >= 2)
        {
            pickTargetEnemy1.SetActive(true);
        }
        if (enemiesInScene.Length >= 3)
        {
            pickTargetEnemy2.SetActive(true);
        }
        pickTargetPlayer.SetActive(false);
        pickTargetPlayer1.SetActive(false);
        pickTargetPlayer2.SetActive(false);

        chosenAction = 0;
    }

    public void ChooseTargetDefend()
    {
        pickTargetEnemy.SetActive(false);
        pickTargetEnemy1.SetActive(false);
        pickTargetEnemy2.SetActive(false);

        if (alliesInScene.Length >= 1)
        {
            pickTargetPlayer.SetActive(true);
        }
        if (alliesInScene.Length >= 2)
        {
            pickTargetPlayer1.SetActive(true);
        }
        if (alliesInScene.Length >= 3)
        {
            pickTargetPlayer2.SetActive(true);
        }

        chosenAction = 1;
    }

    public void ChooseTargetHighPitch()
    {
        if (enemiesInScene.Length >= 1)
        {
            pickTargetEnemy.SetActive(true);
        }
        if (enemiesInScene.Length >= 2)
        {
            pickTargetEnemy1.SetActive(true);
        }
        if (enemiesInScene.Length >= 3)
        {
            pickTargetEnemy2.SetActive(true);
        }
        pickTargetPlayer.SetActive(false);
        pickTargetPlayer1.SetActive(false);
        pickTargetPlayer2.SetActive(false);

        chosenAction = 2;
    }

    public void PickEnemyTarget()
    {
        chosenEnemy = 0;
        pickTargetEnemy.SetActive(false);
        pickTargetEnemy1.SetActive(false);
        pickTargetEnemy2.SetActive(false);
        chooseAttack.SetActive(false);
        chooseDefend.SetActive(false);
        chooseHighPitch.SetActive(false);
        ExecuteAction();

    }
    public void PickEnemyTarget1()
    {
        chosenEnemy = 1;
        pickTargetEnemy.SetActive(false);
        pickTargetEnemy1.SetActive(false);
        pickTargetEnemy2.SetActive(false);
        chooseAttack.SetActive(false);
        chooseDefend.SetActive(false);
        chooseHighPitch.SetActive(false);
        ExecuteAction();

    }
    public void PickEnemyTarget2()
    {
        chosenEnemy = 2;
        pickTargetEnemy.SetActive(false);
        pickTargetEnemy1.SetActive(false);
        pickTargetEnemy2.SetActive(false);
        chooseAttack.SetActive(false);
        chooseDefend.SetActive(false);
        chooseHighPitch.SetActive(false);
        ExecuteAction();

    }

    public void PickAllyTarget0() 
    { 
        PickAllyTarget(0); 
    }
    public void PickAllyTarget1() 
    { 
        PickAllyTarget(1); 
    }
    public void PickAllyTarget2() 
    { 
        PickAllyTarget(2); 
    }

    public void PickAllyTarget(int index)
    {
        chosenAlly = index;

        pickTargetPlayer.SetActive(false);
        pickTargetPlayer1.SetActive(false);
        pickTargetPlayer2.SetActive(false);
        chooseAttack.SetActive(false);
        chooseDefend.SetActive(false);
        chooseHighPitch.SetActive(false);

        ExecuteAction();
    }

    public void ExecuteAction()
    {
        if (chosenAction == 1)
        {
            affectedFigher = alliesInScene[chosenAlly];
            allyBattleScript = affectedFigher.GetComponent<AllyBattleScript>();
            allyBattleScript.CheckForDefense();
            Debug.Log(alliesInScene[chosenAlly]);
        }
        else
        {
            affectedFigher = alliesInScene[chosenAlly];
            allyBattleScript = affectedFigher.GetComponent<AllyBattleScript>();
            allyBattleScript.DoDamage();
        }
        CheckTurn();

    }

    public void CheckTurn()
    {

        int randomAttacker = Random.Range(0, enemiesInScene.Length);
        enemyBattleScript = enemiesInScene[randomAttacker].GetComponent<EnemyBattleScript>();
        enemyBattleScript.TimeToAttack();

    }
}
