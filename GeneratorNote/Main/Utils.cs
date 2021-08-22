using GeneratorNote.DTOs;
using GeneratorNote.Interface;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace GeneratorNote.Main
{
    public class Utils : IUtils
    {
        public async Task<bool> GenerateNfe(string pathOrigin, string pathDestiny, int quantity)
        {
            var nfe = new NFe();
            var serializer = new XmlSerializer(typeof(NFe));

            #region Busca arquivo Base

            try
            {
                using (StreamReader reader = new StreamReader(pathOrigin))
                {
                    nfe = (NFe)serializer.Deserialize(reader);
                }
            }
            catch (Exception e )
            {
                MessageBox.Show($"FALHA AO LER ARQUIVO DE ORIGEM, FAVOR VERIFIQUE!\n \n ERRO: {e.Message}", $"AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return await Task.FromResult(false);
            }
            #endregion

            #region Gera novos arquivos

            
            try
            {
                for (int i = 0; i <= quantity; i++)
                {
                    if (!string.IsNullOrEmpty(nfe.InfNFe.Ide.NNF))
                    {
                        int nNfnew = Convert.ToInt32(nfe.InfNFe.Ide.NNF) + 1;
                        nfe.InfNFe.Ide.NNF = Convert.ToString(nNfnew).PadLeft(9, '0');
                        string pathDestinyBase = pathDestiny + $"\\Nfe{nfe.InfNFe.Ide.NNF}.xml";

                        using (TextWriter textWriter = new StreamWriter(pathDestinyBase))
                        {
                            var xmlSerializer = new XmlSerializer(nfe.GetType());
                            xmlSerializer.Serialize(textWriter, nfe);
                        }
                    }
                    else
                    {
                        MessageBox.Show("A Tag <nNfe> se encontra vazia ou não existe, favor verifique!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return await Task.FromResult(false);
                    }
                }

                   
                
                return await Task.FromResult(true);

            }
            catch (Exception e)
            {
                MessageBox.Show($"Ocorreu um erro ao gerar novos arquivos, Favor verifique!\n \n Mensagem: {e.Message}", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return await Task.FromResult(false);
            }
            #endregion

        }
        public async Task<bool> GenerateCte(string pathOrigin, string pathDestiny, int quantity)
        {
            return await Task.FromResult(false);
        }
        public async Task<bool> GenerateMdfe(string pathOrigin, string pathDestiny, int quantity)
        {
            return await Task.FromResult(false);
        }
        public async Task<bool> GenerateNfse(string pathOrigin, string pathDestiny, int quantity)
        {
            return await Task.FromResult(false);
        }
    }
}
