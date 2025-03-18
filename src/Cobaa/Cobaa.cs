using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Cobaa : Bot
{
    bool peek;
    double moveAmount; 
    int turnDirection = 1;

    static void Main()
    {
        new Cobaa().Start();
    }

    Cobaa() : base(BotInfo.FromFile("Cobaa.json")) { }

    public override void Run()
    {
        BodyColor = Color.White;
        TurretColor = Color.White;
        RadarColor = Color.White;
        BulletColor = Color.Black;
        ScanColor = Color.White;

        moveAmount = Math.Max(ArenaWidth, ArenaHeight);
        peek = false;

        TurnRight(Direction % 90);
        Forward(moveAmount);

        peek = true;
        TurnGunRight(90);
        TurnRight(90);

        while (IsRunning)
        {
            // TurnLeft(5 * turnDirection);
            peek = true;
            // Forward(moveAmount);
            peek = false;
            // TurnRight(185);
            TurnGunRight(180);
        }
    }

    public override void OnHitBot(HitBotEvent e)
    { 
        // TurnToFaceTarget(e.X, e.Y);

        // if (e.Energy > 30)
        //     Fire(3);
        // else Fire(2);

        // Fire(3);

        // Forward(40); 
    }

    private void TurnToFaceTarget(double x, double y)
    {
        var bearing = BearingTo(x, y);
        if (bearing >= 0)
            turnDirection = 1;
        else
            turnDirection = -1;

        TurnGunLeft(bearing);
    }

    // We scanned another bot -> fire!
    public override void OnScannedBot(ScannedBotEvent e)
    {
        var distance = DistanceTo(e.X, e.Y);
        if (distance < 75){
            Fire(5);
            if (distance < 10){
                Forward(10);
                Back(10);
            }
        }else if (distance < 125){
            Fire(4);
        }else if (distance < 200){
            Fire(3);
        }else{
            Fire(2);
        }
        
        Rescan();
    }
}