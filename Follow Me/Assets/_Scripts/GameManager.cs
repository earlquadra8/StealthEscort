using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                _instance = FindObjectOfType<GameManager>();
                return _instance;
            }
        }
    }

    UIManager uimanager;

    private void Awake()
    {
        _instance = this;
    }
    void Start ()
    {
        uimanager = UIManager.Instance;
	}
	void Update ()
    {
		
	}
}
