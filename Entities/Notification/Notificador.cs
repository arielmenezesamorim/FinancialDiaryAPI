using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notification
{
    public class Notificador : INotificador
    {
        public List<Notificacao> notificacoes;

        public Notificador()
        {
            this.notificacoes = new List<Notificacao>();
        }

        public void Adicionar(Notificacao notificacao)
        {
            this.notificacoes.Add(notificacao);
        }

        public void LimparNotificacoes()
        {
            this.notificacoes.Clear();
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return this.notificacoes;
        }

        public bool TemNotificacao()
        {
            return this.notificacoes.Any();
        }
    }
}
