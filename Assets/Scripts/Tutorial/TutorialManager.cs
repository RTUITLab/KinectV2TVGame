using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] bool _showTutorial = true;
    [SerializeField] int _timeBeetweenHints = 4;
    [SerializeField] GameObject[] _hints;
    [SerializeField] GameObject _backgroundBox;

    void Start()
    {
        StartCoroutine(ShotHints());
    }
    IEnumerator ShotHints()
    {
        _backgroundBox.SetActive(true);
        foreach (GameObject hint in _hints)
        {
            hint.SetActive(true);
            yield return new WaitForSeconds(_timeBeetweenHints);
            hint.SetActive(false);
        }
        _backgroundBox.SetActive(false);
    }

}
