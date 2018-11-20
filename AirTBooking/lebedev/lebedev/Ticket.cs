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
    class Ticket
    {
        public int number { get; set; }
        public string F { get; set; }
        public string I { get; set; }
        public string O { get; set; }
        public int count { get; set; }
        public Ticket(BsonDocument doc)
        {
            number = doc["number"].AsInt32;
            F = doc["F"].AsString;
            I = doc["I"].AsString;
            O = doc["O"].AsString;
            count = doc["count"].AsInt32;

        }
        public Ticket()
        {

        }
    }
}
