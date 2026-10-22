using UnityEngine;
using Fusion;
using System.Collections;
using System.Linq;

public class Heath : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] private Transform[] spawnPoints;

    [Networked] public int CurrentHealth { get; set; }
    [Networked] public NetworkBool IsDead { get; set; }

    private void Start()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>().Select(sp => sp.transform).ToArray();
    }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            CurrentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!Object.HasStateAuthority) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (IsDead) return;
        IsDead = true;

        if (Object.HasStateAuthority)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        Vector3 spawnPosition = GetSpawnPosition();
        Quaternion spawnRotation = Quaternion.identity;

        transform.position = spawnPosition;
        transform.rotation = spawnRotation;

        CurrentHealth = maxHealth;
        IsDead = false;

        gameObject.SetActive(true);
    }

    private Vector3 GetSpawnPosition()
    {
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            int index = Random.Range(0, spawnPoints.Length);
            return spawnPoints[index].position;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
