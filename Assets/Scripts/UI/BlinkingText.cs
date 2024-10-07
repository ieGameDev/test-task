using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BlinkingText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _loadingTest;

        private float _blinkDuration = 0.3f;

        private void Start() => 
            StartCoroutine(Blink());

        IEnumerator Blink()
        {
            while (true)
            {
                yield return StartCoroutine(FadeTextToFullAlpha());
                yield return StartCoroutine(FadeTextToZeroAlpha());
            }
        }

        IEnumerator FadeTextToFullAlpha()
        {
            float elapsedTime = 0f;
            Color color = _loadingTest.color;

            while (elapsedTime < _blinkDuration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(0, 1, elapsedTime / _blinkDuration);
               
                    _loadingTest.color = color;
                yield return null;
            }
        }

        IEnumerator FadeTextToZeroAlpha()
        {
            float elapsedTime = 0f;
            Color color = _loadingTest.color;

            while (elapsedTime < _blinkDuration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(1, 0, elapsedTime / _blinkDuration);
                    _loadingTest.color = color;
                yield return null;
            }
        }
    }
}
