using RetroFront.Models;
using RetroFront.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.Services.Implementation
{
    public class MainService : IMainService
    {
        DatabaseService dbService;
        RetroarchService retroarchService;
        FileJSONService FileJSONService;

        public MainService()
        {
            dbService = new DatabaseService();
            retroarchService = new RetroarchService();
            FileJSONService = new FileJSONService();
            LoadingParam();
        }

        public void LoadingParam()
        {
            var jsonparam = FileJSONService.GetAllSysFromJSON();
            foreach (var jsonsys in jsonparam)
            {
                if (dbService.GetSystemeByName(jsonsys.Shortname) == null)
                {
                    dbService.AddSystem(jsonsys);
                }
            }
        }
    }
}
