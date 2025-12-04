using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "QuestData", menuName = "Datas/QuestData")]
public class QuestData : ScriptableObject {
	public Sprite Icon;
	public QuestName Name;
	public bool IsAdd;
	public List<QuestInfo> Info = new List<QuestInfo>();
	public Action OnProgressChanged;

    public virtual void UpdateProgress() {

	}

	#region Properties

	public string Key {
		get { return string.Format("Achievement_{0}", Name); }
	}

	public QuestProgress QuestProgress {
		get {
			QuestProgress quest = IPlayerPrefs.Get<QuestProgress>(Key);
			if (quest == null) {
				quest = new QuestProgress();
				IPlayerPrefs.Set(Key, quest);
			}

			return quest;
		}
	}

	public int Progress {
		get { return QuestProgress.Progress; }
		private set {
			QuestProgress.Progress = value;
			IPlayerPrefs.Set(Key, QuestProgress);
		}
	}

	public int Level {
		get { return QuestProgress.Level; }
		private set {
			QuestProgress.Level = value;
			IPlayerPrefs.Set(Key, QuestProgress);
		}
	}

	public bool Done {
		get { return Level > Info.Count; }
	}

	public bool CanClaim {
		get { return Progress >= Info[Level - 1].Needed; }
	}

	#endregion


	public void AddProgress(int number) {
		if (Level <= Info.Count) {
			//Progress = IsAdd ? Progress + number : number;
			if (IsAdd) {
				Progress += number;
			} else {
				if (number > Progress ) {
					Progress = number;
				}
			}
			if (OnProgressChanged != null) {
				OnProgressChanged.Invoke();
			}
		}
	}

    public void ResetProgress()
    {
        Level = 1;
        Progress = 0;
    }


    public void UpLevel() {
		Level += 1;
	}

	public void GetReward(int multi) {
		QuestInfo questInfo = Info[Level - 1];
		questInfo.GetReward(multi);
	}
}

[Serializable]
public class QuestInfo {
	public string Description;
	public int Needed;
	public List<RewardInfo> RewardInfo = new List<RewardInfo>();

	public void GetReward(int multi) {
		foreach (var reward in RewardInfo) {
			reward.GetReward(multi);
		}
	}
}

[Serializable]
public class QuestProgress {
	public int Progress;
	public int Level;

	public QuestProgress() {
		Progress = 0;
		Level = 1;
	}
}


[Serializable]
public abstract class RewardInfo : ScriptableObject {

    public RewardType RewardType;
    public int Index;
    public string Key;
    public abstract void GetReward(int multi);
}

public enum RewardType {
    Default = -1,
    Currency, Card, Box
}

public enum QuestName {
    #region Achievement
    New_Adventure_Begins, Never_Play_Alone, Strength_In_Numbers, Power_Overwhelming, Greed_Is_Good, The_Collector,
    Mighty_Fighters, Barbarian, D_Rank, C_Rank, B_Rank, A_Rank, S_Rank, Boss_Rush, Epic_Crusaders, New_Skill_Learnt,
    Fish_Hoarder, Treasure_Hunter, High_Score, Elite_Force, Rush_Hour, Total_Domination,
    #endregion


    #region Mission
     HERO_UpgradeLevel_1=99, HERO_UgradeSkill_1, HERO_UgradeSkill_2, HERO_Skin, HERO_UpgradeLevel_2,
     ALLIES_Mana, ALLIES_UpgradeLevel, ALLIES_Rank, ALLIES_ADD,
     COMBAT_Participation, COMBAT_Complete, COMBAT_SingleBattle, COMBAT_Synchronized, COMBAT_KillBoss,
     Other_Login, Other_Gem, Other_Safe, COMBAT_TEMP
    #endregion
}
