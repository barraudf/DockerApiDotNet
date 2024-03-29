﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerApiDotNet.JsonConverters
{
	public class UnixDateTimeConverter : DateTimeConverterBase
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Integer)
			{
				throw new Exception(
					String.Format("Unexpected token parsing date. Expected Integer, got {0}.",
					reader.TokenType));
			}

			long ticks = (long)reader.Value;

			DateTime date = new DateTime(1970, 1, 1);
			date = date.AddSeconds(ticks);

			return date;
		}

		public override void WriteJson(JsonWriter writer, object value,	JsonSerializer serializer)
		{
			long ticks;
			if (value is DateTime)
			{
				DateTime epoc = new DateTime(1970, 1, 1);
				TimeSpan delta = ((DateTime)value) - epoc;
				if (delta.TotalSeconds < 0)
				{
					throw new ArgumentOutOfRangeException(
						"Unix epoc starts January 1st, 1970");
				}
				ticks = (long)delta.TotalSeconds;
			}
			else
			{
				throw new Exception("Expected date object value.");
			}
			writer.WriteValue(ticks);
		}
	}
}
