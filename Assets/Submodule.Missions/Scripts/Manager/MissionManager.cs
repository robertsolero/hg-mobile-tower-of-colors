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
                if (!RemoteConfig.BOOL_MISSION_ENABLED)
                {
                    Debug.LogError($"Cannot access Mission Manager, RC BOOL_MISSION_ENABLED is {RemoteConfig.BOOL_MISSION_ENABLED}");
                    return null;
                }


                return _instance = FindObjectOfType<MissionManager>();
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
    public MissionLogicHandler LogicHandler => logicHandler == null ? logicHandler = new MissionLogicHandler() : logicHandler;

    

    private void Awake()
    {
        if (!RemoteConfig.BOOL_MISSION_ENABLED)
        {
            Destroy(gameObject);
            return;
        }
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        UIHandler.Initialize();
    }
}
