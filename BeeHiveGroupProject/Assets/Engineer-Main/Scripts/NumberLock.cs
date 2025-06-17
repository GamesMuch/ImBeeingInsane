using TMPro;
using UnityEngine;

public class NumberLock : MonoBehaviour
{
    
    int currentNumber = 1;
    public TextMeshProUGUI text;
    public int SetNumber()
    {
        return currentNumber;
    }
    public void NextNumber()
    {
        if (currentNumber == 9)
        {
            currentNumber = 0;
        }
        else
        {
            currentNumber++;
        }
        UpdateNr();
    }
    
    public void PrevNumber()
    {
        if (currentNumber == 0)
        {
            currentNumber = 9;
        }
        else
        {
            currentNumber--;
        }
        UpdateNr();
    }
    void UpdateNr()
    {
        text.text = currentNumber.ToString();
    }
}
