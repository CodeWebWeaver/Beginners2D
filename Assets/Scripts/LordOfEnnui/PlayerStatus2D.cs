using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerStatus2D : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 5, currentHealth = 4;
    [SerializeField]
    float contactForce = 10;


    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    float damageIframes = 60, sprintIframes = 30, flashesPerSecond = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleDashInvincibility() {
        StartCoroutine(Invincibility((int) sprintIframes, new[] { "EnemyAbility" }));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            rb.AddForce(collision.GetContact(0).normal * contactForce, ForceMode2D.Impulse);
            currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
            currentHealth--;
            if (currentHealth <= 0) {
                Debug.Log("ded");
                gameObject.SetActive(false);
                return;
            }
            StartCoroutine(Invincibility((int) damageIframes, new[] { "Enemy", "EnemyAbility" }));
        }
    }

    private IEnumerator Invincibility(int frames, string[] ignoreLayers, bool flash = true) {
        foreach (string layer in ignoreLayers) {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(layer), true);
        }

        float iTime = frames / 60f;
        int flashes = (int) (iTime * flashesPerSecond);
        float timePerFlash = iTime / (flashes * 2);

        for (int i = 0; i < flashes; i++) {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(timePerFlash);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(timePerFlash);
        }

        foreach (string layer in ignoreLayers) {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer(layer), false);
        }
    }
}
