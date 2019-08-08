using GitProc.Data;
using System.Threading.Tasks;
using HtmlAgilityPack;
using GitProc.Model.Data;
using GitProc.Services.Abstractions;
using System.Text.RegularExpressions;
using System.Net;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web;
using System.Text;

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

        public async Task<ProcessoMaster> GetOnlineProcessData(string processoNumber, Guid AdvogadoId, string nick)
        {
            try
            {

                ProcessoMaster processoMaster = new ProcessoMaster
                {
                    ProcessoMasterId = new Guid(),
                    UpdatedDay = DateTime.Now,
                    NumeroProcesso = processoNumber,
                    AdvogadoId = AdvogadoId,
                    Nick = nick
                };
                Encoding iso = Encoding.GetEncoding("iso-8859-1");

                var html = "http://www4.tjrj.jus.br/consultaProcessoWebV2/consultaMov.do?v=2&numProcesso=" + processoNumber + "&acessoIP=internet&tipoUsuario=";
                HtmlWeb web = new HtmlWeb() {
                    AutoDetectEncoding = false,
                    OverrideEncoding = iso,
                };            
                var htmlDoc = web.Load(html);
                var master = new ProcessoMaster
                {
                };

                if (htmlDoc.DocumentNode.SelectSingleNode("//div[@id='container_captcha']") == null)
                {
                    var table = htmlDoc.DocumentNode.SelectSingleNode("//form[@name='formResultado']//table");
                    if (table != null)
                    {
                        var node = table.ChildNodes;

                        for (var i = 0; i < node.Count; i++)
                        {
                            if (node[i].Name != "#text")
                            {
                                foreach (HtmlNode child in node[i].ChildNodes)
                                {
                                    if (child.Name != "#text")
                                    {
                                        if (child.InnerText != "&nbsp;")
                                        {
                                            string convertedText = WebUtility.HtmlDecode(Regex.Replace(child.InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                            string teste = HttpUtility.HtmlDecode(Regex.Replace(child.InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
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
                                            else if (convertedText.Contains("Regional") || convertedText.Contains("Comarca"))
                                            {
                                                var path = node[i].XPath + "/td[2]";
                                                var path2 = node[i + 1].NextSibling.XPath + "/td[2]";

                                                var t1 = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(path).InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                                var t2 = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(path2).InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                                processoMaster.Comarca = t1 + t2;
                                            }
                                            else if (convertedText.Contains("Endereço"))
                                                processoMaster.Endereco = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                            else if (convertedText.Contains("Bairro"))
                                                processoMaster.Bairro = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                            else if (convertedText.Contains("Cidade"))
                                                processoMaster.Cidade = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                            else if (convertedText.Contains("Ação"))
                                                processoMaster.Acao = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                            else if (convertedText.Contains("Assunto"))
                                                processoMaster.Assunto = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                            else if (convertedText.Contains("Classe"))
                                                processoMaster.Classe = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                            else if (convertedText.Contains("Advogado(s)"))
                                                processoMaster.Advogados = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                            else if (convertedText.Contains("Tipo do Movimento"))
                                            {
                                                int j = i;
                                                var listaDeMovimentos = new List<Movimento>();
                                                var contentTag = new List<string>();
                                                var contentData = new List<string>();
                                                var data = new Movimento
                                                {
                                                    MovimentoData = "",
                                                    MovimentoTag = "",
                                                };
                                                while (true)
                                                {
                                                    data.MovimentoTitulo = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[j].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));

                                                    int y = j + 1;
                                                    bool fim = false;

                                                    while (true)
                                                    {
                                                        while (node[y].ChildNodes.Count <= 3 && y <= node.Count)
                                                        {
                                                            y++;
                                                            if (y == node.Count)
                                                            {
                                                                fim = true;
                                                                break;
                                                            }
                                                        }

                                                        if (fim)
                                                        {
                                                            break;
                                                        }
                                                        var tag = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[y].XPath + "/td[1]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                                        var tagData = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[y].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                                        if (tag.Contains("Tipo do Movimento"))
                                                        {
                                                            j = y;
                                                            break;
                                                        }
                                                        contentTag.Add(tag);
                                                        contentData.Add(tagData);

                                                        y++;
                                                        if (node[y].ChildNodes.Count <= 1)
                                                            y++;
                                                    }

                                                    if (fim)
                                                    {
                                                        break;
                                                    }
                                                    data.MovimentoTag = JsonConvert.SerializeObject(contentTag);
                                                    data.MovimentoData = JsonConvert.SerializeObject(contentData);

                                                    if (y == node.Count)
                                                    {
                                                        j = y;
                                                        break;
                                                    }
                                                }
                                                i = j;

                                                master = await _processoMasterService.SaveProcessoMaster(processoMaster);
                                                data.ProcessMasterId = master.ProcessoMasterId;
                                                await _uow.Movimento.Add(data);
                                                _uow.Complete();
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                return master;

            } catch(HtmlWebException)
            {
                throw;
            }
        }

        public async Task UpdateProcess(Guid processoMasterId, string ProcessNumber, string nick)
        {
            try
            {
                var processoMaster = await _uow.ProcessoMaster.SingleOrDefault(x=> x.ProcessoMasterId == processoMasterId);
                string url = processoMaster.NumeroProcesso != null ? processoMaster.NumeroProcesso : ProcessNumber;
                var html = "http://www4.tjrj.jus.br/consultaProcessoWebV2/consultaMov.do?v=2&numProcesso=" + url + "&acessoIP=internet&tipoUsuario=";            
                HtmlWeb web = new HtmlWeb();
                var htmlDoc = web.Load(html);
                processoMaster.UpdatedDay = DateTime.Now;

                if (htmlDoc.DocumentNode.SelectSingleNode("//div[@id='container_captcha']") == null)
                {

                    processoMaster.UpdatedDay = DateTime.Now;
                    var table = htmlDoc.DocumentNode.SelectSingleNode("//form[@name='formResultado']//table");
                    var node = table.ChildNodes;

                    for (var i = 0; i < node.Count; i++)
                    {
                        if (node[i].Name != "#text")
                        {
                            foreach (HtmlNode child in node[i].ChildNodes)
                            {
                                if (child.Name != "#text")
                                {
                                    if (child.InnerText != "&nbsp;")
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
                                        else if (convertedText.Contains("Regional") || convertedText.Contains("Comarca"))
                                        {
                                            var path = node[i].XPath + "/td[2]";
                                            var path2 = node[i + 1].NextSibling.XPath + "/td[2]";

                                            var t1 = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(path).InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                            var t2 = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(path2).InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                            processoMaster.Comarca = convertedText +   t1 + t2;
                                        }
                                        else if (convertedText.Contains("Endereço"))
                                            processoMaster.Endereco = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                        else if (convertedText.Contains("Bairro"))
                                            processoMaster.Bairro = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                        else if (convertedText.Contains("Cidade"))
                                            processoMaster.Cidade = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                        else if (convertedText.Contains("Ação"))
                                            processoMaster.Acao = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                        else if (convertedText.Contains("Assunto"))
                                            processoMaster.Assunto = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                        else if (convertedText.Contains("Classe"))
                                            processoMaster.Classe = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                        else if (convertedText.Contains("Advogado(s)"))
                                            processoMaster.Advogados = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[i].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                        else if (convertedText.Contains("Tipo do Movimento"))
                                        {
                                            int j = i;
                                            var contentTag = new List<string>();
                                            var contentData = new List<string>();
                                            var data = new Movimento
                                            {
                                                MovimentoTitulo = "",
                                                MovimentoData = "",
                                                MovimentoTag = "",
                                                ProcessMasterId = processoMasterId
                                            };

                                            while (true)
                                            {
                                                data.MovimentoTitulo = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[j].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));

                                                int y = j + 1;
                                                bool fim = false;

                                                while (true)
                                                {
                                                    while (node[y].ChildNodes.Count <= 3 && y <= node.Count)
                                                    {
                                                        y++;
                                                        if (y == node.Count)
                                                        {
                                                            fim = true;
                                                            break;
                                                        }
                                                    }

                                                    if (fim)
                                                    {
                                                        break;
                                                    }
                                                    var tag = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[y].XPath + "/td[1]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                                    var tagData = WebUtility.HtmlDecode(Regex.Replace(htmlDoc.DocumentNode.SelectSingleNode(node[y].XPath + "/td[2]").InnerText.Replace("\r\n", string.Empty), " {2,}", " "));
                                                    if (tag.Contains("Tipo do Movimento"))
                                                    {
                                                        j = y;
                                                        break;
                                                    }
                                                    contentTag.Add(tag);
                                                    contentData.Add(tagData);

                                                    y++;
                                                    if (node[y].ChildNodes.Count <= 1)
                                                        y++;
                                                }

                                                if (fim)
                                                {
                                                    break;
                                                }
                                                data.MovimentoTag = JsonConvert.SerializeObject(contentTag);
                                                data.MovimentoData = JsonConvert.SerializeObject(contentData);

                                                if (y == node.Count)
                                                {
                                                    j = y;
                                                    break;
                                                }
                                            }
                                            i = j;

                                            await _uow.Movimento.Add(data);
                                            _uow.Complete();
                                        }

                                    }
                                }
                            }
                        }
                    }
                    _processoMasterService.UpdateProcessoMaster(processoMaster);
                    _uow.Complete();
                }
            }
            catch (HtmlWebException)
            {
                throw;
            }
        }
    }
}
