using UnityEngine;

public class GameManager2D : MonoBehaviour
{
    [SerializeField]
    Trigger2D levelExitTrigger;

    [SerializeField]
    PlayerState pState;

    void Start()
    {
        if (levelExitTrigger != null) levelExitTrigger.triggerEvent.AddListener(OnLevelComplete);
        pState = LDirectory2D.Instance.pState;
    }

    void Update()
    {
        if (levelExitTrigger != null) levelExitTrigger.SetActive(pState.currentOil >= pState.requiredOil);
        if (pState.currentHealth <= 0) {
            OnDeath();
        }
    }

    public void OnLevelComplete(GameObject player, bool entered) {
        if (entered) Debug.Log("Pog");
    }

    public void OnDeath() {
        Debug.Log("Ded");
    }
}
