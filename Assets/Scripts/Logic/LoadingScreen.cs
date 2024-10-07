using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _screen;

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _screen.alpha = 1f;
        }

        public void Hide() =>
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (_screen.alpha > 0)
            {
                _screen.alpha -= 0.5f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}
