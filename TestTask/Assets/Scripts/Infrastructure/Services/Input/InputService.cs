using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string MouseXInput = "Mouse X";

        public abstract Vector2 Axis { get; }

        public abstract float RotateX { get; }
    }
}