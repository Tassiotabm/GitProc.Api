using GitProc.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitProc.Data.Repository
{
    public class UsuarioRepository : Repository<DomainDbContext, Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DomainDbContext context) : base(context)
        {

        }
    }
}
