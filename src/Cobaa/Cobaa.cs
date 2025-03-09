using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Cobaa : Bot
{
    // bool isAttacking = false;
    // int lastScannedTurn = 0;
    // double moveDirection = 1;
    // bool reachedWall = false;
    int turnDirection = 1;
    int dist = 50;

    static void Main(string[] args)
    {
        new Cobaa().Start();
    }
    Cobaa() : base(BotInfo.FromFile("Cobaa.json")) { }

    public override void Run()
    {
        // Set colors
        BodyColor = Color.FromArgb(255, 255, 255);  
        TurretColor = Color.FromArgb(255, 255, 255); 
        RadarColor = Color.FromArgb(255, 255, 255); 

        while (IsRunning)
        {
            TurnLeft(5 * turnDirection);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        TurnToFaceTarget(e.X, e.Y);
        var distance = DistanceTo(e.X, e.Y);
        if (distance < 100){
            Forward(distance + 5);
        }else{
            Fire(1);
        }

        Rescan(); 
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        TurnLeft(NormalizeRelativeAngle(90 - (Direction - e.Bullet.Direction)));

        Forward(dist);
        dist *= -1; 

        Rescan();
    }

    public override void OnHitBot(HitBotEvent e)
    {
        TurnToFaceTarget(e.X, e.Y);

        if (e.Energy > 16)
            Fire(3);
        else if (e.Energy > 10)
            Fire(2);
        else if (e.Energy > 4)
            Fire(1);
        else if (e.Energy > 2)
            Fire(.5);
        else if (e.Energy > .4)
            Fire(.1);

        Forward(40); 
    }

    private void TurnToFaceTarget(double x, double y)
    {
        var bearing = BearingTo(x, y);
        if (bearing >= 0)
            turnDirection = 1;
        else
            turnDirection = -1;

        TurnLeft(bearing);
    }
}