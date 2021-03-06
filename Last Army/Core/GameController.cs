﻿using System;
using System.Linq;

public class GameController
{
	private IArmy army;
	private IWareHouse wareHouse;
    private MissionController missionController;
	private IMissionFactory missionFactory;
	private ISoldierFactory soldierFactory;
	private IWriter writer;

    public GameController(IWriter writer)
    {
		this.army = new Army();
		this.wareHouse = new WareHouse();
		this.missionController = new MissionController(this.army,this.wareHouse);
		this.missionFactory = new MissionFactory();
		this.soldierFactory = new SoldierFactory();
		this.writer = writer;
	}
	
    public void GiveInputToGameController(string input)
    {
        var data = input.Split();

        if (data[0].Equals("Soldier"))
        {
            if(data[1]=="Regenerate")
			{
				army.RegenerateTeam(data[2]);
			}
			else
			{
				var soldier = this.soldierFactory.CreateSoldier(data[1], data[2], int.Parse(data[3])
					,double.Parse(data[4]), double.Parse(data[5]));

				if (wareHouse.TryEquipSoldier(soldier))
					army.AddSoldier(soldier);
				else
					throw new ArgumentException(string.Format(OutputMessages.NotEnoughAmmunitions,data[1],data[2]));
				
			}
			
        }
        else if (data[0].Equals("WareHouse"))
        {
            string name = data[1];
            int number = int.Parse(data[2]);
			this.wareHouse.AddAmmunition(name, number);
        }
        else if (data[0].Equals("Mission"))
        {
			var mission = this.missionFactory
				.CreateMission(data[1], double.Parse(data[2]));

			writer.AppendLine(this.missionController
				.PerformMission(mission).Trim());
        }
    }

    public void RequestResult()
    {
		missionController.FailMissionsOnHold();
		writer.AppendLine(OutputMessages.Results);
		writer.AppendLine(string.Format(OutputMessages.SuccessfullMissionsCount, missionController.SuccessMissionCounter));
		writer.AppendLine(string.Format(OutputMessages.FailedMissionsCount, missionController.FailedMissionCounter));
		writer.AppendLine(OutputMessages.Soldiers);
		foreach (var soldier in this.army.Soldiers.OrderByDescending(s => s.OverallSkill))
		{
			writer.AppendLine(soldier.ToString());
		}
	}

        

			
}
