using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private Animator crossfade;
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            StartLoading();
        }
    }
    public void StartLoading()
    {
        crossfade.SetTrigger("End");
        StartCoroutine(EndLoading());
    }

    IEnumerator EndLoading()
    {
        yield return new WaitForSeconds(1f);
        crossfade.SetTrigger("Start");
    }
}
