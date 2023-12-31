﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HrHarmony.Data.Models.ViewModels;
using HrHarmony.Data.Models.ViewModels.Home;
using HrHarmony.Consts;

namespace HrHarmony.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var ret = new List<IndexViewModel>
        {
            new IndexViewModel
            {
                ImagePath = "/Content/Pracownicy.jpg",
                ImageAlt = "Zarządzaj pracownikami",
                FrontTitle = "Pracownicy",
                FrontText = "",
                BackSideTitle = "System zarządzania pracownikami",
                BackSideText = "Zarządzaj swoim zespołem efektywnie dzięki naszemu systemowi do zarządzania pracownikami.",
                EntityName = EntitiesNames.Employee,
            },
            new IndexViewModel
            {
                ImagePath = "/Content/Umowy.jpg",
                ImageAlt = "Zarządzaj umowami",
                FrontTitle = "Umowy",
                FrontText = "",
                BackSideTitle = "System zarządzania umowami",
                BackSideText = "System zarządzania umowami pozwoli Ci skutecznie monitorować i organizować dokumenty związane z zatrudnieniem.",
                EntityName = EntitiesNames.EmploymentContract,
            },
            new IndexViewModel
            {
                ImagePath = "/Content/Absencje.jpeg",
                ImageAlt = "Zarządzaj absencjami",
                FrontTitle = "Absencje",
                FrontText = "",
                BackSideTitle = "System zarządzania absencjami",
                BackSideText = "Zarządzaj absencjami pracowników w łatwy sposób za pomocą naszego systemu do śledzenia i raportowania.",
                EntityName = EntitiesNames.Absence,
            },
            new IndexViewModel
            {
                ImagePath = "/Content/Wynagrodzenia.jpg",
                ImageAlt = "Zarządzaj wynagrodzeniami",
                FrontTitle = "Wynagrodzenia",
                FrontText = "",
                BackSideTitle = "System zarządzania wynagrodzeniami",
                BackSideText = "System zarządzania wynagrodzeniami umożliwi Ci efektywne monitorowanie i administrację wynagrodzeniami pracowników.",
                EntityName = EntitiesNames.Salary,
            },
            new IndexViewModel
            {
                ImagePath = "/Content/Others.png",
                ImageAlt = "Zarządzaj rodzajami absencji",
                FrontTitle = "Rodzaje absencji",
                FrontText = "",
                BackSideTitle = "System zarządzania rodzajami absencji",
                BackSideText = "Sprawdź nasz system zarządzania rodzajami absencji, aby skutecznie kategoryzować i monitorować nieobecności pracowników.",
                EntityName = EntitiesNames.AbsenceType,
            },
            new IndexViewModel
            {
                ImagePath = "/Content/Others.png",
                ImageAlt = "Zarządzaj adresami",
                FrontTitle = "Adresy",
                FrontText = "",
                BackSideTitle = "System zarządzania adresami",
                BackSideText = "Efektywnie zarządzaj informacjami adresowymi pracowników dzięki naszemu systemowi zarządzania adresami.",
                EntityName = EntitiesNames.Address,
            },
            new IndexViewModel
            {
                ImagePath = "/Content/Others.png",
                ImageAlt = "Zarządzaj rodzajami umów",
                FrontTitle = "Rodzaje umów",
                FrontText = "",
                BackSideTitle = "System zarządzania rodzajami umów",
                BackSideText = "Poznaj różnorodność umów pracowniczych za pomocą naszego systemu zarządzania rodzajami umów.",
                EntityName = EntitiesNames.ContractType,
            },
            new IndexViewModel
            {
                ImagePath = "/Content/Others.png",
                ImageAlt = "Zarządzaj poziomami wykształcenia",
                FrontTitle = "Poziomy wykształcenia",
                FrontText = "",
                BackSideTitle = "System zarządzania poziomami wykształcenia",
                BackSideText = "System zarządzania poziomami wykształcenia umożliwi Ci skuteczne monitorowanie kwalifikacji edukacyjnych pracowników.",
                EntityName = EntitiesNames.EducationLevel,
            },
            new IndexViewModel
            {
                ImagePath = "/Content/Others.png",
                ImageAlt = "Zarządzaj doświadczeniami",
                FrontTitle = "Doświadczenia",
                FrontText = "",
                BackSideTitle = "System zarządzania doświadczeniami",
                BackSideText = "Zarządzaj doświadczeniem zawodowym pracowników za pomocą naszego systemu śledzenia kariery zawodowej.",
                EntityName = EntitiesNames.Experience,
            },
            new IndexViewModel
            {
                ImagePath = "/Content/Others.png",
                ImageAlt = "Zarządzaj stanami cywilnymi",
                FrontTitle = "Rodzaje stanów cywilnych",
                FrontText = "",
                BackSideTitle = "System zarządzania stanami cywilnymi",
                BackSideText = "System zarządzania stanami cywilnymi pomoże w efektywnym monitorowaniu i organizowaniu informacji dotyczących stanu cywilnego pracowników.",
                EntityName = EntitiesNames.MaritalStatus,
            },
        };

        return View(ret);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}