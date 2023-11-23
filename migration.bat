@echo off 
 
REM Solicitar el nombre de la migración 
set /p nombreMigracion="Nombre de la migracion: " 
 
REM Generar la migración 
dotnet ef migrations add !nombreMigracion! --project ./Persistence/ --startup-project ./API/ --output-dir ./Data/Migrations 
 
REM Actualizar la base de datos 
dotnet ef database update --project ./Persistence/ --startup-project ./API/ 
