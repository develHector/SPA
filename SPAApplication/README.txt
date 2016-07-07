
* Notar que en Entity Framework, siempre tenemos que tener una llave en los modelos
* No entiendo bien la diferencia entre Scafolding y simple Controlador, parece como el SqlMetal

	Install-Package EntityFramework

* Cambios en la base de datos tienen que auto-upgradearse, la forma de hacerlo si truena es a través del PackageManager Console

	Enable-Migrations -ContextTypeName SPAApplication.Models.SPAApplicationContext
	Update-Database -Verbose

* Si rebuzna

	Update-Database -Verbose -Force
