using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace lebedev
{
    public class Flight
    {
        public string avia { get; set; }
        public int number { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string dayfrom { get; set; }
        public string dayto { get; set; }
        public string timefrom { get; set; }
        public string timeto { get; set; }
        public string flyclass { get; set; }
        public int countfree { get; set; }
        public Flight()
        {

        }
        public Flight(BsonDocument doc)
        {
            avia = doc["avia"].AsString;
            number = doc["number"].AsInt32;
            from = doc["from"].AsString;
            to = doc["to"].AsString;
            dayfrom = doc["dayfrom"].AsString;
            dayto = doc["dayto"].AsString;
            timefrom = doc["timefrom"].AsString;
            timeto = doc["timeto"].AsString;
            flyclass = doc["flyclass"].AsString;
            countfree = doc["countfree"].AsInt32;

        }
    }
}
