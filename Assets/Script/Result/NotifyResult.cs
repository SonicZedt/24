using System.Collections;
using UnityEngine;
using TMPro;

public class NotifyResult : MonoBehaviour
{
    [SerializeField] private GameHandler gameHandler;

    [Header("Notification")]
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private float fadingSpeed;

    void Start() {
        HideNotification();
    }

    void Update() {
        if(!gameObject.activeSelf) return;

        if(resultText.alpha == 1) {
            Invoke(nameof(FadeOut), 3f);
        }
    }

    private void FadeOut() {
        IEnumerator StartFadingOut() {
            resultText.alpha -= fadingSpeed * Time.deltaTime;

            yield return null;
        }

        if(resultText.alpha <= 0) {
            HideNotification();
            return;
        }

        StartCoroutine(StartFadingOut());
    }

    private void HideNotification() {
        gameObject.SetActive(false);
    }

    public void ShowNotification(string result) {
        resultText.text = "Your Answer Result Is: " + result;
        resultText.alpha = 1;

        gameObject.SetActive(true);
    }
}