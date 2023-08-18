using UnityEngine;

public class PlayerController : MonoBehaviour{

    [Header("Controls")]
    [SerializeField] KeyCode changeVerseKey = KeyCode.Space;    

    bool isConfigured = false;
    int startingDuckAmount = -1;
    int currentDuckAmount = -1;
    int currentVerseLine = 0;

    DuckManager duckManager;
    UIManager uiManager;


    private void UiManager_OnDuckAmountSet(int amount) {
        isConfigured = true;
        startingDuckAmount = amount;
        currentDuckAmount = amount;
        duckManager.SetDucksAmount(amount);
    }

    #region UnityMethods

    private void Start() {
        duckManager = FindObjectOfType<DuckManager>();
        uiManager = FindObjectOfType<UIManager>();
        uiManager.SetSubtitle("Pressione <b>ESPAÇO</b> para avançar os versos!");
        uiManager.OnDuckAmountSet += UiManager_OnDuckAmountSet;
    }
    

    private void Update() {

        if (Input.GetKeyDown(changeVerseKey) && isConfigured) {

            if (currentDuckAmount > 0) {

                if(currentVerseLine == 1) {

                    duckManager.SetDucksState(DuckState.Playing, currentDuckAmount);                    
                }
                else if (currentVerseLine == 2) {
                    duckManager.CallDuckMom();
                }
                else if (currentVerseLine == 3) {

                    duckManager.SetDucksState(DuckState.Patrol, currentDuckAmount - 1);
                    currentDuckAmount -= 1;
                }

                uiManager.SetSubtitle(LyricsMaker.MakeLine(currentDuckAmount, currentVerseLine));

                currentVerseLine = currentVerseLine > 2 ? 0 : currentVerseLine + 1;
            }
            else {

                duckManager.CallDuckMom();

                if (currentVerseLine == 3) {
                    currentDuckAmount = startingDuckAmount;
                    duckManager.SetDucksState(DuckState.Patrol, currentDuckAmount);
                    isConfigured = false;
                }

                uiManager.SetSubtitle(LyricsMaker.MakeLastVerse(startingDuckAmount, currentVerseLine));
                currentVerseLine = currentVerseLine > 2 ? 0 : currentVerseLine + 1;
            }            
        }
    }    

    private void OnDisable() {
        if (uiManager != null)
            uiManager.OnDuckAmountSet -= UiManager_OnDuckAmountSet;
    } 

    #endregion
}
