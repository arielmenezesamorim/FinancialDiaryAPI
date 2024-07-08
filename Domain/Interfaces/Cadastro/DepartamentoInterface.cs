﻿using Domain.Interfaces.Generics;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastro
{
    public interface DepartamentoInterface : IGeneric<Departamento>
    {
        public Departamento Get(string sigla);
        public object Filtrar(DepartamentoFiltro filtro);
    }
}
