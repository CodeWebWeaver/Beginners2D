using System.Collections;
using UnityEngine;

public class ProjectileShooter2D : MonoBehaviour
{
    [SerializeField]
    GameObject target, bulletObject;

    [SerializeField]
    ABullet2D bullet;

    [SerializeField]
    float placeDistance = 0.5f;

    [SerializeField]
    bool fire = false;
    
    void Start()
    {
        target = LDirectory2D.Instance.player;
        bullet = bulletObject.GetComponent<ABullet2D>();
        StartCoroutine(FireBullets());
    }

    private IEnumerator FireBullets() {
        WaitForSeconds wait = new WaitForSeconds(1 / bullet.fireRate);

        while (true) {
            Vector3 targetDirection = (target.transform.position - transform.position).normalized;
            Vector3 placePosition = transform.position + targetDirection * placeDistance;
            GameObject bo = Instantiate(bulletObject, placePosition, Quaternion.identity);
            bo.GetComponent<Rigidbody2D>().AddForce(targetDirection * bullet.shootForce);
            yield return wait;
        }
    }
}
