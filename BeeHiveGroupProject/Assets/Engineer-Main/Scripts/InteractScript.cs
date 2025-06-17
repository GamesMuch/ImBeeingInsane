using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public enum plant { none,FlowerPuzzle,FlowerLock};
    public plant ChoosePlant = plant.none;

    public OpenUI UIManager;

    public GameObject pressButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pressButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pressButton.SetActive(false);
        }
    }

    public void Interaction()
    {
        UIManager.OpenUi(ChoosePlant.ToString());
        Debug.Log(ChoosePlant);
    }
}
