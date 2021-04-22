# ContactManagerApplication

Folder Structure
ContactMangaer
    |
     -ContactMangerEvolent|
                           -ContactManagerApi
                           -ContactManagerEvolentTest
                           -ContactManagerEvolent
     -Resources|
                -Database Script
                -Published Web Api
     Instruction 
     - Import script From Resource folder under SQL Server Management Studio to create DB
     - Host Publishde Web Api in IIS
     - Open Solution from ContactMangerEvolent folder
     - If Web api is not hosted on Local IIS, change app setting under ContactManagerEvolent/Web.config
     - Connection for DB can be changed under ContactManagerApi/AppSettings.json 