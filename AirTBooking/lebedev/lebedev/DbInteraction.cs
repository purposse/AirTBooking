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
    class DbInteraction
    {
        static IMongoCollection<BsonDocument> collectionFlights;
        static IMongoCollection<BsonDocument> collectionTickets;

        public DbInteraction()
        {
            string connectionString = "mongodb://admin:admin@ds119150.mlab.com:19150/reshardlime_db";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("reshardlime_db");
            collectionFlights = database.GetCollection<BsonDocument>("flights");
            collectionTickets = database.GetCollection<BsonDocument>("tickets");
        }

        public static void getListFlights(List<Flight> l)
        {
            l.Clear();
            _getListFlights(l).GetAwaiter().GetResult();
        }

        public static async Task _getListFlights(List<Flight> l)
        {
            var filter = new BsonDocument();
            using (var cursor = await collectionFlights.FindAsync(filter).ConfigureAwait(false))
            {
                while (await cursor.MoveNextAsync())
                {
                    var a_cur = cursor.Current;
                    foreach (var doc in a_cur)
                    {
                        l.Add(new Flight(doc));
                    }
                }
            }
        }

        public static void delFlight(int id)
        {
            _delFlight(id).GetAwaiter().GetResult();
        }

        private static async Task _delFlight(int id)
        {
            var filter = new BsonDocument
            {
                {"number", id }
            };
            await collectionFlights.DeleteOneAsync(filter).ConfigureAwait(false);
        }

        public static void delTicket(int num, string F, string I, string O, int countt)
        {
            _delTicket(num, F, I, O).GetAwaiter().GetResult();
            _decCount(num, -countt).GetAwaiter().GetResult();
        }

        private static async Task _delTicket(int num, string F, string I, string O)
        {
            var filter = new BsonDocument
            {
                {"number", num },
                {"F", F },
                {"I", I },
                {"O", O }
            };
            await collectionTickets.DeleteOneAsync(filter).ConfigureAwait(false);
        }

        public static void getListTickets(List<Ticket> l)
        {
            l.Clear();
            _getListTickets(l).GetAwaiter().GetResult();
        }

        public static async Task _getListTickets(List<Ticket> l)
        {
            var filter = new BsonDocument();
            using (var cursor = await collectionTickets.FindAsync(filter).ConfigureAwait(false))
            {
                while (await cursor.MoveNextAsync())
                {
                    var a_cur = cursor.Current;
                    foreach (var doc in a_cur)
                    {
                        l.Add(new Ticket(doc));
                    }
                }
            }
        }

        public static void addNewFlight(string av, int num, string fr, string tto, string dayfr, string daytto,
            string timefr, string timetto, string clas, int count)
        {
            Flight l = new Flight
            {
                avia = av,
                number = num,
                from = fr,
                to = tto,
                dayfrom = dayfr,
                dayto = daytto,
                timefrom = timefr,
                timeto = timetto,
                flyclass = clas,
                countfree = count
            };
            _addNewFlight(l).GetAwaiter().GetResult();
        }

        public static void replaceFlight(string av, int num, string fr, string tto, string dayfr, string daytto,
            string timefr, string timetto, string clas, int count)
        {
            Flight l = new Flight
            {
                avia = av,
                number = num,
                from = fr,
                to = tto,
                dayfrom = dayfr,
                dayto = daytto,
                timefrom = timefr,
                timeto = timetto,
                flyclass = clas,
                countfree = count
            };
            _replaceFlight(l).GetAwaiter().GetResult();
        }

        public static void addNewTicket(string num, string f, string i, string o, string countt)
        {
            _addNewTicket(new Ticket
            {
                number = Convert.ToInt32(num),
                F = f,
                I = i,
                O = o,
                count = Convert.ToInt32(countt)
            }).GetAwaiter().GetResult();
        }

        public static void decCount(int num, int dec)
        {
            _decCount(num, dec).GetAwaiter().GetResult();
        }

        private static async Task _decCount(int num, int dec)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("number", num);
            var updatee = Builders<BsonDocument>.Update.Inc("countfree", -dec);
            await collectionFlights.UpdateOneAsync(filter, updatee).ConfigureAwait(false);
        }

        private static async Task _addNewTicket(Ticket t)
        {
            BsonDocument doc = new BsonDocument
            {
                {"number", t.number },
                {"F", t.F },
                {"I", t.I },
                {"O",t.O },
                {"count", t.count }
            };
            await collectionTickets.InsertOneAsync(doc).ConfigureAwait(false);
        }

        private static async Task _addNewFlight(Flight l)
        {
            BsonDocument doc = new BsonDocument
            {
                {"avia", l.avia },
                {"number", l.number },
                {"from", l.from },
                {"to", l.to },
                {"dayfrom", l.dayfrom },
                {"dayto", l.dayto },
                {"timefrom", l.timefrom },
                {"timeto", l.timeto },
                {"flyclass", l.flyclass },
                {"countfree", l.countfree }
            };
            await collectionFlights.InsertOneAsync(doc).ConfigureAwait(false);
        }
        private static async Task _replaceFlight(Flight l)
        {
            BsonDocument filter = new BsonDocument
            {
                {"number", l.number }
            };
            BsonDocument doc = new BsonDocument
            {
                {"avia", l.avia },
                {"number", l.number },
                {"from", l.from },
                {"to", l.to },
                {"dayfrom", l.dayfrom },
                {"dayto", l.dayto },
                {"timefrom", l.timefrom },
                {"timeto", l.timeto },
                {"flyclass", l.flyclass },
                {"countfree", l.countfree }
            };
            await collectionFlights.ReplaceOneAsync(filter, doc).ConfigureAwait(false);
        }
    }
}
