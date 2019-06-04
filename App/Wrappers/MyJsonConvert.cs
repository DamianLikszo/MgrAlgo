﻿using Newtonsoft.Json;

namespace App.Wrappers
{
    public class MyJsonConvert : IMyJsonConvert
    {
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
