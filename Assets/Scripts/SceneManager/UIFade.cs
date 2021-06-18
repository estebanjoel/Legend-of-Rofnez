using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class UIFade : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        [SerializeField] GameObject mainCanvas;
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            // StartCoroutine(FadeInOut());
        }

        IEnumerator FadeInOut()
        {
            yield return FadeOut(1f);
            yield return FadeIn(2f);
        }

        public IEnumerator FadeOut(float time)
        {
            mainCanvas.SetActive(false);
            while(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while(canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
            mainCanvas.SetActive(true);
        }
    }
}
