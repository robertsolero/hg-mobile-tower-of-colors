using UnityEngine;

namespace Submodule.Missions{
    
    public abstract class MissionUIPopup : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}