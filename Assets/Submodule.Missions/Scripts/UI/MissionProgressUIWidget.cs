using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Submodule.Missions
{
    public class MissionProgressUIWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI percentText;
        [SerializeField] TextMeshProUGUI currentValueText;
        [SerializeField] TextMeshProUGUI nextValueText;
        [SerializeField] TextMeshProUGUI nextValueTextFill;
        [SerializeField] Slider percentSlider;
        [SerializeField] Image fullPercentImage;
        [SerializeField] float smoothTime = 0.5f;
        [SerializeField] private Button button;
        [SerializeField] Coroutine _animateRoutine;


        bool IsMissionEnabled => RemoteConfig.BOOL_MISSION_ENABLED;
        
        public void Awake()
        {
            if (!IsMissionEnabled)
            {
                Destroy(gameObject);
                return;
            }
            
            RefreshActiveState();
            
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnWidgetPressed);
        }
        
        private void OnWidgetPressed()
        {
            var logicHandler = MissionManager.Instance.LogicHandler;
            if (!logicHandler.IsMissionInProgress)
                return;

            MissionManager.Instance.UIHandler.ShowMissionDetailsUI(logicHandler.CurrentProgressHandler.MissionData,
                logicHandler.CurrentProgressHandler.MissionConditionsAtDifficulty);
        }


        public void OnMissionStarted(MissionProgressHandler missionProgress)
        {
            RefreshActiveState(missionProgress);
        }

        public void OnMissionProgressChanged(MissionProgressHandler missionProgress)
        {
            if (missionProgress == null)
                return;
            
            var progressNormalized = missionProgress.CurrentProgress / (float)missionProgress.MissionRequirement;
            currentValueText.text = missionProgress.CurrentProgress.ToString("N0");
            SetValueSmooth(progressNormalized);
        }

        public void OnMissionCompleted()
        {
            currentValueText.text = nextValueText.text;
            percentSlider.normalizedValue = 1.0f;
            fullPercentImage.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        void RefreshActiveState(MissionProgressHandler missionProgress = null)
        {
            var activeState = IsMissionEnabled && MissionManager.Instance.LogicHandler.IsMissionInProgress;
            gameObject.SetActive(activeState);

            if (activeState)
            {
                missionProgress = missionProgress == null
                    ? MissionManager.Instance.LogicHandler.CurrentProgressHandler
                    : missionProgress;
                
                SetInitialState(missionProgress);
            }
        }

        void SetInitialState(MissionProgressHandler progressHandler)
        {
            titleText.text = progressHandler.MissionData.MissionName;
            SetValues(progressHandler.CurrentProgress, progressHandler.MissionRequirement);
        }


        public void SetValues(int current, int requirement)
        {
            currentValueText.text = current.ToString("N0");
            nextValueText.text = requirement.ToString("N0");
            nextValueTextFill.text = nextValueText.text;
            SetValue(current / (float)requirement);
        }

        public void SetValueSmooth(float value)
        {
            if (_animateRoutine != null)
                StopCoroutine(_animateRoutine);
            _animateRoutine = StartCoroutine(AnimateValue(value));
        }

        public void SetValue(float value)
        {
            //value = (float)System.Math.Truncate(value * 100) / 100;
            // fast patch to avoid reaching 100% prematurely as Unity doesn't likes doubles.
            if (value < 1 && value > .99f)
            {
                value = .99f;
            }

            percentSlider.normalizedValue = value;
            percentText.text = value.ToString("P0");
        }
        
        IEnumerator AnimateValue(float endValue)
        {
            float p = 0;
            float e = 0;
            float startValue = percentSlider.normalizedValue;
            float value = percentSlider.normalizedValue;
            while (p < 1)
            {
                p = e / (smoothTime * Time.timeScale);
                value = Mathf.Clamp01(Mathf.Lerp(startValue, endValue, p));
                SetValue(value);
                yield return null;
                e += Time.deltaTime;
            }

            if (value >= 1)
            {
                fullPercentImage.gameObject.SetActive(true);
            }
            else if (fullPercentImage.gameObject.activeSelf)
            {
                fullPercentImage.gameObject.SetActive(false);
            }

            _animateRoutine = null;
        }
    }
}