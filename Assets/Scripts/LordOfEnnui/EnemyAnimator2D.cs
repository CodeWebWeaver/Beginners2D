public class EnemyAnimator2D : ASpriteAnimator2D
{
    AEnemyStrategy eStrat;

    protected override void Start() {
        base.Start();
        eStrat = (AEnemyStrategy) strat;
    }

    private void FixedUpdate() {
        ComputeAnimatorValues(eStrat.facingAngle, eStrat.speed);
        SetAnimatorValues();
    }
}
