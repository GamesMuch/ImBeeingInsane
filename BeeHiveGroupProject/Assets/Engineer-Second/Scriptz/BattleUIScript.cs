/*

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BattleUIScript : MonoBehaviour
{
    //public GameObject choosePlayer;
    //public GameObject choosePlayer1;
    //public GameObject choosePlayer2;
    //public GameObject chooseAttack;
    //public GameObject chooseDefend;
    //public GameObject chooseHighPitch;

    public List<GameObject> EnemyList;
    public List<GameObject> AllyList;

    //public GameObject pickTargetEnemy;
    //public GameObject pickTargetEnemy1;
    //public GameObject pickTargetEnemy2;
    //public GameObject pickTargetPlayer;
    //public GameObject pickTargetPlayer1;
    //public GameObject pickTargetPlayer2;
    //public GameObject hpBarPlayer;
    //public GameObject hpBarPlayer1;
    //public GameObject hpBarPlayer2;
    //public GameObject hpBarEnemy;
    //public GameObject hpBarEnemy1;
    //public GameObject hpBarEnemy2;


    public GameObject[] alliesInScene;
    public GameObject[] enemiesInScene;

    public int activeAlly = 0;
    public int chosenAction = 0;
    public int chosenEnemy = 0;
    public int chosenAlly = 0;

    public int AmountOfAllies = 1;
    public int AmountOfEnemies = 1;

    Dictionary<string, (int,float)> HealthBars = new Dictionary<string, (int,float)>();


    public GameObject affectedFigher;

    private EnemyBattleScript enemyBattleScript;
    private AllyBattleScript allyBattleScript;

    public bool playerTurn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < AmountOfAllies; i++)
        {
            AllyList[i].SetActive(true);

        }
        for (int i = 0; i < AmountOfEnemies; i++)
        {
            EnemyList[i].SetActive(false);
        }


        //choosePlayer.SetActive(false);
        //choosePlayer1.SetActive(false);
        //choosePlayer2.SetActive(false);
        //chooseAttack.SetActive(false);
        //chooseDefend.SetActive(false);
        //chooseHighPitch.SetActive(false);
        //pickTargetEnemy.SetActive(false);
        //pickTargetEnemy1.SetActive(false);
        //pickTargetEnemy2.SetActive(false);
        //pickTargetPlayer.SetActive(false);
        //pickTargetPlayer1.SetActive(false);
        //pickTargetPlayer2.SetActive(false);
        //hpBarPlayer.SetActive(false);
        //hpBarPlayer1.SetActive(false);
        //hpBarPlayer2.SetActive(false);
        //hpBarEnemy.SetActive(false);
        //hpBarEnemy1.SetActive(false);
        //hpBarEnemy2.SetActive(false);

        FindFightersInScene();
        //SetupUI();
    }

    public void AllyChosen(GameObject thisObject)
    {
        string CurrentAttacker = thisObject.name;

        //Disable itself when attacked
    }



    public void AttackChosen()
    {
        //Type of attack

    }
    public void TargetChosen()
    {
        //Target of attack

    }

    void ThisAttack()
    {
        if (AttackType == "Attack")
        {
            DoDamage(TargetChosen, currentAttacker.GetComponent<StatScript>().damage));
        }
        else if (attackType == "Defend")
        {
            target.GetComponent<AllyBattleScript>().IsBlocked = true;

        }
        else if (screech ... ){
            hornet.GEtCOmponent<Stats>().blockingMult = 1;
        }

        attackDone(CurrentAttacker);
        AllPlayersAttacked()
    }


    void AllPlayersAttacked()
    {
        if (attackedCount == activeAlly)
        {
            hornetAttack();
        }
    }

    void AttackDone(string name)
    {

    }
    void FindFightersInScene()
    {
        foreach (GameObject ally in AllyList)
        {
            if (ally == isActiveAndEnabled)
            {

                AllyBattleScript temp = ally.GetComponent<AllyBattleScript>();
                float nr = temp.GetHP();

                HealthBars.Add(ally.name, (1,nr));


                print(HealthBars[ally.name]);

                (int, float) johnson = HealthBars[ally.name];
                johnson.Item2 -= 10;

                HealthBars[ally.name] = johnson;
            }
        }
        foreach (GameObject enemy in EnemyList)
        {
            if (enemy == isActiveAndEnabled)
            {
                HealthBars.Add(enemy.name, (2,50));
            }
        }
    }


    void EnemyAttack(float Damage)
    {
        foreach (int i = 0; i < AmountOfEnemies; i++){


            int random = Random.Range(0, activeAlly);

            DoDamage(AllyList[random].name, EnemyList[i].GetComponent <StatList>().damage);
        }
    }
    public void DoDamage(string name, float damage)
    {
        if (HealthBars.ContainsKey(name))
        {
            (int, float) temp = HealthBars[name];

            if (HealthBars[name].Item1 == 1)
            {
                temp.Item2 -= damage * Blocking(name);

                HealthBars[name] = temp;

                if (HealthBars[name].Item2 <= 0)
                {

                    foreach (GameObject ally in AllyList)
                    {
                        if (ally.name == name)
                        {
                            ally.SetActive(false);
                            HealthBars.Remove(ally.name);
                            AmountOfAllies--;
                        }

                    }
                }
            }

            if (HealthBars[name].Item1 == 2)
            {
                temp.Item2 -= damage;

                HealthBars[name] = temp;
                if (HealthBars[name].Item2 <= 0)
                {

                    foreach (GameObject enemy in EnemyList)
                    {
                        if (enemy.name == name)
                        {
                            enemy.SetActive(false);
                            HealthBars.Remove(enemy.name);
                            AmountOfEnemies--;
                        }

                    }
                }
            }
        
            IsBattleFinished();
        }
        else
        {
            Debug.LogWarning("Target Does Not Exist!!");
        }
    }
    public float Blocking(string l)
    {
        float DmgMult = 0.50f;

        foreach (GameObject g in AllyList)
        {
            if (g.name == l)
            {
                bool isBlocked = g.GetComponent<AllyBattleScript>().IsBlocked;
                if (isBlocked)
                {
                    g.GetComponent<AllyBattleScript>().IsBlocked = false;
                    return DmgMult;
                   
                }
                else
                {
                    return 1;
                }
            }

        }

        return 1;
        
    }

    void IsBattleFinished()
    {
        if (AmountOfEnemies == 0 && AmountOfAllies > 0)
        {
            Debug.LogWarning("Allies Win!");
        }
        if (AmountOfEnemies > 0 && AmountOfAllies == 0)
        {
            Debug.LogWarning("Enemies win!");
        }
    }



    /*
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
    */
//}

