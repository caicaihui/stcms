using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ST.CMS.UeditorUpload
{
    public class ConfigHandler : Handler
    {
        public ConfigHandler(HttpContext context) : base(context) { }

        public override void Process()
        {
            WriteJson(Config.Items);
        }
    }
}
