﻿using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Omniman : Bot
{
    bool peek;
    double moveAmount; 
    int turnDirection = 1;

    static void Main()
    {
        new Omniman().Start();
    }

    Omniman() : base(BotInfo.FromFile("Omniman.json")) { }

    public override void Run()
    {
        BodyColor = Color.White;
        TurretColor = Color.FromArgb(255, 209, 0, 0);            
        RadarColor = Color.FromArgb(255, 209, 0, 0);    
        BulletColor = Color.FromArgb(255, 209, 0, 0);       
        ScanColor = Color.Red;

        AdjustGunForBodyTurn = true;
        AdjustRadarForBodyTurn = true;
        AdjustRadarForGunTurn  = true;

        moveAmount = Math.Max(ArenaWidth, ArenaHeight);
        TurnRight(Direction % 90);
        Forward(moveAmount);
        TurnRight(90);
        while (IsRunning)
        {
            SetTurnRadarRight(360);
            SetForward(40);
            SetTurnRight(40);
            SetForward(20);
            Go();

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
        TurnToFaceTarget(e.X, e.Y);
        Fire(3);
        Forward(30);
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        TurnGunLeft(GunBearingTo(e.X, e.Y));
        var distance = DistanceTo(e.X, e.Y);

        if (distance < 100)
        {
            TurnToFaceTarget(e.X, e.Y);
            Forward(distance + 5);
        }
        else if (distance < 500)
        {
            Fire(3);
        }
        else
        {
            Fire(2);
        }
    }
}