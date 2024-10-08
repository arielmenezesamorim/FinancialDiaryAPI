﻿using Domain.Interfaces.Generics;
using Domain.Interfaces.IServices.Cadastro;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastro
{
    public interface BancoInterface : IGeneric<Banco>
    {
        public Banco GetBanco(int sequencia);
        public Banco GetAgencia(int agencia);
        public object Filtrar(BancoFiltro filtro);
    }
}
