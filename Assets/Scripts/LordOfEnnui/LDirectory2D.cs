using UnityEngine;

[DefaultExecutionOrder(-100)]
public class LDirectory2D : MonoBehaviour {
    public static LDirectory2D Instance;
    [SerializeField]
    public PlayerState defaultPlayerState;
    [HideInInspector]
    public PlayerState pState;

    public GameObject player;
    public PlayerController2D playerController;
    public GameObject pCamera;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(Instance);
        }
        if (defaultPlayerState == null) defaultPlayerState = ScriptableObject.CreateInstance<PlayerState>();
        pState = Instantiate(defaultPlayerState);
    }
}
