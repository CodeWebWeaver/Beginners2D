using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class ACharacterStatus2D : MonoBehaviour {

    [SerializeField]
    protected float contactKnockbackForce = 10;

    [SerializeField]
    protected Rigidbody2D rb;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (OnCollsionIsDamaged(collision.gameObject)) {
            rb.AddForce(collision.GetContact(0).normal * contactKnockbackForce, ForceMode2D.Impulse);
            OnDamageTaken();
        }
    }

    protected abstract bool OnCollsionIsDamaged(GameObject other);

    protected abstract void OnDamageTaken();
}
