using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("level_LimitCoin", "level_DoubleCoin", "level_GoodCoin")]
	public class ES3UserType_coinGun : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_coinGun() : base(typeof(coinGun)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (coinGun)obj;
			
			writer.WriteProperty("level_LimitCoin", instance.level_LimitCoin);
			writer.WriteProperty("level_DoubleCoin", instance.level_DoubleCoin);
			writer.WriteProperty("level_GoodCoin", instance.level_GoodCoin);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (coinGun)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "level_LimitCoin":
						instance.level_LimitCoin = reader.Read<CodeStage.AntiCheat.ObscuredTypes.ObscuredInt>();
						break;
					case "level_DoubleCoin":
						instance.level_DoubleCoin = reader.Read<CodeStage.AntiCheat.ObscuredTypes.ObscuredInt>();
						break;
					case "level_GoodCoin":
						instance.level_GoodCoin = reader.Read<CodeStage.AntiCheat.ObscuredTypes.ObscuredInt>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_coinGunArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_coinGunArray() : base(typeof(coinGun[]), ES3UserType_coinGun.Instance)
		{
			Instance = this;
		}
	}
}