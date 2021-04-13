using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ScreenBlackoutHandler : MonoBehaviour
    {
        private static ScreenBlackoutHandler _instance;
        public static ScreenBlackoutHandler Instance {
            get {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ScreenBlackoutHandler>();
                    if (_instance == null)
                        Debug.LogError("Warning! There is no ScreenBlackoutHandler" +
                        " Instance on the scene!");
                }
                return _instance;
            }
        }

        [SerializeField] Image hudBlackout;
        [SerializeField] Image fullBlackout;

        [SerializeField] float hudAlphaCeiling = .5f;
        [SerializeField] float fullAlphaCeiling = 1f;

        [SerializeField] float hudFadeTimescale;
        [SerializeField] float fullFadeTimescale;

        float hudTargetAlpha = 0f;
        float fullTargetAlpha = 0f;

        private void Update()
        {
            hudBlackout.color = new Color(0f, 0f, 0f, Mathf.Lerp(hudBlackout.color.a, hudTargetAlpha, Time.unscaledDeltaTime / hudFadeTimescale));
        }

        public void HUD_FadeIn()
        {
            hudTargetAlpha = hudAlphaCeiling;
        }

        public void HUD_FadeOut()
        {
            hudTargetAlpha = 0f;
        }
    }
}