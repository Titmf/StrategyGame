using ECS.Data;
using ECS.Systems;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Start
{
    public class CollisionCheckerView : MonoBehaviour
    {
        public EcsWorld EcsWorld { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            var hit = EcsWorld.NewEntity();

            var hitPool = EcsWorld.GetPool<HitComponent>();
            hitPool.Add(hit);
            ref var hitComponent = ref hitPool.Get(hit);

            hitComponent.First = transform.root.gameObject;
            hitComponent.Other = collision.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tags.ManaTag))
            {
                // instantly destroy coin to avoid multiple OnTriggerEnter() calls.
                other.gameObject.SetActive(false); 
            }
            var hit = EcsWorld.NewEntity();

            var hitPool = EcsWorld.GetPool<HitComponent>();
            hitPool.Add(hit);
            ref var hitComponent = ref hitPool.Get(hit);

            hitComponent.First = transform.root.gameObject;
            hitComponent.Other = other.gameObject;
        }
    }
}