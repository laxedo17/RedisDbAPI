using System.Text.Json;
using RedisDbAPI.Models;
using StackExchange.Redis;

namespace RedisDbAPI.Data
{
    public class RedisPlataformaRepo : IRedisPlataformaRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPlataformaRepo(IConnectionMultiplexer redis)
        {
            _redis = redis; //Agreagado con Dependency Injection
        }
        public void CreatePlataforma(Plataforma plat)
        {
            if (plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }

            var db = _redis.GetDatabase();
            var serialPlat = JsonSerializer.Serialize(plat);
            // db.StringSet(plat.Id, serialPlat);

            // db.SetAdd("PlataformaSet", serialPlat);
            #region Refactorizacion para usar Hashset

            db.HashSet("hashplataforma", new HashEntry[]
            {new HashEntry(plat.Id, serialPlat)});
            #endregion
        }

        public Plataforma? GetPlataformaPorId(string id)
        {
            var db = _redis.GetDatabase();
            //var plat = db.StringGet(id);

            var plat = db.HashGet("hashplataforma", id);

            if (!string.IsNullOrEmpty(plat))
            {
                return JsonSerializer.Deserialize<Plataforma>(plat);
            }
            return null;
        }

        public IEnumerable<Plataforma?>? GetTodasPlataformas()
        {
            var db = _redis.GetDatabase();
            // var setCompleto = db.SetMembers("PlataformaSet");

            var hashCompleto = db.HashGetAll("hashplataforma");

            if (hashCompleto.Length > 0)
            {
                var obj = Array.ConvertAll(hashCompleto, val => JsonSerializer.Deserialize<Plataforma>(val.Value)).ToList();

                return obj;
            }

            return null;

        }
    }
}