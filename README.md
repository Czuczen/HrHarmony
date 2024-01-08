# HrHarmony (w trakcie rozwijania)

HrHarmony to projekt aplikacji do zarządzania zasobami ludzkimi (HR) stworzony w technologii ASP.NET Core 7 MVC i przy użyciu Entity Framework Core. 
Celem tego projektu jest praktyczne zastosowanie technologii ASP.NET Core 7 MVC wraz z wzorcami projektowymi w kontekście zarządzania zasobami ludzkimi w przedsiębiorstwie.  

[Sprawdź online](http://hrharmony-001-site1.gtempurl.com)

## Funkcje

- Zarządzanie danymi pracowników  
- Obsługa umów o pracę  
- Monitorowanie absencji pracowników  
- Zarządzanie wynagrodzeniem  

## Technologie

ASP.NET Core 7 MVC  
Entity Framework Core  

C#  
JavaScript  
HTML  
CSS  
Bootstrap  
xUnit  

## Jak uruchomić aplikację

1. W projekcie "**HrHarmony**" dodaj plik "**secrets.json**" i uzupełnij go według szablonu:  
```  
	{
	  "ConnectionStrings": {
	    "HrHarmony": {
	      "DefaultConnection": "",
	      "TestConnection": ""
	    }
	  },
	  "AccessKeys": {
	    "Visitors": "key",
	    "CreateSampleObjects": "key",
	    "ClearAll": "key",
	    "Logs": "key"
	  }
	}
```  
2. W wartości klucza "**DefaultConnection**" wprowadź informacje dotyczące połączenia z bazą danych MSSQL.
3. Na górnym pasku menu wybierz "Narzędzia", a następnie z rozwijanej listy najedź na "Menedżer pakietów NuGet" i z kolejnej listy wybierz "Konsola menedżera pakietów".  
4. W konsoli menedżera pakietów dla pola "Projekt domyślny" z rozwijanej listy wybierz "HrHarmony".  
5. W konsoli menedżera pakietów wpisz komende "EntityFrameworkCore\Update-Database -Migration Initial -Project HrHarmony" i wciśnij Enter.  
6. Uruchom aplikację (Ctrl+F5).  
  
  
  
> Uwaga: Projekt obecnie jest rozwijany, nadal są wprowadzane zmiany i ulepszenia, niektóre funkcje mogą być dodawane lub zmieniane w kolejnych wersjach.
