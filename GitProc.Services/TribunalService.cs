using GitProc.Data;
using System.Threading.Tasks;
using HtmlAgilityPack;
using GitProc.Model.Data;
using GitProc.Services.Abstractions;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Net;
using System;

namespace GitProc.Services
{
    public class TribunalService : ITribunalService
    {
        private readonly IUnitOfWork _uow;
        private readonly IProcessoMasterService _processoMasterService;

        public TribunalService(IUnitOfWork uow, IProcessoMasterService processoMasterService)
        {
            _uow = uow;
            _processoMasterService = processoMasterService;
        }

        public async Task GetOnlineProcessData(string processoNumber)
        {
            try
            {
                var html = "http://www4.tjrj.jus.br/consultaProcessoWebV2/consultaProc.do?v=2&numProcesso="+ processoNumber;
                HtmlWeb web = new HtmlWeb();            
                var htmlDoc = web.Load(html);
                ProcessoMaster processoMaster = new ProcessoMaster();
                var table = htmlDoc.DocumentNode.SelectSingleNode("//form[@name='formResultado']//table");
                var node = table.ChildNodes;

                for (var i =0; i < node.Count; i++)
                {
                    if(node[i].Name != "#text")
                    {
                       foreach(HtmlNode child in node[i].ChildNodes)
                        {
                            if(child.Name  != "#text")
                            {
                                if(child.InnerText != "&nbsp;")
                                {
                                    string convertedText = WebUtility.HtmlDecode(Regex.Replace(child.InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                    if (convertedText.Contains(" As informações aqui contidas não produzem efeitos legais. Somente a publicação no DJERJ oficializa despachos e decisões e estabelece prazos."))
                                    {

                                    }
                                    else if (convertedText.Contains("instância"))
                                    {
                                        var stringArray = convertedText.Split(" - ");
                                        processoMaster.Tribunal = stringArray[0];
                                        processoMaster.DataVerificacao = Convert.ToDateTime(stringArray[1]);
                                        processoMaster.Instancia = stringArray[2];
                                        processoMaster.DataDistribuicao = Convert.ToDateTime(stringArray[3].Substring(stringArray[3].Length - 10));
                                    }
                                    else if (convertedText.Contains("Regional")  || convertedText.Contains("Comarca"))
                                    {
                                        var path = node[i].XPath+"/td[2]";
                                        var path2 = node[i+1].NextSibling.XPath + "/td[2]";

                                        var t1 = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(path).InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                        var t2 = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(path2).InnerText.Replace("\r\n", string.Empty), " {2,}", " "));

                                        processoMaster.Comarca = t1 + t2;
                                    }
                                    else if (convertedText.Contains("Endereço"))
                                    {
                                        processoMaster.Endereco = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode("//tr[14]//td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));

                                    }
                                    else if (convertedText.Contains("Bairro"))
                                    {
                                        processoMaster.Bairro = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode("//tr[15]//td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));

                                    }
                                    else if (convertedText.Contains("Cidade"))
                                    {
                                        processoMaster.Cidade = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode("//tr[16]//td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));

                                    }
                                    else if (convertedText.Contains("Ação"))
                                    {
                                        processoMaster.Acao = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode("//tr[18]//td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));

                                    }
                                    else if (convertedText.Contains("Advogado"))
                                    {

                                    }
                                    else if (convertedText.Contains("Assunto"))
                                    {
                                        processoMaster.Assunto = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode("//tr[20]//td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                    }
                                    else if (convertedText.Contains("Classe"))
                                    {
                                        processoMaster.Classe = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode("//tr[22]//td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                    }
                                }
                            }
                        }
                    }
                }
                
                var advogado = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode("//td[contains(text(),'Advogado(s):')]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                var teste1 = htmlDoc.DocumentNode.SelectSingleNode("//td[contains(text(),'Advogado(s):')]").NextSibling.NextSibling.InnerText.Trim().Replace("\r\n", string.Empty);

                _processoMasterService.SaveProcessoMaster(processoMaster);

            } catch(HtmlWebException)
            {
                throw;
            }
        }
    }
}
