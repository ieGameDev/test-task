using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Input
{
    public class StandaloneInput : InputService
    {
        public override Vector2 Axis => DescktopAxis();
        public override float RotateX => MouseRotate();

        private static float MouseRotate() => 
            UnityEngine.Input.GetAxis(MouseXInput);

        private static Vector2 DescktopAxis() =>
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}