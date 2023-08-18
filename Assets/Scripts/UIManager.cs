using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform subtitleTextParent;
    [SerializeField] RectTransform subtitleActivePos, subtitleInactivePos;
    
    [SerializeField] TMP_Text subtitleText;    
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button startButton, restartButton;

    [SerializeField] RectTransform inputTransform;
    [SerializeField] RectTransform activePosition, inactivePosition;
    [SerializeField] float inputMoveSpeed = 5f;


    bool interactable = true;

    bool Interactable {
        get => interactable;
        set {
            interactable = value;
            inputField.interactable = interactable;
            startButton.interactable = interactable;
        }
    }

    public event Action<int> OnDuckAmountSet;

    public void SetSubtitle(string subtitle) {
        subtitleText.text = subtitle;
    }

    void StartGame() {

        if (int.TryParse(inputField.text, out int inputInt) && inputInt > 0) {
            Interactable = false;
            OnDuckAmountSet?.Invoke(inputInt);
        }
        else {
            inputField.text = "";
        }
    }

    void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update() {

        if (!Interactable) {

            if(Vector3.Distance(inactivePosition.position, inputTransform.position) > 1f) {

                Vector3 dir = (inactivePosition.position - inputTransform.position).normalized;
                inputTransform.position += (dir * inputMoveSpeed * Time.deltaTime);

            }

            if (Vector3.Distance(subtitleActivePos.position, subtitleTextParent.position) > 1f) {

                Vector3 dir = (subtitleActivePos.position - subtitleTextParent.position).normalized;
                subtitleTextParent.position += (dir * inputMoveSpeed * Time.deltaTime);
            }

        }
        else {
            if (Vector3.Distance(activePosition.position, inputTransform.position) > 1f) {

                Vector3 dir = (activePosition.position - inputTransform.position).normalized;
                inputTransform.position += (dir * inputMoveSpeed * Time.deltaTime);

            }

            if (Vector3.Distance(subtitleInactivePos.position, subtitleTextParent.position) > 1f) {

                Vector3 dir = (subtitleInactivePos.position - subtitleTextParent.position).normalized;
                subtitleTextParent.position += (dir * inputMoveSpeed * Time.deltaTime);
            }
        }        
    }

    private void OnEnable() {        
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable() {        
        startButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
    }

}
