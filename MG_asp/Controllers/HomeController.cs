﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MG_asp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Chat()
        {
            return View();
        }
    }
}
