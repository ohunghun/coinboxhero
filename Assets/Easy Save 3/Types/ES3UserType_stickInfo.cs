using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("level_power", "count")]
	public class ES3UserType_stickInfo : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_stickInfo() : base(typeof(stickInfo)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (stickInfo)obj;
			
			writer.WriteProperty("level_power", instance.level_power, ES3Type_int.Instance);
			writer.WriteProperty("count", instance.count, ES3Type_int.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (stickInfo)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "level_power":
						instance.level_power = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "count":
						instance.count = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new stickInfo();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_stickInfoArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_stickInfoArray() : base(typeof(stickInfo[]), ES3UserType_stickInfo.Instance)
		{
			Instance = this;
		}
	}
}