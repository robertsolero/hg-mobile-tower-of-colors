using System;
using UnityEngine;
using UnityEngine.UI;

namespace Submodule.Missions{
    
    public abstract class MissionUIPopupBase : MonoBehaviour
    {
        [SerializeField]
        private Button closeButton;

        private void Awake()
        {
            if (closeButton != null)
            {
                closeButton.onClick.RemoveAllListeners();
                closeButton.onClick.AddListener(Close);
            }
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            Destroy(gameObject);
        }
    }
}