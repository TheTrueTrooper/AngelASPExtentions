﻿#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: July 18,2017
//Project Goal: Encapsulate code for reuse and make more readable.
//File Goal: To add a PostRedirect and other redirects as needed to the controller class  
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: ASP.NetMVC-razor-Example-TaskPlanner 
//  Writer/Publisher:Angelo Sanches (BitSan)
//  Link: https://github.com/TheTrueTrooper/ASP.NetMVC-razor-Example-TaskPlanner
//
//  Name: ASP.net
//  Writer/Publisher: Microsoft
//  Link: https://www.asp.net/
//  }
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AngelASPExtentions.ASPMVCCustomResults;
using System.Reflection;
using AngelASPExtentions.ExtraExtentions.Types;

namespace AngelASPExtentions.ASPMVCControllerExtentions
{
    public static class RedirectControllerExtentions
    {

        /// <summary>
        ///     This Redirects to a an action in any controller
        /// Throws
        ///     an exeption if the data is not flat. Use only simple serialzable data types in data as a property
        ///     this is to ensure a consitency
        /// </summary>
        /// <param name="This">a controller to call from/extend</param>
        /// <param name="Action">The action to use</param>
        /// <param name="Contoller">The location of the controller</param>
        /// <param name="Data">The Data to use</param>
        /// <returns>a PostRedirectResult that redirects to a post</returns>
        public static ActionResult RedirectToPostAction(this Controller This, string Action, string Contoller, object Data)
        {
            Type DataType;
            IList<PropertyInfo> props = null;

            if (Data != null)
            {
                //first get its type
                DataType = Data.GetType();
                //then make a list of properties from it
                props = new List<PropertyInfo>(DataType.GetProperties());

                //check to see if any type is too complicated
                if (props.All(x => !x.PropertyType.IsSimpleType()))
                    throw new Exception("Type is too complex. To ensure perdicable results please ensure all properties types are simple ");
            }

            return new PostRedirectResult(Action, Contoller, Data);
        }

        /// <summary>
        ///     This Redirects to a an action in curret controller
        /// Throws
        ///     an exeption if the data is not flat. Use only simple serialzable data types in data as a property
        ///     this is to ensure a consitency
        /// </summary>
        /// <param name="This">a controller to call from/extend</param>
        /// <param name="Action">The action to use</param>
        /// <param name="Data">The Data to use</param>
        /// <returns>a PostRedirectResult that redirects to a post</returns>
        public static ActionResult RedirectToPostAction(this Controller This, string Action, object Data)
        {

            return RedirectToPostAction(This, Action, This.ControllerContext.RouteData.Values["controller"].ToString(), Data);
        }
    }
}
