using UnityEngine;

namespace ECS.Player.Components
{
    public struct PlayerInputComponent
    {
        public Vector3 MoveInput;
        public float RotateInput;
        public float StepAccumulator;
        public float RotationStepAccumulator;
        
        //public bool ShootInput;
    }
}