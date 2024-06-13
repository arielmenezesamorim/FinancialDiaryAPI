using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notification
{
    public interface INotificador
    {
        public void Adicionar(Notificacao notificacao);
        public void LimparNotificacoes();
        public List<Notificacao> ObterNotificacoes();
        public bool TemNotificacao();
    }
}
