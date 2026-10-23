using UnityEngine;
using Fusion;
public class Bullet : NetworkBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f;
    [Networked] private TickTimer lifeTimer { get; set; }

    private Rigidbody _rb;

    public override void Spawned()
    {
        _rb = GetComponent<Rigidbody>();

        _rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        
        // Добавляем проверку на null
        if (Object != null && Object.HasStateAuthority) 
        {
            lifeTimer = TickTimer.CreateFromSeconds(Runner, lifeTime);
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            if (lifeTimer.Expired(Runner))
            {
                Runner.Despawn(Object);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Object.HasStateAuthority) return;

        Heath targetHealth = other.GetComponent<Heath>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(10);
        }
        Runner.Despawn(Object);
    }
}
