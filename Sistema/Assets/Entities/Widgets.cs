/*
Descrição: Classe de controle de widgets
Data: 01/01/2021 - v.1.0
*/

using Functions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Sistema.Assets.Entities
{

    // Widgets
    public class Widgets
    {
        // Variáveis
        public int xpos { get; set; }
        public int ypos { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int min_width { get; set; }
        public int min_height { get; set; }
        public bool resize { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string name { get; set; }
        public int inputFind { get; set; }
        public string inputFields { get; set; }
        public string inputReturn { get; set; }

        // Inicial
        public Widgets()
        {
            this.xpos = 0;
            this.ypos = 0;
            this.width = 0;
            this.height = 0;
            this.min_width = 0;
            this.min_height = 0;
            this.resize = false;
            this.controller = "";
            this.action = "";
            this.name = "";
            this.inputFind = 0;
            this.inputFields = "";
            this.inputReturn = "";
        }

        public Widgets(int xpos, int ypos, int width, int height, int min_width, int min_height, bool resize, string controller, string action, string name)
        {
            this.xpos = xpos;
            this.ypos = ypos;
            this.width = width;
            this.height = height;
            this.min_width = min_width;
            this.min_height = min_height;
            this.resize = resize;
            this.controller = controller;
            this.action = action;
            this.name = name;
        }

        public Widgets Create(FormCollection form)
        {
            Widgets widget = new Widgets();
            widget.name = Convert.ToString(form["widget_temp_name"]);
            widget.controller = Convert.ToString(form["widget_temp_controller"]);
            widget.action = Convert.ToString(form["widget_temp_action"]);

            // Cria campos para definir se a localização veio de um input
            if (form.AllKeys.Contains("widget_temp_input_find"))
            {
                widget.inputFind = Convert.ToInt32(form["widget_temp_input_find"]);
            }

            if (form.AllKeys.Contains("widget_temp_input_fields"))
            {
                widget.inputFields = Convert.ToString(form["widget_temp_input_fields"]);
            }

            if (form.AllKeys.Contains("widget_temp_input_return"))
            {
                widget.inputReturn = Convert.ToString(form["widget_temp_input_return"]);
            }
            return widget;
        }

    }

    // Listagem
    public class WidgetsListConfig
    {
        // Variáveis
        public int count { get; set; }
        public int[] columns { get; set; }
        public int[] show { get; set; }
        public string order { get; set; }
        public string direction { get; set; }
        public string[] headers { get; set; }
        public string[] fields { get; set; }
        public string[] orderfields { get; set; }
        public int registers { get; set; }
        public int page { get; set; }
        public string[] formatFields { get; set; }

        // Inicial
        public WidgetsListConfig()
        {
            this.count = 0;
            this.columns = null;
            this.show = null;
            this.order = "";
            this.direction = "";
            this.headers = null;
            this.fields = null;
            this.registers = 10;
            this.page = 1;
            this.formatFields = null;
        }
    }
}