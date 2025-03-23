using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Invincible : Bot
{
    bool movingForward;
    int turnDirection = 1;
    static void Main()
    {
        new Invincible().Start();
    }
    
    Invincible() : base(BotInfo.FromFile("Invincible.json")) { }
    
    public override void Run()
    {
        BodyColor = Color.Black;
        TurretColor = Color.DarkBlue;
        RadarColor = Color.Blue;
        BulletColor = Color.Blue;
        ScanColor = Color.LightBlue;
        
        movingForward = true;
        
        while (IsRunning)
        {
            if (IsNearWall(20))
            {
                AvoidWall();
            }
            else
            {
                SetTurnRight(5000);
                MaxSpeed = 5;
                Forward(10000);
            }
        }
    }
    
    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double firePower = DetermineFirePower(distance);
        
        if (distance <= 50){
            Fire(firePower);
            TurnToFaceTarget(e.X, e.Y);
            Fire(firePower);
            Forward(distance+5);
        }
        else if (distance <= 100)
        {
            Fire(firePower);
            TurnToFaceTarget(e.X, e.Y);
            Forward(distance*0.5);
        }
        else if (distance <= 800)
        {
            Fire(firePower);
            TurnToFaceTarget(e.X, e.Y);
            Forward(distance*0.3);
        }
        else
        {
            Fire(firePower);
            TurnToFaceTarget(e.X, e.Y);
            Fire(firePower);
            Forward(distance*0.2);
        }
    }
    
    public override void OnHitWall(HitWallEvent e)
    {
        ReverseDirection();
    }
    public override void OnHitBot(HitBotEvent e)
    {
        var bearing = BearingTo(e.X, e.Y);
        if (bearing > -10 && bearing < 10)
        {
            Fire(3);
        }
        if (e.IsRammed)
        {
            TurnLeft(10);
            ReverseDirection();
        }
    }
    
    private void ReverseDirection()
    {
        if (movingForward)
        {
            SetBack(200);
            movingForward = false;
        }
        else
        {
            SetForward(200);
            movingForward = true;
        }
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
    private double DetermineFirePower(double distance)
    {
        if (Energy > 60)
        {
            if (distance <= 500) return 3;
            return 2;
        }

        if (Energy > 20)
        {
            if (distance <= 50) return 3;
            if (distance <= 100) return 2;
            return 1;
        }

        if (Energy > 10)
        {
            if (distance <= 50) return 2;
            return 1;
        }

        return 0.5;
    }
    private bool IsNearWall(double margin)
    {
        double arenaWidth = ArenaWidth;
        double arenaHeight = ArenaHeight;
        if (X <= margin || X >= (arenaWidth - margin)) return true;
        if (Y <= margin || Y >= (arenaHeight - margin)) return true;
        return false;
    }
    private void AvoidWall()
    {
        TurnRight(90);
        Back(100);
    }
}
