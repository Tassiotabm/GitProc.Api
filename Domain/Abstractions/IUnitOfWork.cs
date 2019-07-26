using GitProc.Data.Repository;
using GitProc.Data.Repository.Abstractions;
using System;
using System.Threading.Tasks;

namespace GitProc.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IAdvogadoRepository Advogado { get; }
        IUsuarioRepository Usuario { get; }
        IEscritorioRepository Escritorio { get; }
        IProcessoMasterRepository ProcessoMaster { get; }
        IProcessoRepository Processo{ get; }
        IMovimentoRepository Movimento { get; }
        IComentarioRepository Comentario { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}