using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Systems
{
    public class GroundCheckerView : MonoBehaviour
    {
        public EcsPool<GroundedComponent> GroundedPool;
        public int PlayerEntity;

        private void OnTriggerStay(Collider other)
        {
            if (!other.gameObject.CompareTag("Ground")) return;

            if (!GroundedPool.Has(PlayerEntity))
            {
                GroundedPool.Add(PlayerEntity);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Ground")) return;

            if (GroundedPool.Has(PlayerEntity))
            {
                GroundedPool.Del(PlayerEntity);
                
            }
        }
    }
}