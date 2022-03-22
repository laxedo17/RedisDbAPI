using RedisDbAPI.Models;

namespace RedisDbAPI.Data
{
    public interface IRedisPlataformaRepo
    {
        void CreatePlataforma(Plataforma plat);
        Plataforma? GetPlataformaPorId(string id);
        IEnumerable<Plataforma?>? GetTodasPlataformas();
    }
}