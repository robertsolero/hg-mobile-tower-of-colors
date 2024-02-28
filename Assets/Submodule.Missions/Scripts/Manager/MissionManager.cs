using System;
using System.Collections.Generic;
using Submodule.Missions;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private static MissionManager _instance;
    public static MissionManager Instance //TODO improve this a simplified version of a Singleton
    {
        get
        {
            if (_instance == null)
            {
                return _instance = Resources.Load<MissionManager>("MissionManager");
            }

            return _instance;
        }
    }

    [SerializeField] 
    private MissionDataHandler dataHandler;
    public MissionDataHandler DataHandler => dataHandler;
    
    
    [SerializeField] 
    private MissionUIHandler uiHandler;
    public MissionUIHandler UIHandler => uiHandler;

    
    [SerializeField] 
    private MissionLogicHandler logicHandler;
    public MissionLogicHandler LogicHandler => logicHandler;

    

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
