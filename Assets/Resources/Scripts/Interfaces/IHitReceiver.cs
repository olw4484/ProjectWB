
using UnityEngine;

public interface IHitReceiver
{
    void TakeHit(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
