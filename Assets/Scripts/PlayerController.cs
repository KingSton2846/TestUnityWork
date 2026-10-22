using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private NetworkPrefabRef bulletPrefab;
    [SerializeField] private Transform shootPoint;

    private const float MoveSpeed = 5f;

    private NetworkCharacterController _cc;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.Direction.Normalize();
            _cc.Move(MoveSpeed * data.Direction * Runner.DeltaTime);

            if (data.IsShootPressed) 
            {
                RPC_Shoot();
            }
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_Shoot()
    {
        Runner.Spawn(bulletPrefab, shootPoint.position, shootPoint.rotation, Object.InputAuthority);
    }
}