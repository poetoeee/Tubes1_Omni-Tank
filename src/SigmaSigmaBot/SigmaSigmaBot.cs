using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class SigmaSigmaBot : Bot
{   
    /* A bot that drives forward and backward, and fires a bullet */
    static void Main(string[] args)
    {
        new SigmaSigmaBot().Start();
    }

    SigmaSigmaBot() : base(BotInfo.FromFile("SigmaSigmaBot.json")) { }

    public override void Run()
    {
        /* Customize bot colors, read the documentation for more information */
        BodyColor = Color.Gray;
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
