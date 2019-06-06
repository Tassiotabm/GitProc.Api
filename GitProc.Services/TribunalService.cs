using GitProc.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using GitProc.Model.Data;
using GitProc.Services.Abstractions;

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

                var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

                _processoMasterService.SaveProcessoMaster(processoMaster);

            } catch(HtmlWebException)
            {
                throw;
            }
        }
    }
}


/*
         string urlAddress = "http://www4.tjrj.jus.br/consultaProcessoWebV2/consultaProc.do?v=2&numProcesso=" + processoNumber;
         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
         HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    if (response.StatusCode == HttpStatusCode.OK)
         {
             Stream receiveStream = response.GetResponseStream();
             StreamReader readStream = null;

             if (response.CharacterSet == null)
             {
                 readStream = new StreamReader(receiveStream);
             }
             else
             {
                 readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
             }

             string data = readStream.ReadToEnd();

             response.Close();
             readStream.Close();
         }
*/
