using UnityEngine;

public class WrenchPickup : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            //ModuleRewardManager.Instance.OpenModuleSelection();
            //This will open the UI with the selection of Modules, commented
            //out right now till everything is working
            Debug.Log("Collected");
            Destroy(gameObject);
        }
    }
}
