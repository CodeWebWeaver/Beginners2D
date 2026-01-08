using UnityEngine;

public class OilPickup : MonoBehaviour
{
    public float amount = 20;

    private void Awake() {
        gameObject.layer = Layers.Pickup;
    }
}
