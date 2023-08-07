using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class Cannon : MonoBehaviour
{
    public GameObject projectilePrefab; // prefabrykat pocisku
    public float fireRate = 1f; // czas miêdzy strza³ami
    public float projectileSpeed = 60f; // prêdkoœæ pocisku
    public float projectileLifetime = 2f; // czas ¿ycia pocisku

    private Transform target; // cel do œledzenia
    private float nextFireTime = 3; // czas nastêpnego strza³u
    private bool isPlayerNear = false;
    public float rotationSpeed = 1f;
    public float lerpSpeed = 10f;

    void Start()
    {
        nextFireTime += Time.time;
        target = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza
    }

    void Update()
    {
        if (isPlayerNear)
        {
            Vector3 directionToTarget = target.position - transform.position;
            float distanceToTarget = directionToTarget.magnitude;

            float firingAngle = Mathf.Atan2(directionToTarget.y, distanceToTarget) * Mathf.Rad2Deg;

            // Quaternion targetRotation = Quaternion.Euler(firingAngle, transform.rotation.eulerAngles.y, 0);
            // transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);

            if (Time.time > nextFireTime)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
                projectileRigidbody.velocity = directionToTarget.normalized * projectileSpeed;

                float timeToTarget = distanceToTarget / projectileSpeed;
                projectileRigidbody.AddForce(Vector3.up * 9.81f * timeToTarget / 2f, ForceMode.Impulse);

                Destroy(projectile, projectileLifetime);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}