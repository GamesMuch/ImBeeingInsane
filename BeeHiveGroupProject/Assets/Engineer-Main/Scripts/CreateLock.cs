using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public struct CodeInfo
{
    public int first;
    public int second;
    public int third;
}

public struct CurrentCode
{
    public int first;
    public int second;
    public int third;
}

public class CreateLock : MonoBehaviour
{
    CurrentCode currentCode = new CurrentCode();
    public CodeInfo Code;

    public List<NumberLock> CodeSlots;

    void Awake()
    {
        CodeSlots = new List<NumberLock>(GetComponentsInChildren<NumberLock>());
    }
    void GetCode() {
        currentCode.first = CodeSlots[0].SetNumber();
        currentCode.second = CodeSlots[1].SetNumber();
        currentCode.third = CodeSlots[2].SetNumber();
    }
    public void CheckCode()
    {
        GetCode();
        if (currentCode.first == Code.first && currentCode.second == Code.second && currentCode.third == Code.third)
        {
            Debug.LogWarning("We did it, do something");
        }
        else
        {
            Debug.LogWarning("It also works, but you fumbled it severly you bollock");
        }
    }
}
