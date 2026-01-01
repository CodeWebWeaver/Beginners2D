using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Scriptable Object/Player State")]
public class PlayerState : ScriptableObject {
    [Header("Status")]
    public float maxHealth = 5, currentHealth = 4;
    public float maxOil = 100, currentOil = 0;
    public float requiredOil = 80;
}
