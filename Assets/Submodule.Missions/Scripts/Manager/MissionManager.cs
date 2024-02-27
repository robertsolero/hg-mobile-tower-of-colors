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
                var go = new GameObject("Factory");
                return _instance = go.AddComponent<MissionManager>();
            }

            return _instance;
        }
    }
}
