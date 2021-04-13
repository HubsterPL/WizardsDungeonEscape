using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelCompletionHandler : MonoBehaviour
    {
        private static LevelCompletionHandler _instance;
        public static LevelCompletionHandler Instance {
            get {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LevelCompletionHandler>();
                    Debug.LogError("Warning! There is no LevelCompletionHandler Instance on the scene!");
                }
                return _instance;
            }
        }

        public Transform levelCompleteScreenParent;

        public void DisplayLevelComplete()
        {
            levelCompleteScreenParent.gameObject.SetActive(true);
            ScreenBlackoutHandler.Instance.HUD_FadeIn();
        }

        public void HideLevelComplete()
        {
            levelCompleteScreenParent.gameObject.SetActive(false);
            ScreenBlackoutHandler.Instance.HUD_FadeOut();
        }
    }
}