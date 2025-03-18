using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Coba2 : Bot
{
    bool peek;
    bool isRamMode = false;
    double moveAmount; 
    int turnDirection = 1;

    static void Main()
    {
        new Coba2().Start();
    }

    Coba2() : base(BotInfo.FromFile("Coba2.json")) { }

    public override void Run()
    {
        BodyColor = Color.Orange;
        TurretColor = Color.Orange;
        RadarColor = Color.Orange;
        BulletColor = Color.Orange;
        ScanColor = Color.Orange;

        moveAmount = Math.Max(ArenaWidth, ArenaHeight);
        peek = false;

        TurnRight(Direction % 90);
        Forward(moveAmount);
        TurnRight(90);

        while (IsRunning)
        {
            if (!isRamMode) {
                Forward(40);
                peek = true;
                TurnRight(180);
                Forward(40);
                peek = false;
                TurnGunRight(360);
            }
            else {
                TurnLeft(5 * turnDirection);
            }
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

    public override void OnHitBot(HitBotEvent e)
    { 
        isRamMode = true;
        TurnToFaceTarget(e.X, e.Y);

        Fire(5);

        Forward(40);
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        var distance = DistanceTo(e.X, e.Y);
        
        if (distance < 20) {
            isRamMode = true;
            
            TurnToFaceTarget(e.X, e.Y);
            TurnGunLeft(GunBearingTo(e.X, e.Y));
            Forward(distance + 5);
        }
        else if (distance < 100) {
            isRamMode = false;
            Fire(3);
        }
        else {
            isRamMode = false;
            Fire(2);
        }
        Rescan();
    }
}