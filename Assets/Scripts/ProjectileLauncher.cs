using UnityEngine;

public class ProjectileLauncher
{
    [SerializeField] private static float hight = 0.7f;
    [SerializeField] private static float gravity = -9.81f;


    public static void Launch(Rigidbody2D projectile, GameObject launchPosition, GameObject targetPosition)
    {
        projectile.velocity = CalculateLaunchData(launchPosition, targetPosition).initialVelocity;
    }

    // void DrawPath()
    // {
    //     LaunchData launchData = CalculateLaunchData();
    //     Vector3 previousDrawPoint = transform.position;

    //     int resolution = 30;
    //     for (int i = 1; i <= resolution; i++)
    //     {
    //         float simulationTime = i / (float)resolution * launchData.timeToTarget;
    //         Vector2 displacement = launchData.initialVelocity * simulationTime + Vector2.up * gravity * simulationTime * simulationTime / 2f;
    //         Vector2 drawPoint = (Vector2)transform.position + displacement;
    //         Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
    //         previousDrawPoint = drawPoint;
    //     }
    // }

    private static LaunchData CalculateLaunchData(GameObject launchPositon, GameObject targetPosition)
    {
        var targetPos = targetPosition.transform.position;
        var launchPos = launchPositon.transform.position;
        float displacementY = targetPos.y - launchPos.y;
        float displacementX = targetPos.x - launchPos.x;
        float time = Mathf.Sqrt(-2 * hight / gravity) + Mathf.Sqrt(2 * (displacementY - hight) / gravity);
        Vector2 velocityY = Vector2.up * Mathf.Sqrt(-2 * gravity * hight);
        Vector2 velocityX = Vector2.right * displacementX / time;

        return new LaunchData(velocityX + velocityY * -Mathf.Sign(gravity), time);
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
