using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace StatsSA.eRecruitment.WebApi.ExLogger
{
    public class CustomExceptionLogger : ExceptionLogger
    {
        ILog _logger = null;
        public CustomExceptionLogger()
        {
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Error(context.Exception.ToString() + Environment.NewLine);
            //_logger.Error(Environment.NewLine +" Excetion Time: " + System.DateTime.Now + Environment.NewLine  
            //    + " Exception Message: " + context.Exception.Message.ToString() + Environment.NewLine  
            //    + " Exception File Path: " + context.ExceptionContext.ControllerContext.Controller.ToString() + "/" + context.ExceptionContext.ControllerContext.RouteData.Values["action"] + Environment.NewLine);   
        }
        public void Log(string ex)
        {
            _logger.Error(ex);
            //_logger.Error(Environment.NewLine +" Excetion Time: " + System.DateTime.Now + Environment.NewLine  
            //    + " Exception Message: " + context.Exception.Message.ToString() + Environment.NewLine  
            //    + " Exception File Path: " + context.ExceptionContext.ControllerContext.Controller.ToString() + "/" + context.ExceptionContext.ControllerContext.RouteData.Values["action"] + Environment.NewLine);   
        }
    }
}