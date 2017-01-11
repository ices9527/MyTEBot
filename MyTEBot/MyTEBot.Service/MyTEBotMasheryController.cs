using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accenture.CIO.AuthenticationFilters;

namespace MyTEBot.Service
{

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Mashery"), BasicAuthenticationFilter]
    public class MyTEBotMasheryController : MyTEBotController
    {
    }
}
