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
        BodyColor = Color.Yellow;
        TurretColor = Color.Blue;
        RadarColor = Color.Black;
        BulletColor = Color.Yellow;
        ScanColor = Color.Blue;
        
        movingForward = true;
        
        while (IsRunning)
        {
            SetTurnRight(5000);
            MaxSpeed = 5;
            Forward(10000);
        }
    }
    
    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double firePower = DetermineFirePower(distance);
        
        if (distance <= 50){
            Fire(firePower);
            TurnToFaceTarget(e.X, e.Y);
            Forward(distance+40);
        }
        else if (distance <= 100)
        {
            Fire(firePower);
        }
        else if (distance <= 500)
        {
            Fire(firePower);
        }
        else
        {
            Forward(distance - 500);
            Fire(firePower);
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
        // **Jika energi bot di atas 50, gunakan strategi normal**
        if (Energy > 40)
        {
            if (distance <= 50) return 5;
            if (distance <= 100) return 3;
            if (distance <= 500) return 2;
            return 0.4; // Jarak jauh, hemat energi
        }

        // **Jika energi bot mulai menipis, lebih hemat energi**
        if (Energy > 20)
        {
            if (distance <= 50) return 3;
            if (distance <= 100) return 2;
            if (distance <= 500) return 1;
            return 0.4; // Jarak jauh, sangat hemat energi
        }

        // **Jika energi sangat rendah, hanya tembak jika perlu**
        if (Energy > 10)
        {
            if (distance <= 50) return 2;
            if (distance <= 100) return 1;
            return 0.2; // Sangat hemat energi
        }

        // **Jika energi hampir habis, sangat defensif**
        return 1; // Tidak menembak untuk menghindari eliminasi cepat
    }
}
