using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability_Switcher_UI : MonoBehaviour
{
    [SerializeField] 
    Button previousButton;
    [SerializeField] 
    Button nextButton;
    public int currentAbility;

    private void Awake()
    {
        SelectAbility(0);
    }
    private void SelectAbility(int _index)
    {
        previousButton.interactable = (_index != 0);
        nextButton.interactable = (_index != transform.childCount - 1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }

    public void ChangeAbility(int _change)
    {
        currentAbility += _change;
        SelectAbility(currentAbility);
    }
}
