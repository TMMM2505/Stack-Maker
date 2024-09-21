using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congrats : MonoBehaviour
{
    [SerializeField] private GameObject congratsVFX;
    
    public void OnInit()
    {
        congratsVFX.SetActive(false);
    }
    public void TurnOnVFX()
    {
        congratsVFX.SetActive(true);
    }
}
