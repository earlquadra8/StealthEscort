using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                _instance = FindObjectOfType<UIManager>();
                return _instance;
            }
        }
    }

    GameManager gameManager;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        gameManager = GameManager.Instance;
    }
    void Update()
    {

    }
}
