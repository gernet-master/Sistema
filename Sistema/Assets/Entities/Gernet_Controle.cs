﻿/*
Descrição: Classe con a configuração do cliente
Data: 01/01/2020 - v.1.0
*/

using Functions;
using Sistema.Assets.DB;
using Sistema.Assets.Functions;
using System;

namespace Sistema.Assets.Entities
{
    // Configuração do Cliente
    public class Gernet_Controle : Audit
    {

        // Variáveis
        public Variable idgernet = new Variable(config: "0|0");
        public Variable txlink = new Variable(config: "0|0");
        public Variable txcliente = new Variable(config: "0|0");
        public Variable qtunidades = new Variable(config: "0|0");

        // Inicial
        public Gernet_Controle()
        {
            this.idgernet.value = 0;
            this.txlink.value = "";
            this.txcliente.value = "";
            this.qtunidades.value = 0;
        }

    }
}