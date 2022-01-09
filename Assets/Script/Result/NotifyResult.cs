using System.Collections;
using UnityEngine;
using TMPro;

public class NotifyResult : MonoBehaviour
{
    [SerializeField] private GameHandler gameHandler;

    [Header("Notification")]
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private float displayDuration;
    [SerializeField] private float fadingSpeed;
    [SerializeField] private float fadingDelay;
    private bool fadingOut;

    void Start() {
        HideNotification();
    }

    void Update() {
        if(!gameObject.activeSelf) return;

        if(!fadingOut) {
            fadingOut = true;

            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut() {
        yield return new WaitForSeconds(displayDuration);

        while(notificationText.alpha >= 0) {
            notificationText.alpha -= fadingSpeed * Time.deltaTime;
    
            yield return new WaitForSeconds(fadingDelay);
        }

        HideNotification();
        yield return null;
    }

    private void HideNotification() {
        gameObject.SetActive(false);
    }

    public void ShowNotification(string result) {
        fadingOut = false;
        notificationText.alpha = 1;
        notificationText.text = "Your Answer Result Is: " + string.Format("{0:0.##}", float.Parse(result));

        gameObject.SetActive(true);
    }
}