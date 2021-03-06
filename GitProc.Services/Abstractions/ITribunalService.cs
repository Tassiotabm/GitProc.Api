﻿using GitProc.Model.Data;
using System;
using System.Threading.Tasks;

namespace GitProc.Services
{
    public interface ITribunalService
    {
        Task<ProcessoMaster> GetOnlineProcessData(string processoNumber, Guid AdvogadoId, string Nick);
        Task UpdateProcess(Guid processoMasterId, string ProcessNumber, string Nick);
    }
}