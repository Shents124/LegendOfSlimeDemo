using UnityEngine;

public class ProjectileLauncher
{
    private const float H = 0.5f;
    private const float Gravity = -9.81f;
    
    public static void Launch(Rigidbody2D projectile, GameObject launchPosition, GameObject targetPosition)
    {
        projectile.velocity = CalculateLaunchData(launchPosition, targetPosition).initialVelocity;
    }
    
    private static LaunchData CalculateLaunchData(GameObject launchPosition, GameObject targetPosition)
    {
        var targetPos = targetPosition.transform.position;
        var launchPos = launchPosition.transform.position;
        float displacementY = targetPos.y - launchPos.y;
        float displacementX = targetPos.x - launchPos.x;
        float time = Mathf.Sqrt(-2 * H / Gravity) + Mathf.Sqrt(2 * (displacementY - H) / Gravity);
        Vector2 velocityY = Vector2.up * Mathf.Sqrt(-2 * Gravity * H);
        Vector2 velocityX = Vector2.right * displacementX / time;

        return new LaunchData(velocityX + velocityY * -Mathf.Sign(Gravity), time);
    }

    struct LaunchData
    {
        public readonly Vector2 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector2 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
