using System.Threading.Tasks;

namespace GeneratorNote.Interface
{
    public interface IUtils
    {
        public Task<bool> GenerateNfe(string pathOrigin, string pathDestiny, int quantity);
        public Task<bool> GenerateCte(string pathOrigin, string pathDestiny, int quantity);
        public Task<bool> GenerateMdfe(string pathOrigin, string pathDestiny, int quantity);
        public Task<bool> GenerateNfse(string pathOrigin, string pathDestiny, int quantity);
    }
}
