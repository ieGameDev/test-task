using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        float RotateX { get; }
        bool AttackButtonPressed { get; }
    }
}