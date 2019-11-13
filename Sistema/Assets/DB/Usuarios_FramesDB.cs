using Functions;
using Sistema.Assets.Entities;
using Sistema.Assets.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Sistema.Assets.DB
{
    public class Usuarios_FramesDB : Session
    {
        // Exclui todos os registros de frames
        public void Excluir(int idusuario = 0)
        {
            try
            {
                Connection session = new Connection();
                Query query = session.CreateQuery(@"DELETE FROM usuarios_frames WHERE idusuario = " + idusuario);
                query.ExecuteUpdate();
                session.Close();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        
    }
}

