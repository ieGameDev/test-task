using Assets.Scripts.Logic;

namespace Assets.Scripts.Player
{
    public class PlayerBullet : Bullet
    {
        protected override string TargetTag => "Enemy";
    }
}
