using Assets.Scripts.Logic;

namespace Assets.Scripts.Enemy
{
    public class EnemyBullet : Bullet
    {
        protected override string TargetTag => "Player";
    }
}
