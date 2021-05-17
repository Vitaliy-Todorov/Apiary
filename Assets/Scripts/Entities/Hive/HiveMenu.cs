using UnityEngine;
using UnityEngine.UI;

public class HiveMenu : MonoBehaviour
{
    [SerializeField]
    Slider honey;
    [SerializeField]
    Text bees;

    int maxBees;

    public void MaxValue(float maxHoney, int maxBees)
    {
        honey.maxValue = maxHoney;
        this.maxBees = maxBees;
    }

    public void SetHoney(float health)
    {
        honey.value = health;
    }

    public void SetBees(int bee)
    {
        bees.text = bee + "/" + maxBees;
    }
}
