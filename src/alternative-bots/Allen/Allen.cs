using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Allen : Bot
{   
    /* A bot that drives forward and backward, and fires a bullet */
    static void Main(string[] args)
    {
        new Allen().Start();
    }

    Allen() : base(BotInfo.FromFile("Allen.json")) { }

    public override void Run()
    {
        /* Customize bot colors, read the documentation for more information */
        BodyColor = Color.FromArgb(255,  80, 200,  80);
        TurretColor = Color.White;
        RadarColor = Color.Orange;
        BulletColor = Color.Orange;
        ScanColor = Color.FromArgb(255, 200, 230, 200);

        AdjustGunForBodyTurn = true;
        AdjustRadarForBodyTurn = true;
        AdjustRadarForGunTurn  = true;
        while (IsRunning)
        {
            SetForward(10); 
            SetTurnRadarRight(360);
            SetTurnRight(10);
            Go();
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        TurnGunLeft(GunBearingTo(e.X, e.Y));
        Fire(2);
        // Console.WriteLine("I see a bot at " + e.X + ", " + e.Y);
    }

    public override void OnHitBot(HitBotEvent e)
    {
        TurnRight(90);
        // Console.WriteLine("Ouch! I hit a bot at " + e.X + ", " + e.Y);
    }

    public override void OnHitWall(HitWallEvent e)
    {
        TurnRight(180);
        // Console.WriteLine("Ouch! I hit a wall, must turn back!");
    }

    /* Read the documentation for more events and methods */
}
