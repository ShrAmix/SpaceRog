using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerShooting : NetworkBehaviour
{
    [SerializeField] private BulletGo _projectile;
    [SerializeField] private Transform Gun1;
    [SerializeField] private Transform Gun2;
    private float _lastFired = float.MinValue;
    private bool _fired;


    [ServerRpc]
    private void RequestFireServerRpc(Vector3 dir)
    {
        FireClientRpc(dir);
    }

    [ClientRpc]
    private void FireClientRpc(Vector3 dir)
    {
        if (!IsOwner) ExecuteShoot(dir);
    }

    private void ExecuteShoot(Vector3 dir)
    {
        var projectile = Instantiate(_projectile, Gun1.transform.position, Gun1.rotation);
        var projectile1 = Instantiate(_projectile, Gun2.transform.position, Gun2.rotation);
    }

   
    public void Gun()
    {
        if (!IsOwner) return;

        
            var dir = transform.forward;

            // Send off the request to be executed on all clients
            RequestFireServerRpc(dir);

            // Fire locally immediately
            ExecuteShoot(dir);
            
        
    }
    
   
}